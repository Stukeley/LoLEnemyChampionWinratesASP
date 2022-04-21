using System.Diagnostics;
using LoLEnemyChampionWinratesASP.Exceptions;
using LoLEnemyChampionWinratesASP.Extensions;
using LoLEnemyChampionWinratesASP.Helpers;
using Microsoft.AspNetCore.Mvc;
using LoLEnemyChampionWinratesASP.Models;

namespace LoLEnemyChampionWinratesASP.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[HttpPost]
	public async Task<IActionResult> ParseGames(InputModel input)
	{
		var summonerNames = new [] {input.SummonerName};
		var region = input.SummonerRegion;
		var regionGeneral = input.SummonerRegionGeneral;
		
		var gameData = new ChampionWinLossStatCollection();

		// Step 1 - get PUUID of all accounts.
		// Step 2 - get match ids for all accounts, merge into one collection.
		// Step 3 - get match info for all match ids. <- Keep track of progress here.
		
		var puuids = new List<string>();

		foreach (var summonerName in summonerNames)
		{
			try
			{
				var puuid = await ApiController.GetAccountPuuid(summonerName, region);
				puuids.Add(puuid);
			}
			catch (ApiException ex)
			{
				_logger.Log(LogLevel.Error, ex, $"Error while parsing match list for Summoner {summonerName}");
			}
		}
		
		var matchIds = new List<string>();
		
		foreach (var puuid in puuids)
		{
			try
			{
				var matchList = await ApiController.GetMatchListInfo(puuid, regionGeneral);
				matchIds.AddRange(matchList);
			}
			catch (ApiException ex)
			{
				_logger.Log(LogLevel.Error, ex, $"Error while parsing match list for summoner of Puuid {puuid}");
			}
		}

		gameData.ExpectedGames = matchIds.Count;

		foreach (var matchId in matchIds)
		{
			try
			{
				var matchInfo = await ApiController.GetMatchInfo(matchId, regionGeneral);

				var partialDictionary = MatchInfoParser.ParseSingleGame(matchInfo, summonerNames);

				gameData.ChampionWinLossStats = gameData.ChampionWinLossStats.Merge(partialDictionary);

				gameData.ParsedGames++;
			}
			catch (ApiException ex)
			{
				_logger.Log(LogLevel.Error, ex, $"Error while parsing match of Id {matchId}");
			}
		}
		
		gameData.FixChampionNames();
		gameData.OrderDictionary();

		return View(gameData);
	}
}