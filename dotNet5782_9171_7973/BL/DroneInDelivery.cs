﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInDelivery
    {
        public int Id { get; set; }
        public double BatteryState { get; set; }
        public Location Location { get; set; }
    }
}