﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL.DO
{
    public struct Drone
    {
        WeightCategory maxWeight;
        DroneStatus status;
        public int Id { get; set; }

        public string Model { get; set; }

        public WeightCategory MaxWeight
        {
            get
            {
                return maxWeight;
            }
            set
            {
                if ((int)value < 0 || (int)value > 2)
                    throw new ArgumentException("Invalid MaxWeight Value.");
                maxWeight = value;
            }
        }

        public DroneStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if ((int)value < 0 || (int)value > 2)
                    throw new ArgumentException("Invalid Status Value.");
                status = value;
            }
        }

        public double Battery { get; set; }

        /// <summary>
        /// Return a new drone with random values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Drone Random(int id)
        {
            return new Drone()
            {
                Id = id,
                Model = DAL.RandomManager.RandomName(),
                Status = (DroneStatus)DAL.RandomManager.RandomEnumOption(typeof(DroneStatus)),
                MaxWeight = (WeightCategory)DAL.RandomManager.RandomEnumOption(typeof(WeightCategory)),
                Battery = DAL.RandomManager.Rand.NextDouble() * 100,
            };
        }

        public override string ToString()
        {
            return (
                $"************************************\n"+
                $"Drone #{Id} information: \n" +
                $"Model  : {Model} \n" +
                $"Weight : {MaxWeight} \t " +
                $"Status : {Status} \n" +
                $"Battery: {Battery}%\n"+
                $"************************************\n" 
            );
        }
        
    }
}
