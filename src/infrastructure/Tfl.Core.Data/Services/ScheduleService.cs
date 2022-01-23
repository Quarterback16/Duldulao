using System.Collections.Generic;
using System.IO;
using System;
using Tfl.Core.Application.Interfaces;
using Tfl.Core.Domain.Entities;
using Tfl.Core.Data.Events;
using System.Linq;

namespace Tfl.Core.Data.Services
{
    public class ScheduleService :IScheduleService
    {
        public string SchedulePath { get; set; }
        public Dictionary<string, Dictionary<int, List<Game>>> LeagueSchedules
        {
            get;
            set;
        }

        public ScheduleService()
        {
            SchedulePath = "l:\\apps\\Schedules";
            LeagueSchedules = new Dictionary<string, Dictionary<int, List<Game>>>();
            LoadSchedules();
        }

        private void LoadSchedules()
        {
            var path = SchedulePath;
            DirectoryInfo dir = new(path);
            foreach (FileInfo fi in dir.GetFiles())
            {
                if (fi.Name.Length != 22)
                    continue;
                if (fi.Extension.Equals(".json")
                    && fi.Name.Substring(3, 10) == "-schedule-")
                {
                    LoadSchedule(fi.FullName);
                }
            }
        }

        private static int SeasonFrom(
            string fileName)
        {
            string[] parts = fileName.Replace(".json", "").Split('-');
            return int.Parse(parts[2].ToString());
        }

        private void LoadSchedule(
            string fileName)
        {
            var fileSeason = SeasonFrom(
                fileName);

            var ses = new ScheduleEventStore(
                fileName);

            var events = (List<ScheduleEvent>) ses
                .Get<ScheduleEvent>("schedule");
            foreach (var e in events)
            {
                var theGame = new Game
                {
                    Season = fileSeason,
                    League = e.LeagueCode,
                    Round = e.Round,
                    GameDate = e.GameDate,
                    HomeTeam = e.HomeTeam,
                    AwayTeam = e.AwayTeam,
                };
                if (theGame.League == null)
                    continue;
                if (!LeagueSchedules.ContainsKey(
                    LeagueKey(theGame.League, fileSeason)))
                {
                    LeagueSchedules.Add(
                        LeagueKey(theGame.League, fileSeason),
                        new Dictionary<int, List<Game>>());
                }
                var roundDict = LeagueSchedules[LeagueKey(
                    theGame.League,
                    fileSeason)];
                if (!roundDict.ContainsKey(theGame.Round))
                    roundDict.Add(theGame.Round, new List<Game>());
                var gameList = roundDict[theGame.Round];
                gameList.Add(theGame);
                roundDict[theGame.Round] = gameList;
                LeagueSchedules[LeagueKey(theGame.League, fileSeason)]
                    = roundDict;
            }
        }

        public bool HasSeason(
            string leagueCode,
            int season)
        {
            var key = LeagueKey(leagueCode, season);
            if (LeagueSchedules.ContainsKey(key))
                return true;
            return false;
        }

        private static string LeagueKey(
            string league,
            int season)
        {
            return $"{league}:{season}";
        }

        public List<Game> GetSchedule(
            string team,
            string leagueCode,
            int season)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetRoundData(
            int round,
            string leagueCode,
            int season)
        {
            var result = new List<Game>();
            var key = LeagueKey(leagueCode, season);
            if (LeagueSchedules.ContainsKey(key))
            {
                var sched = LeagueSchedules[key];
                var games = sched[round];
                result.AddRange(games);
            }
            return result;
        }

        public Game GetNextGame(
            string teamCode,
            string leagueCode,
            int season)
        {
            var result = new Game();
            var key = LeagueKey(leagueCode, season);
            if (LeagueSchedules.ContainsKey(key))
            {
                var sched = LeagueSchedules[key];
                foreach (var round in sched)
                {
                    var games = round.Value;
                    var gameForTeam = games
                        .Where(g=>g.HomeTeam == teamCode 
                               || g.AwayTeam == teamCode)
                        .FirstOrDefault();
                    if (gameForTeam == null)
                        continue;
                    if (gameForTeam.GameDate > DateTime.Now)
                    {
                        result = gameForTeam;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
