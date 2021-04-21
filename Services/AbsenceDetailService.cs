using Microsoft.AspNet.SignalR;
using OffWork.Hubs;
using OffWork.Models;

namespace OffWork.Services
{
    public class AbsenceDetailService
    {
        public void NotifyUpdates(AbsenceDetail absenceDetail)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub1>();
            if (hubContext != null)
            {
                hubContext.Clients.All.SendUpdatedAbsenceDetail(absenceDetail);
            }
        }
    }
}