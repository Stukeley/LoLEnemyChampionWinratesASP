using LoLEnemyChampionWinratesASP.Models;
using Newtonsoft.Json;

namespace LoLEnemyChampionWinratesASP.Controllers;

public static class ApiController
{
	private const string API_KEY = "REDACTED";

	private static int _requestCount;
	public static int RequestCount
	{
		get
		{
			return _requestCount;
		}
		set
		{
			_requestCount = value;
			
			if (_requestCount == 100)
			{
				Console.WriteLine("Waiting 2 minutes...");
				Thread.Sleep(120000);
				_requestCount = 0;
			}
			else if (_requestCount % 20 == 0)
			{
				Console.WriteLine("Waiting 1 second...");
				Thread.Sleep(1000);
			}
		}
	}

	public static async Task<string> GetAccountPuuid(string summonerName, string region)
	{
		var url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}?api_key={API_KEY}";
		
		var client = new HttpClient();
		var response = await client.GetAsync(url);

		RequestCount++;

		if (response.IsSuccessStatusCode)
		{
			string responseBody = await response.Content.ReadAsStringAsync();
			
			var parsed = JsonConvert.DeserializeObject<AccountInfo>(responseBody);

			return parsed.Puuid;
		}
		else
		{
			return null;
		}
	}

	public static async Task<List<string>> GetMatchListInfo(string puuid, string regionGeneral)
	{
		var url = $"https://{regionGeneral}.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}?api_key={API_KEY}";
		
		var client = new HttpClient();
		var response = await client.GetAsync(url);

		RequestCount++;

		if (response.IsSuccessStatusCode)
		{
			string responseBody = await response.Content.ReadAsStringAsync();
			
			var parsed = JsonConvert.DeserializeObject<List<string>>(responseBody);

			return parsed;
		}
		else
		{
			return null;
		}
	}

	public static async Task<MatchInfo> GetMatchInfo(string gameId, string regionGeneral)
	{
		var url = $"https://{regionGeneral}.api.riotgames.com/lol/match/v5/matches/{gameId}?api_key={API_KEY}";
		var client = new HttpClient();
		var response = await client.GetAsync(url);

		RequestCount++;

		if (response.IsSuccessStatusCode)
		{
			string responseBody = await response.Content.ReadAsStringAsync();
			
			var parsed = JsonConvert.DeserializeObject<MatchInfo>(responseBody);

			return parsed;
		}
		else
		{
			return null;
		}
	}
}