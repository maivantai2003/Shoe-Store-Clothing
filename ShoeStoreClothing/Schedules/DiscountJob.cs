using Microsoft.AspNetCore.SignalR;
using Quartz;
using ShoeStoreClothing.Hubs;
namespace ShoeStoreClothing.Schedules
{
    public class DiscountJob:IJob
    {
        private readonly IHubContext<myHub> _hubContext;
        public DiscountJob(IHubContext<myHub> hubContext)
        {
            _hubContext=hubContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //var date = _context.Discounts.FirstOrDefault(x => x.DiscountId == 1);
            //if (date != null && (DateTime.Now - date.DiscountDate).TotalMinutes < 1)
            //{
            //    await _hubContext.Clients.All.SendAsync("DiscountScheduled", "Scheduled message from Quartz.NET!");
            //}
            Console.WriteLine("DiscountJob executed at: " + DateTime.Now);
            await _hubContext.Clients.All.SendAsync("DiscountScheduled", "Scheduled message from Quartz.NET!");
        }
    }
}