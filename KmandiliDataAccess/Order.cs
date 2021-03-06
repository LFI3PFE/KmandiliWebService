//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }
    
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Status_FK { get; set; }
        public int User_FK { get; set; }
        public int PastryShop_FK { get; set; }
        public int DeleveryMethod_FK { get; set; }
        public int PaymentMethod_FK { get; set; }
        public bool SeenUser { get; set; }
        public bool SeenPastryShop { get; set; }
    
        public virtual DeleveryMethod DeleveryMethod { get; set; }
        public virtual PastryShop PastryShop { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
