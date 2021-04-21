using Microsoft.AspNet.SignalR;
using OffWork.Models;

namespace OffWork.Hubs
{
    public class MyHub1 : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void SendUpdatedAbsenceDetail(AbsenceDetail absenceDetail)
        {
            Clients.All.SendUpdatedAbsenceDetail(absenceDetail);
        }
    }
}