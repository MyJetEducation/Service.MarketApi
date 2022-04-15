using System.ComponentModel.DataAnnotations;
using Service.MarketProduct.Domain.Models;

namespace Service.WalletApi.MarketApi.Controllers.Contracts
{
	public class ProductsResponse
	{
		[EnumDataType(typeof(MarketProductType))]
		public MarketProductType Product { get; set; }

		[EnumDataType(typeof(MarketProductCategory))]
		public MarketProductCategory Category { get; set; }

		public decimal? Price { get; set; }

		public int Priority { get; set; }
	}
}