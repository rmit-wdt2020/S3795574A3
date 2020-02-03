using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using S3795574A2.Data;
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

        public BillPayService(IServiceScopeFactory scopeFactory)
        {
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
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NwbaContext>();
            var billPayManager = new BillPayManager(dbContext);

            while (true)
            {

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
