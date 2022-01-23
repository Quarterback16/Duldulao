using DbfDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tfl.Core.Domain.Entites;

namespace Tfl.Core.Data.Services
{
    public class TflDataReaderService
    {
        public List<PlayerInfo> Players { get; set; }
        public string PlayerFile { get; set; }

        public Dictionary<string, List<Projection>> Projections { get; set; }

        public TflDataReaderService(
            string fileName = "e:\\tfl\\nfl\\player.dbf")
        {
            // load current players into memory
            Players = new List<PlayerInfo>();
            Projections = new Dictionary<string, List<Projection>>();
            PlayerFile = fileName;
            LoadCurrentPlayers();
        }

        private void LoadCurrentPlayers()
        {
            using var dbfTable = new DbfTable(
                PlayerFile, 
                Encoding.UTF8);
            var skipDeleted = true;
            var dbfRecord = new DbfRecord(dbfTable);
            while (dbfTable.Read(dbfRecord))
            {
                if (skipDeleted && dbfRecord.IsDeleted)
                    continue;
                if (!PlayerIsCurrent(dbfRecord))
                    continue;
                if (!PlayerIsInFantasy(dbfRecord))
                    continue;
                var name = $@"{
                    dbfRecord.Values[4]
                    } {
                    dbfRecord.Values[3]
                    }";
                //if (name.Contains("Mahomes"))
                //    Console.WriteLine("got him");
                Players.Add(
                    new PlayerInfo
                    {
                        Id = dbfRecord.Values[0].ToString(),
                        Name = name,
                        TeamCode = TeamCodeFrom(dbfRecord),
                        Position = PositionFrom(dbfRecord),
                        Level = LevelFrom(dbfRecord)
                    });
            }
            Console.WriteLine($"{Players.Count} players loaded");
        }

        private static int LevelFrom(DbfRecord dbfRecord)
        {
            var role = dbfRecord.Values[6].ToString();
            if (string.IsNullOrEmpty(role))
                return 0;
            if (role.Equals("S"))
                return 1;
            if (role.Equals("B"))
                return 2;
            if (role.Equals("R"))
                return 3;
            if (role.Equals("D"))
                return 4;
            return 0;
        }

        private static string TeamCodeFrom(DbfRecord dbfRecord)
        {
            var teamCode = dbfRecord.Values[5].ToString();
            if (string.IsNullOrEmpty(teamCode))
                return string.Empty;
            return teamCode;
        }

        private static bool PlayerIsInFantasy(DbfRecord dbfRecord)
        {
            var playerCat = dbfRecord.Values[17].ToString();
            if (playerCat == null)
                return false;
            if (Int32.Parse(playerCat) < 5)
                return true;
            return false;
        }

        private static string PositionFrom(DbfRecord dbfRecord)
        {
            var playerCat = dbfRecord.Values[17].ToString();
            var position = string.Empty;
            if (playerCat == "1")
                position = "QB";
            if (playerCat == "2")
                position = "RB";
            if (playerCat == "3")
                position = "WR";
            if (playerCat == "4")
                position = "PK";
            return position;
        }

        private static bool PlayerIsCurrent(
            DbfRecord dbfRecord)
        {
            var currTeam = dbfRecord.Values[5].ToString();
            if (string.IsNullOrWhiteSpace(currTeam))
                return false;
            if (currTeam == "??")
                return false;
            return true;
        }

        public static List<string> ReadDbf(
            string fileName = "e:\\tfl\\nfl\\player.dbf")
        {
            var result = new List<string>();    
            using (var dbfTable = new DbfTable(fileName, Encoding.UTF8))
            {
                var header = dbfTable.Header;

                var versionDescription = header.VersionDescription;
                var hasMemo = dbfTable.Memo != null;
                var recordCount = header.RecordCount;
                var col = 0;
                foreach (var dbfColumn in dbfTable.Columns)
                {

                    var name = dbfColumn.ColumnName;
                    var columnType = dbfColumn.ColumnType;
                    var length = dbfColumn.Length;
                    var decimalCount = dbfColumn.DecimalCount;
                    result.Add(
                        $"{col,3} {name,-15} {columnType,-10} {length,3}.{decimalCount}");
                    col++;
                }
            }
            return result;
        }

        public Projection ReadProjections(
            string season,
            string week,
            string playerName)
        {
            var player = GetPlayer(playerName);
            var result = new Projection();
            var weekKey = $"{season}:{week}";
            var fileName = "e:\\tfl\\nfl\\pgmetric.dbf";
            using (var dbfTable = new DbfTable(fileName, Encoding.UTF8))
            {
                var skipDeleted = true;
                var dbfRecord = new DbfRecord(dbfTable);
                while (dbfTable.Read(dbfRecord))
                {
                    if (skipDeleted && dbfRecord.IsDeleted)
                        continue;
                    var recordKey = ProjectionKey(dbfRecord);
                    if (recordKey != weekKey)
                        continue;
                    var playerId = dbfRecord.Values[0].ToString();
                    if (playerId != player.Id)
                        continue;
                    result.PlayerId = playerId;
                    result.TotalPoints = Decimal.Parse(
                        dbfRecord.Values[18].ToString());
                }
            }
            return result;
        }

        private PlayerInfo GetPlayer(
            string playerName)
        {
            var playerInfo = EmptyPlayerInfo();
            if (Players == null)
                LoadCurrentPlayers();
            if (Players == null)
                return playerInfo;
            var playerFound = Players.Where(p=>p.Name==playerName)
                .FirstOrDefault();
            if (playerFound != null)
                return playerFound;
            return playerInfo;
        }

        private static PlayerInfo EmptyPlayerInfo()
        {
            return new PlayerInfo
            {
                Name = String.Empty
            };
        }

        private static string ProjectionKey(DbfRecord dbfRecord)
        {
            var gameCode = dbfRecord.Values[1].ToString();
            if (string.IsNullOrEmpty(gameCode))
                return string.Empty;
            if (gameCode.Length < 7)
                return string.Empty;
            return gameCode.Substring(0,7);
        }
    }
}
