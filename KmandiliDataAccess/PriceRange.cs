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
    
    public partial class PriceRange
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceRange()
        {
            this.PastryShops = new HashSet<PastryShop>();
        }
    
        public int ID { get; set; }
        public double MinPriceRange { get; set; }
        public double MaxPriceRange { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastryShop> PastryShops { get; set; }
    }
}
