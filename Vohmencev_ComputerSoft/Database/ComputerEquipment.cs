//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vohmencev_ComputerSoft.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ComputerEquipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ComputerEquipment()
        {
            this.OrderInfo = new HashSet<OrderInfo>();
        }
    
        public int EquipmentCode { get; set; }
        public string EquipmentSerialNumber { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
    
        public virtual EquipmentType EquipmentType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}
