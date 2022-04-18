using LoLEnemyChampionWinratesASP.Models;

namespace LoLEnemyChampionWinratesASP.Extensions;

public static class DictionaryExtensions
{
	public static Dictionary<string, WinLossStat> Merge(this Dictionary<string, WinLossStat> dict, Dictionary<string, WinLossStat> other)
	{
		foreach (var item in other)
		{
			if (dict.ContainsKey(item.Key))
			{
				dict[item.Key].Wins += item.Value.Wins;
				dict[item.Key].Losses += item.Value.Losses;
			}
			else
			{
				dict.Add(item.Key, item.Value);
			}
		}
		return dict;
	}
}