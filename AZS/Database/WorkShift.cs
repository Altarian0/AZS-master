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
    
    public partial class WorkShift
    {
        public int ID { get; set; }
        public System.DateTime Datetime { get; set; }
        public int UserID { get; set; }
    
        public virtual User User { get; set; }
    }
}