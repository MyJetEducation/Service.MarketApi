using Service.Market.Grpc.Models;
using Service.WalletApi.MarketApi.Controllers.Contracts;

namespace Service.WalletApi.MarketApi.Mappers
{
	public static class ProductMapper
	{
		public static ProductsResponse ToModel(this ProductGrpcModel model) => new ProductsResponse
		{
			Product = model.Product,
			Category = model.Category,
			Price = model.Price
		};
	}
}