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

namespace Aridio_Rent_A_Car.Server.Endpoints.Clientes
{
    using Respuesta = Result<int>;
    using Request = ClienteUpdateRequest;

    public class Update : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
    {
        private readonly IMyDbContext dbContext;

        public Update(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut(ClienteRouteManager.Update)]
        public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var cliente = await dbContext.Clientes.FindAsync(request.Id);
                if (cliente == null)
                    return Respuesta.Fail($"No fue posible encontrar el cliente con el id '{request.Id}'");

                // Actualiza los campos del cliente con los valores proporcionados en la solicitud

                cliente.Nombre = request.Nombre;
                cliente.Direccion = request.Direccion;
                cliente.Telefono = request.Telefono;
                cliente.Nacionalidad = request.Nacionalidad;
                cliente.Cedula = request.Cedula;
                cliente.Ocupacion = request.Ocupacion;
                cliente.Pasaporte = request.Pasaporte;
                cliente.Licencia = request.Licencia;
                cliente.FechaExpiracionLicencia = request.FechaExpiracionLicencia;
                cliente.Sexo = request.Sexo;
                // Actualiza otros campos según sea necesario

                await dbContext.SaveChangesAsync(cancellationToken);
                return Ok(cliente.Id);
            }
            catch (Exception ex)
            {
                return Respuesta.Fail(ex.Message);
            }
        }
    }
}
