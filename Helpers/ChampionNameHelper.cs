using LoLEnemyChampionWinratesASP.Models;

namespace LoLEnemyChampionWinratesASP.Helpers;

public static class ChampionNameHelper
{
	public static Dictionary<string, WinLossStat> RemakeDictionaryWithProperChampionNames(Dictionary<string, WinLossStat> championStats)
	{
		var fixedChampionStats = new Dictionary<string, WinLossStat>();
		
		foreach (var championStat in championStats)
		{
			string fixedName = FixChampionName(championStat.Key);
			fixedChampionStats.Add(fixedName, championStat.Value);
		}

		return fixedChampionStats;
	}

	public static string FixChampionName(string name)
	{
		string fixedName;
		
		// 1. Replace MonkeyKing with Wukong
		if (name.Equals("MonkeyKing", StringComparison.OrdinalIgnoreCase))
		{
			fixedName = "Wukong";
			return fixedName;
		}
		
		// 2. Replace FiddleSticks with Fiddlesticks
		if (name.Equals("FiddleSticks", StringComparison.OrdinalIgnoreCase))
		{
			fixedName = "Fiddlesticks";
			return fixedName;
		}
		
		// 3. Replace JarvanIV with Jarvan IV
		if (name.Equals("JarvanIV", StringComparison.OrdinalIgnoreCase))
		{
			fixedName = "Jarvan IV";
			return fixedName;
		}
		
		// 4. Add a space between capital letters.
		fixedName = string.Concat(name.Select(c => char.IsUpper(c) ? " " + c : c.ToString())).TrimStart();
		
		// 5. In case of Dr Mundo, add a dot.
		if (fixedName == "Dr Mundo")
		{
			fixedName = "Dr. Mundo";
			return fixedName;
		}
		
		// 6. In case of void champions, add an apostrophe.
		if (fixedName.StartsWith("Kai") || fixedName.StartsWith("Kha") || fixedName.StartsWith("Kog") || fixedName.StartsWith("Rek"))
		{
			fixedName = name.Substring(0,3) + "'" + name.Substring(3);
		}

		return fixedName;
	}
}