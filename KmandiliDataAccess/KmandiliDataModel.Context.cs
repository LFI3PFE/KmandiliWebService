﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KmandiliDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KmandiliDBEntities : DbContext
    {
        public KmandiliDBEntities()
            : base("name=KmandiliDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DeleveryDelay> DeleveryDelays { get; set; }
        public virtual DbSet<DeleveryMethod> DeleveryMethods { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Parking> Parkings { get; set; }
        public virtual DbSet<PastryDeleveryPayment> PastryDeleveryPayments { get; set; }
        public virtual DbSet<PastryShop> PastryShops { get; set; }
        public virtual DbSet<PastryShopDeleveryMethod> PastryShopDeleveryMethods { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<PointOfSale> PointOfSales { get; set; }
        public virtual DbSet<PriceRange> PriceRanges { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<SaleUnit> SaleUnits { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkDay> WorkDays { get; set; }
    }
}
