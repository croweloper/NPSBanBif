﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanBif.Sintomatologia.DA
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class panelEntities1 : DbContext
    {
        public panelEntities1()
            : base("name=panelEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NPS_NUEVO_RESULTADO> NPS_NUEVO_RESULTADO { get; set; }
        public virtual DbSet<NPS_NUEVO_RESULTADO_MATRIZ> NPS_NUEVO_RESULTADO_MATRIZ { get; set; }
    }
}
