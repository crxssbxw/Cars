using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Models.CustomViews
{
    public class CustomViewCar
    {
        public int CarId { get; set; }

        public string Serial { get; set; }

        public string CarNum { get; set; }

        public string Manufacturer { get; set; }

        public int EnginePower { get; set; }

        public int PassengersAmount { get; set; }

        public double Weight { get; set; }

        public string Color { get; set; }

        public string BodyType { get; set; }

        public bool Conditioner { get; set; }

        public List<Models.Client> Owners { get; set; } = new();

        public List<Client> Clients { get; set; } = new();

        public void ShallowCopy(Car car)
        {
            CarId = car.CarId;
            Serial = car.Serial;
            CarNum = car.CarNum;
            Manufacturer = car.Manufacturer;
            EnginePower = car.EnginePower;
            PassengersAmount = car.PassengersAmount;
            Weight = car.Weight;
            Color = car.Color;
            BodyType = car.BodyType;
            Conditioner = car.Conditioner;
            var clients = from c in App.Context.Clients
                          join cc in App.Context.ClientCars on c.ClientId equals cc.ClientId
                          join cs in App.Context.Cars on cc.CarId equals cs.CarId
                          where cc.CarId == CarId
                          select c;
            foreach (var client in clients) 
            {
                Owners.Add((Client)client);
            }
        }
    }
}
