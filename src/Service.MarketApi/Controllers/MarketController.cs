using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Service.Grpc;
using Service.Market.Grpc;
using Service.Market.Grpc.Models;
using Service.MarketApi.Constants;
using Service.MarketApi.Mappers;
using Service.MarketApi.Models;
using Service.Web;

namespace Service.MarketApi.Controllers
{
	[Authorize]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[SwaggerResponse(HttpStatusCode.Unauthorized, null, Description = "Unauthorized")]
	[Route("/api/v1/market")]
	public class MarketController : ControllerBase
	{
		private readonly IGrpcServiceProxy<IMarketService> _marketService;

		public MarketController(IGrpcServiceProxy<IMarketService> marketService) => _marketService = marketService;

		[HttpPost("tokens")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<UserTokenAmountResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> GetTokenAmountAsync()
		{
			Guid? userId = GetUserId();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			TokenAmountGrpcResponse response = await _marketService.Service.GetTokenAmountAsync(new GetTokenAmountGrpcRequest
			{
				UserId = userId
			});

			return DataResponse<UserTokenAmountResponse>.Ok(new UserTokenAmountResponse
			{
				Value = (response?.Value).GetValueOrDefault()
			});
		}

		[HttpPost("products")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<ProductsResponse[]>), Description = "Ok")]
		public async ValueTask<IActionResult> GetProductsAsync()
		{
			Guid? userId = GetUserId();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			ProductGrpcResponse response = await _marketService.Service.GetProductsAsync(new GetProductsGrpcRequest
			{
				UserId = userId
			});

			ProductGrpcModel[] products = response?.Products;
			if (products == null || !products.Any())
				return StatusResponse.Error(ResponseCode.NoResponseData);

			return DataResponse<ProductsResponse[]>.Ok(products.Select(model => model.ToModel()).ToArray());
		}

		[HttpPost("buy")]
		[SwaggerResponse(HttpStatusCode.OK, typeof (DataResponse<StatusResponse>), Description = "Ok")]
		public async ValueTask<IActionResult> BuyProductAsync(BuyProductRequest request)
		{
			Guid? userId = GetUserId();
			if (userId == null)
				return StatusResponse.Error(ResponseCode.UserNotFound);

			BuyProductGrpcResponse response = await _marketService.TryCall(service => service.BuyProductAsync(new BuyProductGrpcRequest
			{
				UserId = userId,
				Product = request.Product
			}));

			if (response != null)
			{
				if (response.InsufficientAccount)
					return StatusResponse.Error(MarketResponseCodes.NotEnoughTokens);

				if (response.Successful)
					return StatusResponse.Ok();
			}

			return StatusResponse.Error();
		}

		protected Guid? GetUserId() => Guid.TryParse(User.Identity?.Name, out Guid uid) ? (Guid?) uid : null;
	}
}