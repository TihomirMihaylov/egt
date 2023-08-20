using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("addItems")]
    public class AddItemsToRedisController : ApiBaseController
    {
        private readonly ICacheService m_CacheService;

        public AddItemsToRedisController(ICacheService cacheService)
        {
            m_CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<BaseResponse> Post(AddItemsRequest request)
        {
            var result = await m_CacheService.AddItemsToRedis(request.Items, Cts.Token);

            return new BaseResponse
            {
                IsSuccess = result
            };
        }
    }
}
