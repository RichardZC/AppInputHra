using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hra.Application.DTO
{
    public class UspListarPermisoMenu
    {
        public string Denominacion { get; set; }
        public string Controlador { get; set; }
        public int TablaId { get; set; }
        public int ItemId { get; set; }
    }
}
