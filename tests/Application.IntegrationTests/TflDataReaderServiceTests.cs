using System.Linq;
using Tfl.Core.Data.Services;
using Tfl.Core.Domain.Entites;
using Xunit;
using Xunit.Abstractions;

namespace Application.IntegrationTests
{
    public class TflDataReaderServiceTests
    {
		private readonly ITestOutputHelper _output;

		public TflDataReaderServiceTests(
			ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void TflDataReaderService_CanReadDbf()
		{
			var result = TflDataReaderService.ReadDbf();
			foreach (var item in result)
			{
				_output.WriteLine(
					item.ToString());
			}
		}

		[Fact]
		public void TflDataReaderService_LoadsPlayers()
		{
			var cut = new TflDataReaderService();
			Assert.True(cut.Players.Any());
			_output.WriteLine($"{cut.Players.Count} players loaded");
		}

		[Fact]
		public void TflDataReaderService_CanReadProjections()
		{
			var cut = new TflDataReaderService();
			var result = cut.ReadProjections(
				"2021",
				"20",
				"Patrick Mahomes");
			Assert.IsType<Projection>(result);
            _output.WriteLine(result.ToString());
		}
	}
}
