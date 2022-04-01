using Autofac;
using Microsoft.Extensions.Logging;
using Service.Market.Client;

namespace Service.MarketApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterMarketClient(Program.Settings.MarketServiceUrl, Program.LogFactory.CreateLogger(typeof (MarketClientFactory)));
		}
	}
}