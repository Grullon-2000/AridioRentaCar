
using System.ComponentModel.DataAnnotations;

namespace Aridio_Rent_A_Car.Shared.Requests;

public class ReservaCreateRequest 
{
    [Required(ErrorMessage = "Ingrese una fecha valida")]
    public DateTime FechaInicio { get; set; }
    
    [Required(ErrorMessage = "Ingrese una fecha valida")]
    public DateTime FechaFin { get; set; }
    
    [Required(ErrorMessage = "Ingrese un Id valido")]
    public int VehiculoId { get; set; }
    
    [Required(ErrorMessage = "Ingrese un Id valido")]
     public int ClienteId { get; set; }

    [Required(ErrorMessage = "Ingrese un Dia valido")]
    public int Dias { get; set; }

    [Required(ErrorMessage = "Ingrese un Precio valido")]
    public decimal PrecioTotal { get; set; }

    [Required(ErrorMessage = "Ingrese un PrecioRenta valido")]
    public decimal precioRenta { get; set; }

    [Required(ErrorMessage = "Ingrese un Pago valido")]
    public string Pago {get; set;} = null!;

     [Required(ErrorMessage = "Ingrese un NombrePago valido")]
    public string NombrePago {get; set;} = null!;

    [Required(ErrorMessage = "Ingrese un Fecha valido")]
    public DateTime Fecha {get; set;}
    public bool Finalizada { get; set; }

}
