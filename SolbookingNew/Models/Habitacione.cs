//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SolbookingNew.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Habitacione
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<int> HotelId { get; set; }
    
        public virtual Hotele Hotele { get; set; }
    }
}
