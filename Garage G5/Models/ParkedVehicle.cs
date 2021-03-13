﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage_G5.Models
{

    public class ParkedVehicle
    {
        [Key]
        private DateTime? dateCreated;

        public int Id { get; set; }
        [Display(Name = "Type")]
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Registration")]
        [Remote("IsRegisterNumberExists", "ParkedVehicles", ErrorMessage = "Registration Number already in use", AdditionalFields = "Id")]
        public string RegistrationNum { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        [Display(Name = "Wheels")]
        public int WheelsNum { get; set; }

        [Editable(false)]
        [Display(Name = "Arrival")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnteringTime
        {
            
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }

        }



    }
}
