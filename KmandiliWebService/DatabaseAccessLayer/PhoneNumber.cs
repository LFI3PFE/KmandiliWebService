//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KmandiliWebService.DatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhoneNumber
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public int PhoneNumberType_FK { get; set; }
    
        public virtual PhoneNumberType PhoneNumberType { get; set; }
        public virtual PastryShop PastryShop { get; set; }
        public virtual PointOfSale PointOfSale { get; set; }
        public virtual User User { get; set; }
    }
}