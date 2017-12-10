﻿using AMS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AMS.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<AMS.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.SubCategory> SubCategories { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.ItemUnit> ItemUnits { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.AccountState> AccountStates { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Currency> Currencies { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Merchant> Merchants { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.MerchantServingArea> MerchantServingAreas { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.BusinessHours> BusinessHours { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Courier> Couriers { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.MerchantSupervisor> MerchantSupervisors { get; set; }
        public System.Data.Entity.DbSet<AMS.Models.MerchantCategories> MerchantCategories { get; set; }
        public System.Data.Entity.DbSet<AMS.Models.MerchantSubCategory> MerchantSubCategories { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.EmployeeRole> EmployeeRoles { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Models.MerchantProduct> MerchantProducts { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.ItemDetails> ItemDetails { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Models.ReportingIssues> ReportingIssues { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.Models.SuggestProduct> SuggestProducts { get; set; }
        public System.Data.Entity.DbSet<AMS.Models.Language> Languages { get; set; }

        public System.Data.Entity.DbSet<ItemType> ItemTypes { get; set; }

        public System.Data.Entity.DbSet<ItemPackaging> ItemPackagings { get; set; }

        public System.Data.Entity.DbSet<ItemBrand> ItemBrands { get; set; }

        public System.Data.Entity.DbSet<MerchantCoupon> MerchantCoupons { get; set; }
        public System.Data.Entity.DbSet<Shopper> Shoppers { get; set; }
        public System.Data.Entity.DbSet<MotorBrand> MotorBrand { get; set; }
        public System.Data.Entity.DbSet<MotorType> MotorType { get; set; }

        public System.Data.Entity.DbSet<AMS.Models.MerchantCouponType> MerchantCouponTypes { get; set; }
    }
}