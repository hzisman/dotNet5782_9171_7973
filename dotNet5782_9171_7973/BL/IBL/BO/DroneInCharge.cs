﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class DroneInCharge
    {
        public int Id { get; set; }
        public double BatteryState { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
