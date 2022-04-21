namespace LoLEnemyChampionWinratesASP.Helpers;

public static class ApiKeyHelper
{
	public static string GetApiKey()
	{
		var path = Path.Combine(Environment.CurrentDirectory, "Resources", "ApiKey.txt");
		var key = File.ReadAllText(path);

		return key;
	}
}