using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Models.CustomViews
{
    public class CustomViewCraftsman
    {
        public int CraftsmanId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public int Expirience { get; set; }

        public ServiceCenter ServiceCenter { get; set; }

        public void ShallowCopy(Craftsman craftsman, ServiceCenterViewModel serviceCenterViewModel)
        {
            CraftsmanId = craftsman.CraftsmanId;
            FirstName = craftsman.FirstName;
            SecondName = craftsman.SecondName;
            MiddleName = craftsman.MiddleName;
            Expirience = craftsman.Expirience;
            ServiceCenter = serviceCenterViewModel.ServiceCenters.Where(obj => obj.CenterId == craftsman.CenterId).FirstOrDefault();
        }
        public void ShallowCopy(Craftsman craftsman, List<ServiceCenter> list)
        {
            CraftsmanId = craftsman.CraftsmanId;
            FirstName = craftsman.FirstName;
            SecondName = craftsman.SecondName;
            MiddleName = craftsman.MiddleName;
            Expirience = craftsman.Expirience;
            ServiceCenter = list.Where(obj => obj.CenterId == craftsman.CenterId).FirstOrDefault();
        }
    }
}
