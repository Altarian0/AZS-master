﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AZS.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AZSEntities : DbContext
    {
        public AZSEntities()
            : base("name=AZSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ColumnStatus> ColumnStatus { get; set; }
        public virtual DbSet<Fuel> Fuel { get; set; }
        public virtual DbSet<FuelColumn> FuelColumn { get; set; }
        public virtual DbSet<FuelDelivery> FuelDelivery { get; set; }
        public virtual DbSet<FuelOfFuelColumn> FuelOfFuelColumn { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<SaleType> SaleType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WorkShift> WorkShift { get; set; }
    }
}
