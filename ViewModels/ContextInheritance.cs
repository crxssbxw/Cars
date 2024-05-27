using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.ViewModels
{
    public class ContextInheritance
    {
        protected CarsContext context = App.Context;
    }
}
