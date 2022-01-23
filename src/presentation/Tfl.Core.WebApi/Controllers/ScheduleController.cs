using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tfl.Core.Application.Interfaces;
using Tfl.Core.Domain.Entities;

namespace Tfl.Core.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("/schedule/{season}/{league}/{id}")]
        public IEnumerable<Game> Get(
            int season,
            string league,
            int id)
        {
            var result = _scheduleService.GetRoundData(
                round: id,
                leagueCode: league,
                season: season);
            return result.ToArray();
        }

        [HttpGet("/nextgame/{season}/{league}/{teamcode}")]
        public Game GetNextGame(
            int season,
            string league,
            string teamcode)
        {
            var result = _scheduleService.GetNextGame(
                teamCode: teamcode,
                leagueCode: league,
                season: season);
            return result;
        }
    }
}
