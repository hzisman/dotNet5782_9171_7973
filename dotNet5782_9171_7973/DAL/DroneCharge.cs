﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct DroneCharge
    {
        public int StationId { get; set; }

        public int DroneId { get; set; }

        public override string ToString()
        {
            return (
                $"*********************************************\n" +
                $"Charges drone #{DroneId} in station #{StationId}\n" +
                $"*********************************************"
            );
        }
    }
}
