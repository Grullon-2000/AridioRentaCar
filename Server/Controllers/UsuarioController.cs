using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aridio_Rent_A_Car.Server.Models;
using Aridio_Rent_A_Car.Server.Context;
using Aridio_Rent_A_Car.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorLogin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMyDbContext dbContext;

        public UsuarioController(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();

            if(login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                sesionDTO.Nombre = "admin";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Administrador";
            }
            else
            {
                sesionDTO.Nombre = "empleado";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Empleado";
            }

            if(login.Correo == "empleado@gmail.com" && login.Clave == "empleado")
            {
                sesionDTO.Nombre = "empleado";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Empleado";
            }

            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDTO registro)
        {
            // Verificar si el correo ya está registrado en la base de datos
            bool correoExistente = await dbContext.Usuarios.AnyAsync(u => u.Correo == registro.Correo);
            if (correoExistente)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "El correo ya está registrado");
            }

            // Crear un nuevo usuario en la base de datos
            var usuario = new Usuario
            {
                Correo = registro.Correo,
                Clave = registro.Clave,
                Nombre = registro.Nombre,
                Rol = "Cliente"
            };

            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "Registro exitoso");
        }
    }
}
