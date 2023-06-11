
namespace Aridio_Rent_A_Car.Shared.Records;

public class UsuarioRecord
{
    public UsuarioRecord()
    {
    }

    public UsuarioRecord(int id, string nombre, string correo, string clave, string rol)
    {
        Id = id;
        Nombre = nombre;
        Correo = correo;
        Clave = clave;
        Rol = rol;
    }

    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Clave { get; set; } = null!;
    public string Rol { get; set; } = null!;
}
