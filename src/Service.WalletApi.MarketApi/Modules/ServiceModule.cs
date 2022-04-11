using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.ApiSecurityManager.Autofac;
using MyJetWallet.Sdk.RestApiTrace;
using Service.Market.Client;

namespace Service.WalletApi.MarketApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterEncryptionServiceClient();

			builder.RegisterMarketClient(Program.Settings.MarketServiceUrl, Program.LoggerFactory.CreateLogger(typeof (MarketClientFactory)));

			if (Program.Settings.EnableApiTrace)
			{
				builder
					.RegisterInstance(new ApiTraceManager(Program.Settings.ElkLogs, "api-trace",
						Program.LoggerFactory.CreateLogger("ApiTraceManager")))
					.As<IApiTraceManager>()
					.As<IStartable>()
					.AutoActivate()
					.SingleInstance();
			}
		}
	}
}