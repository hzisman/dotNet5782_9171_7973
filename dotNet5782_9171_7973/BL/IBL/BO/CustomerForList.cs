﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringUtilities;

namespace IBL.BO
{
    public class CustomerForList 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelsSendAndSupplied { get; set; }
        public int ParcelsSendAndNotSupplied { get; set; }
        public int ParcelsRecieved { get; set; }
        public int ParcelsOnWay { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
