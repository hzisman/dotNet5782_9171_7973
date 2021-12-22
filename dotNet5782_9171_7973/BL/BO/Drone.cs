﻿using StringUtilities;

namespace BO
{
    /// <summary>
    /// A class to represent a PDS of drone
    /// </summary>
    public class Drone : ILocalable
    {
        /// <summary>
        /// Drone Id
        /// </summary>
        public int Id { get; set; }

        string model;
        /// <summary>
        /// Drone model
        /// </summary>
        public string Model
        {
            get => model;
            set
            {
                if (!Validation.IsValidName(value))
                {
                    throw new InvalidPropertyValueException(nameof(Model), value);
                }
                model = value;
            }
        }

        WeightCategory maxWeight;
        /// <summary>
        /// Category of max weight drone can carry
        /// </summary>
        public WeightCategory MaxWeight 
        {
            get => maxWeight;
            set
            {
                if(!Validation.IsValidEnumOption<WeightCategory>((int)value))
                {
                    throw new InvalidPropertyValueException(nameof(MaxWeight), value.ToString());
                }
                maxWeight = value;
            }
        }

        double battery;
        /// <summary>
        /// Drone battery 
        /// (in parcents)
        /// </summary>
        public double Battery 
        {
            get => battery;
            set
            {
                if(value < 0) 
                {
                    throw new InvalidPropertyValueException(nameof(Battery), value);
                }
                battery = value;
            }
        }

        DroneState state;
        /// <summary>
        /// Drone state
        /// </summary>
        public DroneState State
        {
            get => state;
            set
            {
                if (!Validation.IsValidEnumOption<DroneState>((int)value))
                {
                    throw new InvalidPropertyValueException(nameof(State), value.ToString());
                }
                state = value;
            }
        }

        /// <summary>
        /// Drone's related parcel
        /// (parcel drone delivers)
        /// </summary>
        public ParcelInDeliver ParcelInDeliver { get; set; }

        /// <summary>
        /// Drone location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Uses an outer project <see cref="StringUtilities"/>
        /// to override the <code>ToString()</code> method
        /// </summary>
        /// <returns>String representation of customer</returns>
        public override string ToString() => this.ToStringProperties();
    }
}
