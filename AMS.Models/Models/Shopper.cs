﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class Shopper
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Current Budget is required")]
        [Display(Name = "Current Budget")]
        public double CurrentBudget { get; set; }

        public double Rating { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

       // [Required(ErrorMessage = "Longitude is required")]
        [Display(Name = "LicenseFilePath")]
        public string LicenseFilePath { get; set; }

        //[Required(ErrorMessage = "ImageUrl is required")]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "MotorType is required")]
        [Display(Name = "MotorType")]
        public int MotorTypeId { get; set; }
        [Required(ErrorMessage = "MototBrand is required")]
        [Display(Name = "MototBrand")]
        public int MotorBrandId { get; set; }
        [Required(ErrorMessage = "MotorYear is required")]
        [Display(Name = "MotorYear")]
        public string MotorYear { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public DateTime CreationDate { get; set; }
    }
}