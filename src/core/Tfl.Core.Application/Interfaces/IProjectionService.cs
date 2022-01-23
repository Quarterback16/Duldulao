using Tfl.Core.Domain.Entites;

namespace Tfl.Core.Application.Interfaces
{
    public interface IProjectionService
    {
		Projection GetProjectionByName(
			string playerName,
			string season,
			string week);
	}
}
