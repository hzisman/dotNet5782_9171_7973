﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInDeliver
    {
        public int Id { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public bool Position { get; set; }
        public Location Collect { get; set; }
        public Location Target { get; set; }
        public double DeliveryDistance { get; set; }
       

    }
}