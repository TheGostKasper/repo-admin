﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class Merchant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Merchant name (EN) is required")]
        [Display(Name = "Merchant name (EN)")]
        public string NameEN { get; set; }

        [Required(ErrorMessage = "Merchant name (AR) is required")]
        [Display(Name = "Merchant name (AR)")]
        public string NameAR { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Short Password is required")]
        [Display(Name = "Short Password")]
        public string ShortPassword { get; set; }

        [Required(ErrorMessage = "SmsCode is required")]
        [Display(Name = "SmsCode")]
        public string SmsCode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country name")]
        public int CountryId { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Account State is required")]
        [Display(Name = "Account State")]
        public int AccountStateId { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [Required(ErrorMessage = "Language is required")]
        [Display(Name = "Language")]
        public int LangId { get; set; }

        [Display(Name = "Logo")]
        public string LogoUrl { get; set; }
        [Required(ErrorMessage = "Latitude is required")]
        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }
        [Required(ErrorMessage = "Longitude is required")]
        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }

        [Required(ErrorMessage = "DeliveryTime is required")]
        [Display(Name = "DeliveryTime")]
        public int DeliveryTime { get; set; }

        [Display(Name = "Service Fees")]
        public double ServiceFees { get; set; }

        [Display(Name = "Tax")]
        public double Tax { get; set; }

        [Required(ErrorMessage = "MinOrder is required")]
        [Display(Name = "Min Order")]
        public int MinOrder { get; set; }

        [Required(ErrorMessage = "Percentage is required")]
        [Display(Name = "Percentage")]
        public int Percentage { get; set; }

        [Display(Name = "Is Branched")]
        public int IsBranch { get; set; }

        public DateTime? CreationDate { get; set; }

        public virtual List<MerchantServingArea> ServingArea { get; set; }
        public virtual List<BusinessHours> BusinessHours { get; set; }
    }
}

