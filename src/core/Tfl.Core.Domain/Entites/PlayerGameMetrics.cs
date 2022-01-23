namespace Tfl.Core.Domain.Entites
{
	public class PlayerGameMetrics
	{
		public string PlayerId { get; set; }
		public string GameKey { get; set; }

		public int ProjTDp { get; set; }
		public int TDp { get; set; }
		public decimal ProjTDr { get; set; }
		public int TDr { get; set; }
		public int ProjTDc { get; set; }
		public int TDc { get; set; }
		public int ProjYDp { get; set; }
		public int YDp { get; set; }
		public int ProjYDr { get; set; }
		public int YDr { get; set; }
		public int ProjYDc { get; set; }
		public int YDc { get; set; }

		public int ProjRec { get; set; }
		public int Rec { get; set; }

		public int ProjFG { get; set; }
		public int FG { get; set; }
		public int ProjPat { get; set; }
		public int Pat { get; set; }

		public decimal FantasyPoints { get; set; }

		public decimal ProjectedFantasyPoints { get; set; }

		public bool IsEmpty { get; set; }


		public PlayerGameMetrics()
		{
			IsEmpty = true;
		}

		public override string ToString()
		{
			if (YDp + TDp + YDr + TDr + YDc + TDc + FG + Pat > 0)
				return string.Format(
				"{0} in {1} actuals: passing>{2,3}-({3})  running>{4,3}-({5})  catch>{6,3}-({7})  kick>{8}-{9}",
				PlayerId, GameKey, YDp, TDp, YDr, TDr, YDc, TDc, FG, Pat);

			return string.Format(
			   "{0} in {1} projected: passing>{2,3}-({3})  running>{4,3}-({5:0.0})  catch>{6,3}-({7})  kick>{8}-{9}",
			   PlayerId, GameKey, ProjYDp, ProjTDp, ProjYDr, ProjTDr, ProjYDc, ProjTDc, ProjFG, ProjPat);
		}


		public string Season()
		{
			return GameKey.Substring(1, 4);
		}

		public string Week()
		{
			return GameKey.Substring(5, 2);
		}


		public bool HasNumbers()
		{
			var checkSum =
							  ProjYDp +
							  ProjTDp +
							  ProjYDr +
							  ProjTDr +
							  ProjYDc +
							  ProjTDc +
							  ProjFG +
							  ProjPat;
			return (checkSum > 0);
		}


		public decimal CalculateVariance()
		{
			return ProjectedFantasyPoints - FantasyPoints;
		}
	}
}
