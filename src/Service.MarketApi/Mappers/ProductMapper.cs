using Service.Market.Grpc.Models;
using Service.MarketApi.Models;

namespace Service.MarketApi.Mappers
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