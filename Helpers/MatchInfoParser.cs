using LoLEnemyChampionWinratesASP.Models;

namespace LoLEnemyChampionWinratesASP.Helpers;

public class MatchInfoParser
{
	public static Dictionary<string, WinLossStat> ParseSingleGame(MatchInfo matchInfo, string[] summonerNames)
	{
		var champions = new Dictionary<string, WinLossStat>();
		
		var summoner = matchInfo.Info.Participants.First(x => summonerNames.Contains(x.SummonerName));
			
		var summonerTeamId = summoner.TeamId;

		var enemyTeamParticipants = matchInfo.Info.Participants.Where(x => x.TeamId != summonerTeamId);

		var enemyTeamChampionNames = enemyTeamParticipants.Select(x => x.ChampionName);

		bool didSummonerWin = summoner.Win;

		foreach (var championName in enemyTeamChampionNames)
		{
			if (champions.ContainsKey(championName))
			{
				if (didSummonerWin)
				{
					champions[championName].Losses++;
				}
				else
				{
					champions[championName].Wins++;
				}
			}
			else
			{
				champions.Add(championName, didSummonerWin ? new WinLossStat(0, 1) : new WinLossStat(1, 0));
			}
		}

		return champions;
	}
}