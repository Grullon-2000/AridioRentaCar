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

namespace Aridio_Rent_A_Car.Server.Endpoints.Vehiculos
{
    using Respuesta = Result<int>;
    using Request = VehiculoUpdateRequest;

    public class Update : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
    {
        private readonly IMyDbContext dbContext;

        public Update(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut(VehiculoRouteManager.Update)]
        public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var Vehi = await dbContext.Vehiculos.FindAsync(request.Id);
                if (Vehi == null)
                    return Respuesta.Fail($"No fue posible encontrar el cliente con el id '{request.Id}'");

                // Actualiza los campos del cliente con los valores proporcionados en la solicitud

                Vehi.Marca = request.Marca;
                Vehi.Modelo = request.Modelo;
                Vehi.Año = request.Año;
                Vehi.Color = request.Color;
                Vehi.NumeroPlaca = request.NumeroPlaca;
                Vehi.PrecioPorDia= request.PrecioPorDia;
                Vehi.Mensaje = request.Mensaje;
                Vehi.Activo = request.Activo;
                Vehi.Inactivo = request.Inactivo;

                // Actualiza otros campos según sea necesario

                await dbContext.SaveChangesAsync(cancellationToken);
                return Ok(Vehi.Id);
            }
            catch (Exception ex)
            {
                return Respuesta.Fail(ex.Message);
            }
        }
    }
}
