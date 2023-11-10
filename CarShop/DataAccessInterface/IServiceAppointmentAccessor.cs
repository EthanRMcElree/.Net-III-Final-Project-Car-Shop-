﻿using DataObjectsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IServiceAppointmentAccessor
    {
        int CreateNewServiceAppointment(int CarID, int CustomerID, int ServiceTypeID);
        void UpdateServiceAppointment(int AppointmentID, int CarID, int CustomerID, int ServiceTypeID, int SupplierID, DateTime ScheduledDate);
        ServiceAppointmentVM RetrieveServiceAppointmentByAppointmentID(int AppointmentID);

        void DeleteServiceAppointmentByAppointmentID(int AppointmentID);
    }
}
