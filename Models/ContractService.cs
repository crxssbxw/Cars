﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cars.Models;

public partial class ContractService
{
    public int ContractServiceId { get; set; }

    public int ContractId { get; set; }

    public int ServiceId { get; set; }

    public virtual Contract Contract { get; set; }

    public virtual Service Service { get; set; }
}