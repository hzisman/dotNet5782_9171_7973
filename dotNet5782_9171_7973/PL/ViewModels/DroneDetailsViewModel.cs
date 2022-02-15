﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using PO;

namespace PL.ViewModels
{
    class DroneDetailsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The drone 
        /// </summary>
        public Drone Drone { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// List of markers to display on drone's map
        /// </summary>
        public ObservableCollection<MapMarker> Markers { get; set; } = new();

        /// <summary>
        /// Proceed with related parcel to the next step in delivery command
        /// </summary>
        public RelayCommand ProceedWithParcelCommand { get; set; }

        /// <summary>
        /// Send to charge or release drone from charging command
        /// </summary>
        public RelayCommand HandleChargeCommand { get; set; }

        /// <summary>
        /// Rename drone command
        /// </summary>
        public RelayCommand RenameDroneCommand { get; set; }

        /// <summary>
        /// Delete drone command
        /// </summary>
        public RelayCommand DeleteCommand { get; set; }

        /// <summary>
        /// Open panel of related parcel's details command
        /// </summary>
        public RelayCommand ViewParcelCommand { get; set; }

        public DroneDetailsViewModel(int id)
        {
            Drone.Id = id;
            LoadDrone();

            PLNotification.DroneNotification.AddHandler(LoadDrone, id);

            ProceedWithParcelCommand = new(ProceedWithParcel, () => Drone.State != DroneState.Maintenance);
            HandleChargeCommand = new(HandleCharge, () => Drone.State != DroneState.Deliver);
            RenameDroneCommand = new(RenameDrone, () => Drone.Error == null);
            DeleteCommand = new(Delete, () => Drone.State == DroneState.Free);
            ViewParcelCommand = new(() => Workspace.AddPanelCommand.Execute(Workspace.ParcelPanel(Drone.ParcelInDeliver?.Id)),
                                    () => Drone.ParcelInDeliver != null);
        }

        /// <summary>
        /// Proceed with related parcel to the next step in delivery
        /// </summary>
        private void ProceedWithParcel()
        {
            //drone's state is Free
            //step: assign parcel to drone
            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.AssignParcelToDrone(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
            //drone's state is Deliver
            //(we do not get to this function with Maintanance state
            //since then drone does not have a related parcel)
            else
            {
                //Parcel was not picked up yet
                if (!Drone.ParcelInDeliver.WasPickedUp)
                {
                    PLService.PickUpParcel(Drone.Id);
                }
                else
                {
                    try
                    {
                        PLService.SupplyParcel(Drone.Id);
                    }
                    catch (BO.InvalidActionException e)
                    {
                        MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                    }
                }
            }
        }

        private void HandleCharge()
        {

            if (Drone.State == DroneState.Free)
            {
                try
                {
                    PLService.ChargeDrone(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
            else if (Drone.State == DroneState.Maintenance)
            {
                try
                {
                    PLService.FinishCharging(Drone.Id);
                }
                catch (BO.InvalidActionException e)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, e.Message);
                }
            }
        }

        private void RenameDrone()
        {
            PLService.RenameDrone(Drone.Id, Drone.Model);
            MessageBox.Show(MessageBox.BoxType.Success, $"Drone {Drone.Id} renamed succesfully to {Drone.Model}");
        }

        private void Delete()
        {
            PLService.DeleteDrone(Drone.Id);
            Workspace.RemovePanelCommand.Execute(Workspace.DronePanelName(Drone.Id));
        }

        private void LoadDrone()
        {
            Drone.Reload(PLService.GetDrone(Drone.Id));

            Markers.Clear();
            Markers.Add(MapMarker.FromType(Drone));
        }
    }
}
