using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApiBaseController : ControllerBase, IDisposable
    {
        protected readonly CancellationTokenSource Cts;

        public ApiBaseController()
        {
            Cts = new CancellationTokenSource();
        }

        public void Dispose()
        {
            Cts.Cancel();
            Cts.Dispose();
        }
    }
}
