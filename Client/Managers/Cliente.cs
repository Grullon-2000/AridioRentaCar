using Aridio_Rent_A_Car.Shared.Wrapper;
using Aridio_Rent_A_Car.Shared.Records;
using Aridio_Rent_A_Car.Shared.Routes;
using Aridio_Rent_A_Car.Client.Extensions;
using Aridio_Rent_A_Car.Shared.Requests;
using System.Net.Http.Json;
using OfficeOpenXml;

namespace Aridio_Rent_A_Car.Client.Managers;

public interface IClienteManager
{
    Task<ResultList<ClienteRecord>> GetAsync();
    Task<Result<int>> CreateAsync(ClienteCreateRequest request);
    Task<Result<ClienteRecord>> GetByIdAsync(int Id);
    Task<Result> DeleteAsync(int id);
    Task<Result> UpdateAsync(int id, ClienteUpdateRequest request);
    Task<Result<List<ClienteRecord>>> BuscarPorNombreAsync(string nombre);
}

public class ClienteManager : IClienteManager
{
    private readonly HttpClient httpClient;

    public ClienteManager(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ResultList<ClienteRecord>> GetAsync()
    {
        try
        {
            var response = await httpClient.GetAsync(ClienteRouteManager.BASE);
            var resultado = await response.ToResultList<ClienteRecord>();
            return resultado;
        }
        catch (Exception e)
        {
            return ResultList<ClienteRecord>.Fail(e.Message);
        }
    }

    public async Task<Result<int>> CreateAsync(ClienteCreateRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(ClienteRouteManager.BASE,request);
        return await response.ToResult<int>();
    }

    public async Task<Result<ClienteRecord>> GetByIdAsync(int Id)
    {
        var response = await httpClient.GetAsync(ClienteRouteManager.BuildRoute(Id));
        return await response.ToResult<ClienteRecord>();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync(ClienteRouteManager.BuildRouteDelete(id));
            var resultado = await response.ToResult<int>();
            return resultado;
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    
        public async Task<Result> UpdateAsync(int id, ClienteUpdateRequest request)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{ClienteRouteManager.BASE}/{id}", request);
                return await response.ToResult<int>();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }




public async Task<Result<List<ClienteRecord>>> BuscarPorNombreAsync(string nombre)
{
    try
    {
        var response = await httpClient.GetAsync($"{ClienteRouteManager.BASE}/buscar?nombre={nombre}");
        var resultado = await response.ToResultList<ClienteRecord>();

        if (resultado.Succeeded)
        {
            var listaClientes = resultado.Items.ToList();
            return Result<List<ClienteRecord>>.Sucess(listaClientes, resultado.Message);
        }
        else
        {
            return Result<List<ClienteRecord>>.Fail(resultado.Message);
        }
    }
    catch (Exception ex)
    {
        return Result<List<ClienteRecord>>.Fail(new List<string> { "Error al buscar clientes: " + ex.Message });
    }
}








}