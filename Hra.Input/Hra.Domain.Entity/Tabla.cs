using System;
using System.Collections.Generic;

namespace Hra.Domain.Entity
{
    public partial class Tabla
    {
        public int TablaId { get; set; }
        public int ItemId { get; set; }
        public string Denominacion { get; set; } = null!;
        public string? Valor { get; set; }
        public string? DesCorta { get; set; }
    }
}
