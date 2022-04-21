using LoLEnemyChampionWinratesASP.Helpers;

namespace LoLEnemyChampionWinratesASP.Models;

public class ChampionWinLossStatCollection
{
	public Dictionary<string, WinLossStat> ChampionWinLossStats { get; set; }
	
	public int ExpectedGames { get; set; }
	
	public int ParsedGames { get; set; }
	
	public bool IsComplete => ExpectedGames == ParsedGames;
	
	public ChampionWinLossStatCollection(int expectedGames = 0)
	{
		ChampionWinLossStats = new Dictionary<string, WinLossStat>();
		ExpectedGames = expectedGames;
	}

	public void OrderDictionary()
	{
		ChampionWinLossStats = ChampionWinLossStats.OrderByDescending(x => x.Value.Winrate)
			.ThenByDescending(y=>y.Value.Wins + y.Value.Losses)
			.ToDictionary(x => x.Key, x => x.Value);
	}

	public void FixChampionNames()
	{
		ChampionWinLossStats = ChampionNameHelper.RemakeDictionaryWithProperChampionNames(ChampionWinLossStats);
	}
}