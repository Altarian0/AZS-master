//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public int ID { get; set; }
        public int FuelID { get; set; }
        public int FuelColumnID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Datetime { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal Sum { get; set; }
        public int SaleTypeID { get; set; }
    
        public virtual Fuel Fuel { get; set; }
        public virtual FuelColumn FuelColumn { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual User User { get; set; }
    }
}