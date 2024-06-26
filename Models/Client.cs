﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cars.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string MiddleName { get; set; }

    public string TelNumber { get; set; }

    public virtual ICollection<ClientCar> ClientCars { get; set; } = new List<ClientCar>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public override string ToString() => $"[{ClientId} : {SecondName} {FirstName}]";
}