using Ardalis.ApiEndpoints;
using Aridio_Rent_A_Car.Server.Context;
using Aridio_Rent_A_Car.Shared.Requests;
using Aridio_Rent_A_Car.Shared.Routes;
using Aridio_Rent_A_Car.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aridio_Rent_A_Car.Server.Endpoints.UsuariosRoles
{
    using Respuesta = Result<int>;
    using Request = UsuarioRolUpdateRequest;

    public class Update : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
    {
        private readonly IMyDbContext dbContext;

        public Update(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut(UsuarioRolRouteManager.Update)]
        public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var Rol = await dbContext.UsuariosRoles.FindAsync(request.Id);
                if (Rol == null)
                    return Respuesta.Fail($"No fue posible encontrar el cliente con el id '{request.Id}'");

                // Actualiza los campos del cliente con los valores proporcionados en la solicitud

                Rol.Nombre = request.Nombre;
                Rol.PermisoParaCrear = request.PermisoParaCrear;
                Rol.PermisoParaEditar = request.PermisoParaEditar;
                Rol.PermisoParaEliminar = request.PermisoParaEliminar;

                // Actualiza otros campos según sea necesario

                await dbContext.SaveChangesAsync(cancellationToken);
                return Ok(Rol.Id);
            }
            catch (Exception ex)
            {
                return Respuesta.Fail(ex.Message);
            }
        }
    }
}
