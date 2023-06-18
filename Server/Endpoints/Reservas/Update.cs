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

namespace Aridio_Rent_A_Car.Server.Endpoints.Reservas
{
    using Respuesta = Result<int>;
    using Request = ReservaUpdateRequest;

    public class Update : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
    {
        private readonly IMyDbContext dbContext;

        public Update(IMyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut(ReservaRouteManager.Update)]
        public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var reserva = await dbContext.Reservas.FindAsync(request.Id);
                if (reserva == null)
                    return Respuesta.Fail($"No fue posible encontrar el cliente con el id '{request.Id}'");

                // Actualiza los campos del cliente con los valores proporcionados en la solicitud

                reserva.FechaInicio = request.FechaInicio;
                reserva.FechaFin = request.FechaFin;
                reserva.VehiculoId = request.VehiculoId;
                reserva.ClienteId = request.ClienteId;
                reserva.Dias = request.Dias;
                reserva.PrecioTotal = request.PrecioTotal;
                reserva.precioRenta = request.precioRenta;
                reserva.Pago = request.Pago;
                reserva.NombrePago = request.NombrePago;
                reserva.Fecha = request.Fecha;
                reserva.Finalizada = request.Finalizada;
                // Actualiza otros campos según sea necesario
                

                await dbContext.SaveChangesAsync(cancellationToken);
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return Respuesta.Fail(ex.Message);
            }
        }
    }
}
