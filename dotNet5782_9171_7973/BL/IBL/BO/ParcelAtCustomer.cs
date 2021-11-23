﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtCustomer : IDAL.DO.IIdentifiable
    {
        public int Id { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority Priority { get; set; }
        public ParcelState State { get; set; }
        public CustomerInDelivery OtherCustomer { get; set; }
        public override string ToString() => this.ToStringProps();


    }
}
