using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using S3795574A2.Data;
using S3795574A2.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
/*
 * The code below references from https://forums.asp.net/t/2147747.aspx?Send+email+after+a+few+days+in+Asp+net+Core
 */
namespace S3795574A2
{
    public class BillPayService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IHttpContextAccessor accessor;
        public BillPayService(IServiceScopeFactory scopeFactory, IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
            this.scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(TaskRoutine, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }

        public Task TaskRoutine()
        {
           
            

            while (true)
            {
                //if(accessor.HttpContext.Session.GetInt32(nameof(Customer.CustomerID)) == null)
                //    accessor.HttpContext.Response.Redirect("https://localhost:44380/Nwba/SecureLogin/LogoutNow");
                using var scope = scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<NwbaContext>();
                var billPayManager = new BillPayManager(dbContext);
                _ = billPayManager.Run();
                //Run this loop every 5 second
                DateTime nextStop = DateTime.Now.AddSeconds(5);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }
    }
}
