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
    
    public partial class Vendedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendedores()
        {
            this.Facturas = new HashSet<Facturas>();
        }
    
        public int VendedorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public Nullable<decimal> Salario { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Nullable<decimal> ComisionPorVenta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
    }
}
