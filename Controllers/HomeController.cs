using System.Diagnostics;
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

	public async Task<IActionResult> ParseGames()
	{
		var summonerNames = new [] {"Borpa", "Milkshake"};
		var region = "eun1";
		var regionGeneral = "europe";
		
		var gameData = new Dictionary<string, WinLossStat>();

		foreach (var summonerName in summonerNames)
		{
			var puuid = await ApiController.GetAccountPuuid(summonerName, region);

			var matchList = await ApiController.GetMatchListInfo(puuid, regionGeneral);
			
			foreach (var matchId in matchList)
			{
				var matchInfo = await ApiController.GetMatchInfo(matchId, regionGeneral);

				var partialDictionary = MatchInfoParser.ParseSingleGame(matchInfo, summonerNames);

				gameData = gameData.Merge(partialDictionary);
			}
		}

		return View(gameData);
	}
}