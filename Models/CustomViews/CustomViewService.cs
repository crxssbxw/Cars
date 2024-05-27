using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Models.CustomViews
{
    public class CustomViewService
    {
        public int ServiceId { get; set; }

        public string? ServiceType { get; set; }

        public decimal Cost { get; set; }

        public ServiceCenter? ServiceCenter{ get; set; }

        public void ShallowCopy(Service service, ServiceCenterViewModel vm)
        {
            ServiceId = service.ServiceId;
            ServiceType = service.ServiceType;
            Cost = service.Cost;
            ServiceCenter = vm.ServiceCenters.Where(a => service.CenterId == a.CenterId).FirstOrDefault();
        }
        public void ShallowCopy(Service service, List<ServiceCenter> list)
        {
            ServiceId = service.ServiceId;
            ServiceType = service.ServiceType;
            Cost = service.Cost;
            ServiceCenter = list.Where(a => service.CenterId == a.CenterId).FirstOrDefault();
        }
    }
}
