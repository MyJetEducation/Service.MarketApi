using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.MarketApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("MarketApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("MarketApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("MarketApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("MarketApi.MarketServiceUrl")]
		public string MarketServiceUrl { get; set; }
	}
}
