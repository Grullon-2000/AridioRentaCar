using System.ComponentModel.DataAnnotations;
using Aridio_Rent_A_Car.Shared.Records;
using Aridio_Rent_A_Car.Shared.Requests;

namespace Aridio_Rent_A_Car.Server.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;        
    public string Correo { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public string Rol { get; set; } = null!;

    public static Usuario Crear(UsuarioCreateRequest request)
    {
        return new Usuario(){
            Nombre = request.Nombre,
            Correo = request.Correo,
            Clave = request.Clave,
            Rol = request.Rol
        };
    }

    public void Modificar(UsuarioUpdateRequest request)
    {
        if(Nombre != request.Nombre)
            Nombre = request.Nombre;
        if(Correo != request.Correo)
            Correo = request.Correo;
        if(Clave != request.Clave)
            Clave = request.Clave;
        if(Rol != request.Rol)
            Rol = request.Rol;
    }

    public UsuarioRecord ToRecord()
    {
        return new UsuarioRecord(Id, Nombre, Correo, Clave, Rol);
    }
}
