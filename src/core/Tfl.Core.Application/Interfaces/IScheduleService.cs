using System.Collections.Generic;
using Tfl.Core.Domain.Entities;

namespace Tfl.Core.Application.Interfaces
{
    public interface IScheduleService
    {
		List<Game> GetSchedule(
			string team,
			string leagueCode,
			int season);

		List<Game> GetRoundData(
			int round,
			string leagueCode,
			int season);

		bool HasSeason(
			string leagueCode,
			int season);

		Game GetNextGame(
			string teamCode,
			string leagueCode,
			int season);
	}
}
