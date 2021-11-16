﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBLDrone
    {
        void AddDrone(int id, string model, WeightCategory maxWeight, int stationId);
        void RenameDrone(int droneId, string newName);
        void SendDroneToCharge(int droneId);
        void FinishCharging(int droneId, double timeInCharge);
    }
}