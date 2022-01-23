using Tfl.Core.Data.Services;
using Xunit.Abstractions;
using Xunit;

namespace Application.IntegrationTests
{
	public class ScheduleServiceTests
	{
		private readonly ITestOutputHelper _output;

		public ScheduleServiceTests(
			ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void ScheduleMaster_CanReturnRoundData()
		{
			var cut = new ScheduleService();
			var leagueCode = "NFL";
			var season = 2021;
			Assert.True(cut.HasSeason(leagueCode, season));
			var result = cut.GetRoundData(
				9,
				leagueCode,
				season);
			Assert.NotNull(
				result);
			foreach (var item in result)
			{
				_output.WriteLine(
					item.ToString());
			}
			Assert.Equal(14, result.Count);
		}

		[Fact]
		public void ScheduleMaster_CanReturnNextGameForATeam()
		{
			var cut = new ScheduleService();
			var leagueCode = "NFL";
			var season = 2021;
			var result = cut.GetNextGame(
				"SF",
				leagueCode,
				season);
			Assert.NotNull(
				result);
			_output.WriteLine(
				result.ToString());
		}
	}
}
