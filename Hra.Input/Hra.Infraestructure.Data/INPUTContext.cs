using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Hra.Domain.Entity;
using Hra.Application.DTO;

namespace Hra.Infraestructure.Data
{
    public partial class INPUTContext 
    {       

        public virtual DbSet<UspAutenticarUsuario> UspAutenticarUsuario { get; set; } = null!;
        public virtual DbSet<UspListarPermisoMenu> UspListarPermisoMenu { get; set; } = null!;

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UspAutenticarUsuario>().HasNoKey();
            modelBuilder.Entity<UspListarPermisoMenu>().HasNoKey();
        }
    }
}
