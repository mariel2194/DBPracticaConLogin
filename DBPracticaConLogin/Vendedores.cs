//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBPracticaConLoginSearchYList
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Vendedores
    {
        public bool ValidarCedula(string cedula)
        {
            int digito = 0;
            int digitoVerificador = 0;
            bool resultado = false;
            int[] multiplicadores = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int producto = 0;
            int suma = 0;

            if (cedula.Contains("-"))
                cedula = cedula.Replace("-", "");

            _ = int.TryParse(cedula.Substring(cedula.Length - 1), out digitoVerificador);

            for (int i = 0; i < (cedula.Length - 1); i++)
            {
                _ = int.TryParse(cedula[i].ToString(), out digito);
                producto = digito * multiplicadores[i];

                if (producto >= 10)
                    producto = (producto / 10) + (producto % 10);

                suma += producto;
            }

            if ((suma + digitoVerificador) % 10 == 0)
                resultado = true;

            return resultado;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendedores()
        {
            this.Facturas = new HashSet<Facturas>();
        }
    
        public int VendedorId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La cedula es obligatoria")]
       
        public string Cedula { get; set; }

        [Range(15000,1500000, ErrorMessage = "Value should be greater than or equal to 1")]

        public Nullable<decimal> Salario { get; set; }

        public Nullable<bool> Activo { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"(1\s?)?(849\s?|829\s?|809)[\s\-]?\d{3}[\s\-]?\d{4}",
                           ErrorMessage = "Digite un numero de telefono valido usando guiones (-).")]
        public string Telefono { get; set; }
         [Display(Name = "% Comision Por Venta")]
        public Nullable<decimal> ComisionPorVenta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facturas> Facturas { get; set; }
    }

    
    }

