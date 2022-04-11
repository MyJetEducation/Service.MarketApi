using System.ComponentModel.DataAnnotations;
using Service.MarketProduct.Domain.Models;

namespace Service.WalletApi.MarketApi.Controllers.Contracts
{
	public class BuyProductRequest
	{
		[EnumDataType(typeof(MarketProductType))]
		public MarketProductType Product { get; set; }
	}
}