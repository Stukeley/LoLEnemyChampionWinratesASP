// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using LoLEnemyChampionWinratesASP.Models;
//
//    var accountInfo = AccountInfo.FromJson(jsonString);

namespace LoLEnemyChampionWinratesASP.Models
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class AccountInfo
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("accountId")]
		public string AccountId { get; set; }

		[JsonProperty("puuid")]
		public string Puuid { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("profileIconId")]
		public long ProfileIconId { get; set; }

		[JsonProperty("revisionDate")]
		public long RevisionDate { get; set; }

		[JsonProperty("summonerLevel")]
		public long SummonerLevel { get; set; }
	}

	public partial class AccountInfo
	{
		public static AccountInfo FromJson(string json) => JsonConvert.DeserializeObject<AccountInfo>(json, LoLEnemyChampionWinratesASP.Helpers.Converter.Settings);
	}

	public static partial class Serialize
	{
		public static string ToJson(this AccountInfo self) => JsonConvert.SerializeObject(self, LoLEnemyChampionWinratesASP.Helpers.Converter.Settings);
	}
}