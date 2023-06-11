using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aridio_Rent_A_Car.Shared
{
    public class RegistroDTO
    {
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}