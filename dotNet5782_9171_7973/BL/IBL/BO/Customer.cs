﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer: IDAL.DO.IIdentifiable, ILocalable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<Parcel> Send { get; set; }
        public List<Parcel> Recieve { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}
