﻿using System.Collections.Generic;
using PO;

namespace PL
{
    static partial class PL
    {
        /// <summary>
        /// add customer
        /// </summary>
        /// <param name="customer">The customer to add</param>
        /// <exception cref="IdAlreadyExistsException" />
        /// <exception cref="InvalidPropertyValueException" />
        public static void AddCustomer(CustomerToAdd customer)
        {
            bl.AddCustomer((int)customer.Id,
                           customer.Name,
                           customer.Phone,
                           (double)customer.Location.Longitude,
                           (double)customer.Location.Latitude);

            CustomersNotification.NotifyCustomerChanged();
        }

        /// <summary>
        /// return specific customer
        /// </summary>
        /// <param name="id">Id of requested customer</param>
        /// <returns>The <see cref="Customer"/> who has the spesific Id</returns>
        /// <exception cref="ObjectNotFoundException" />
        public static Customer GetCustomer(int id);

        /// <summary>
        /// return customers list
        /// </summary>
        /// <returns>customers list</returns>
        public static IEnumerable<CustomerForList> GetCustomersList()
        {
            List<CustomerForList> customersList = new();

            foreach (var customer in bl.GetCustomersList())
            {
                customersList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    ParcelsOnWay = customer.ParcelsOnWay,
                    ParcelsRecieved = customer.ParcelsRecieved,
                    ParcelsSendAndNotSupplied = customer.ParcelsSendAndNotSupplied,
                    ParcelsSendAndSupplied = customer.ParcelsSendAndSupplied,
                });
            }

            return customersList;
        }

        /// <summary>
        /// update customer's details
        /// </summary>
        /// <param name="customerId">customer to update</param>
        /// <param name="name">new name</param>
        /// <param name="phone">new phone</param>
        public static void UpdateCustomer(int customerId, string name = null, string phone = null)
        {
            bl.UpdateCustomer(customerId, name, phone);
            CustomersNotification.NotifyCustomerChanged();
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customerId">The customer Id</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public static void DeleteCustomer(int customerId)
        {
            bl.DeleteCustomer(customerId);
            CustomersNotification.NotifyCustomerChanged();
        }
    }
}