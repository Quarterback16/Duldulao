using Microsoft.AspNetCore.Mvc;
using Tfl.Core.Application.Interfaces;
using Tfl.Core.Domain.Entites;

namespace Tfl.Core.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectionController : Controller
    {
        private readonly IProjectionService _projectionService;

        public ProjectionController(
            IProjectionService projectionService)
        {
            _projectionService = projectionService;
        }

        [HttpGet("/projection/{season}/{week}/{player}")]
        public Projection Get(
            string season,
            string week,
            string player)
        {
            var result = _projectionService.GetProjectionByName(
                playerName: player,
                season: season,
                week: week);
            return result;
        }
    }
}
