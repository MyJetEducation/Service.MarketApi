using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.WalletApi.MarketApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("WalletApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("WalletApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("WalletApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("WalletApi.EnableApiTrace")]
		public bool EnableApiTrace { get; set; }

		[YamlProperty("WalletApi.MyNoSqlReaderHostPort")]
		public string MyNoSqlReaderHostPort { get; set; }

		[YamlProperty("WalletApi.AuthMyNoSqlReaderHostPort")]
		public string AuthMyNoSqlReaderHostPort { get; set; }

		[YamlProperty("WalletApi.SessionEncryptionKeyId")]
		public string SessionEncryptionKeyId { get; set; }

		[YamlProperty("WalletApi.MyNoSqlWriterUrl")]
		public string MyNoSqlWriterUrl { get; set; }

		[YamlProperty("WalletApi.MarketServiceUrl")]
		public string MarketServiceUrl { get; set; }
	}
}
