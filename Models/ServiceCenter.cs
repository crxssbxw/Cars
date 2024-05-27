﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cars.Models;

public partial class ServiceCenter
{
    public int CenterId { get; set; }

    public string FirmName { get; set; }

    public string CenterName { get; set; }

    public int CarsAmount { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string Building { get; set; }

    public virtual ICollection<Craftsman> Craftsmen { get; set; } = new List<Craftsman>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public override string ToString() => $"[{CenterId}] : {FirmName} {CenterName} ({City})";
}