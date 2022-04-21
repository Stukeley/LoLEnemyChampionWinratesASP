namespace LoLEnemyChampionWinratesASP.Models;

public class InputModel
{
	public string SummonerName { get; set; }
	
	public string SummonerRegion { get; set; }

	public string SummonerRegionGeneral => SummonerRegion switch
	{
		"EUNE" or "EUW" or "RU" => "europe",
		"NA" or "LAS" or "LAN" or "BR" => "americas",
		"KR" or "JP" or "TR" => "asia",
		_ => "europe"
	};
	
	public InputModel()
	{
		SummonerName = "";
		SummonerRegion = "";
	}
}