﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cars.Models;

public partial class ClientCar
{
    public int ClientCarId { get; set; }

    public int ClientId { get; set; }

    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public virtual Client Client { get; set; }
}