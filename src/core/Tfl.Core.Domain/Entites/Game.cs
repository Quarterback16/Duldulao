using System;

namespace Tfl.Core.Domain.Entities
{
    public class Game
    {
        public int Season { get; set; }
        public string? League { get; set; }
        public int Match { get; set; }
        public int Round { get; set; }
        public DateTime GameDate { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }


 
        public string Result()
        {
            return $"{AwayScore} - {HomeScore}";
        }

        public override string ToString()
        {
            return $@"{
                League
                } Round {
                Round
                } : {GameDate:yyyy-MM-dd HH:mm} {
                AwayTeam
                } {AwayScore} @ {
                HomeTeam
                } {HomeScore}";
        }

        public string GameLine()
        {
            var line = $@"{
                League
                } Rd {Round,2} {GameDate:yyyy-MM-dd}";
            return line;
        }

    }
}
