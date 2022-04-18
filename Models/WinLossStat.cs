namespace LoLEnemyChampionWinratesASP.Models;

public class WinLossStat
{
	public int Wins { get; set; }
	public int Losses { get; set; }
	
	public double Winrate => (double)Wins / (Wins + Losses);
	
	public string WinrateString => Math.Round(Winrate * 100, 2) + "%";
	
	public WinLossStat(int wins = 0, int losses = 0)
	{
		Wins = wins;
		Losses = losses;
	}
}