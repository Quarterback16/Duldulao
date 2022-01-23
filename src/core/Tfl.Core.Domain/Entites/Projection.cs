using System.Collections.Generic;

namespace Tfl.Core.Domain.Entites
{
	public class Projection
	{
		public string PlayerId { get; set; }
		public string Season { get; set; }

		public decimal TotalPoints { get; set; }

		public List<PlayerGameMetrics> GameMetrics { get; set; }

        public override string ToString()
        {
            return $"{PlayerId}:{TotalPoints}";
        }
    }
}
