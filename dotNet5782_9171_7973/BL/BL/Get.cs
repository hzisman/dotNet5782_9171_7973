﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public partial class BL
    {
        
        
        /// <summary>
        /// return specific parcel
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel with id</returns>
        public Parcel GetParcel(int id)
        {
            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), id);
            }

            return new Parcel()
            {
                Id = parcel.Id,
                Drone = parcel.DroneId.HasValue? GetDrone(parcel.DroneId.Value) : null,
                Sender = GetCustomerInDelivery(parcel.SenderId),
                Target = GetCustomerInDelivery(parcel.TargetId),
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Supplied = parcel.Supplied,
            };
        }
        

        /// <summary>
        /// return specific drone
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone with id</returns>
        
       
        /// <summary>
        /// return converted drone to drone for list
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone for list</returns>
        public DroneForList GetDroneForList(int id)
        {
            return GetDroneForListRef(id).Clone();
        }
        /// <summary>
        /// return converted parcel to parcel for list
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel for list</returns>
        public ParcelForList GetParcelForList(int id)
        {
            var parcel = GetParcel(id);

            return new ParcelForList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                SenderName = parcel.Sender.Name,
                TargetName = parcel.Target.Name,
            };
        }
        /// <summary>
        /// return converted drone to drone in charge
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone in charge</returns>
        public DroneInCharge GetDroneInCharge(int id)
        {
            return new DroneInCharge()
            {
                Id = id,
                BatteryState = GetDroneForList(id).Battery,
            };
        }
        /// <summary>
        /// return converted drone to drone in delivery
        /// </summary>
        /// <param name="id">id of requested drone</param>
        /// <returns>drone in delivery</returns>
        public DroneInDelivery GetDroneInDelivery(int id)
        {
            var drone = GetDrone(id);

            return new DroneInDelivery()
            {
                Id = id,
                BatteryState = drone.Battery,
                Location = drone.Location,
            };
        }
        
        
        /// <summary>
        /// return converted parcel to parcel at customer
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel at customer</returns>
        public ParcelAtCustomer GetParcelAtCustomer(int id)
        {
            var parcel = GetParcel(id);

            var state = parcel.Requested == null ? ParcelState.Associated
                        : parcel.Scheduled == null ? ParcelState.Defined
                        : parcel.PickedUp == null ? ParcelState.PickedUp
                        : ParcelState.Provided;

            return new ParcelAtCustomer()
            {
                Id = id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                OtherCustomer = parcel.Supplied != null? parcel.Sender: parcel.Target,
                State = state,
            };
        }
        /// <summary>
        /// return converted parcel to parcel in delivery
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel in delivery</returns>
        public ParcelInDeliver GetParcelInDeliver(int id)
        {
            DO.Parcel parcel;
            try
            {
                parcel = dal.GetById<DO.Parcel>(id);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Parcel), id);
            }

            DO.Customer targetCustomer;
            try
            {
                targetCustomer = dal.GetById<DO.Customer>(parcel.SenderId);  
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), parcel.SenderId);
            }

            DO.Customer senderCustomer;
            try
            {
                senderCustomer = dal.GetById<DO.Customer>(parcel.TargetId);
            }
            catch
            {
                throw new ObjectNotFoundException(typeof(Customer), parcel.TargetId);
            }

            var targetLocation = new Location() { Latitude = targetCustomer.Latitude, Longitude = targetCustomer.Longitude };
            var senderLocation = new Location() { Latitude = senderCustomer.Latitude, Longitude = senderCustomer.Longitude };

            return new ParcelInDeliver()
            {
                Id = id,
                Weight = (WeightCategory)parcel.Weight,
                Priority = (Priority)parcel.Priority,
                TargetLocation = targetLocation,
                CollectLocation = senderLocation,
                Position = parcel.PickedUp != null,
                DeliveryDistance = Location.Distance(senderLocation, targetLocation),
                Sender = GetCustomerInDelivery(senderCustomer.Id),
                Target = GetCustomerInDelivery(targetCustomer.Id),
            };
        }
    }
}
