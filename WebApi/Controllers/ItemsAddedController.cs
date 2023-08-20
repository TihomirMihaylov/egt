using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("itemsAdded")]
    public class ItemsAddedController : ApiBaseController
    {
        private readonly IMessageService m_MessageService;

        public ItemsAddedController(IMessageService messageService)
        {
            m_MessageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
        }

        public async Task<BaseResponse> Post()
        {
            var result = await m_MessageService.NotifyConsumers(Cts.Token);

            return new BaseResponse
            {
                IsSuccess = result
            };
        }
    }
}
