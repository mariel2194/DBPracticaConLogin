//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBPracticaConLogin
{
    using System;
    using System.Collections.Generic;
    
    public partial class MetodoPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetodoPago()
        {
            this.Clientes = new HashSet<Clientes>();
            this.Facturas = new HashSet<Facturas>();
        }
    
        public int MetodoPagoID { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> CantidadDias { get; set; }
        public Nullable<bool> MonedaLocal { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clientes> Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
    }
}
