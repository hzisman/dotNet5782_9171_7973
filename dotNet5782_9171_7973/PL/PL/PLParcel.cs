﻿using System.Collections.Generic;
using PO;

namespace PL
{
    static partial class PL
    {
        /// <summary>
        /// Adds a parcel
        /// </summary>
        /// <param name="parcel">The parcel to add</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void AddParcel(ParcelToAdd parcel)
        {
            bl.AddParcel((int)parcel.SenderId,
                         (int)parcel.TargetId,
                         (BO.WeightCategory)parcel.Weight,
                         (BO.Priority)parcel.Priority);

            ParcelsNotification.NotifyParcelChanged();
        }

        /// <summary>
        /// Returns a specific parcel
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel with id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static Parcel GetParcel(int id);

        /// <summary>
        /// Returns the parcels list
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> parcels list</returns>
        public static IEnumerable<ParcelForList> GetParcelsList()
        {
            List<ParcelForList> parcelsList = new();

            foreach (var parcel in bl.GetParcelsList())
            {
                parcelsList.Add(ConvertParcel(parcel));
            }

            return parcelsList;
        }

        /// <summary>
        /// Returns a list of parcels which weren't assigned to drone yet
        /// </summary>
        /// <returns>list of parcels which weren't assigned to drone yet</returns>
        public static IEnumerable<ParcelForList> GetNotAssignedToDroneParcels()
        {
            List<ParcelForList> parcelsList = new();

            foreach (var parcel in bl.GetNotAssignedToDroneParcels())
            {
                parcelsList.Add(ConvertParcel(parcel));
            }

            return parcelsList;
        }

        /// <summary>
        /// Picks a parcel up by a drone
        /// </summary>
        /// <param name="droneId">The drone id</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        public static void PickUpParcel(int parcelId)
        {
            bl.PickUpParcel(parcelId);
            ParcelsNotification.NotifyParcelChanged();
            DronesNotification.NotifyDroneChanged();
        }

        /// <summary>
        /// Supply a parcel by a drone
        /// </summary>
        /// <param name="droneId">The drone Id</param>
        /// <exception cref="ObjectNotFoundException" />
        /// <exception cref="InvalidActionException" />
        public static void SupplyParcel(int droneId)
        {
            bl.SupplyParcel(droneId);
            ParcelsNotification.NotifyParcelChanged();
            DronesNotification.NotifyDroneChanged();
        }

        /// <summary>
        /// Deletes a parcel
        /// </summary>
        /// <param name="parcelId">The parcel Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void DeleteParcel(int parcelId)
        {
            bl.DeleteParcel(parcelId);
            ParcelsNotification.NotifyParcelChanged();
        }

        private static ParcelForList ConvertParcel(BO.ParcelForList boParcel)
        {
            return new ParcelForList()
            {
                Id = boParcel.Id,
                Priority = (Priority)boParcel.Priority,
                Weight = (WeightCategory)boParcel.Weight,
                SenderName = boParcel.SenderName,
                TargetName = boParcel.TargetName,
            };
        }
    }
}