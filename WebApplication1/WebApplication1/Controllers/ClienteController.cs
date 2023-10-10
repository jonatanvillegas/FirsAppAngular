using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IConfiguration _Config;

        public ClienteController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAllCliente()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("DefaultConnection"));
            conexion.Open();
            var oCliente = conexion.Query<Cliente>("SP_ObtenerTodosLosClientes", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Cliente>>> GetClientebyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Cliente>("SP_ObtenerClientePorID",parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateN_Cliente(Cliente cl)
        {
            try
            {
                using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
                conexion.Open();
                var parametro = new DynamicParameters();
                parametro.Add("@Nombre", cl.Nombre);
                parametro.Add("@Direccion", cl.Direccion);
                parametro.Add("@Telefono", cl.Telefono);
                
                var oCliente = conexion.Query<Cliente>("SP_InsertarCliente", parametro, commandType: System.Data.CommandType.StoredProcedure);

                // Verificar si la operación fue exitosa (por ejemplo, si oCliente no es nulo)
                if (oCliente != null)
                {
                    
                    var mensaje = "Cliente creado exitosamente.";
                    return Ok(new { mensaje, resultado = oCliente });
                }
                else
                {
                   
                    var mensaje = "No se pudo crear el cliente.";
                    return BadRequest(new { mensaje });
                }
            }
            catch (Exception ex)
            {
                
                var mensaje = "Se produjo un error al crear el cliente: " + ex.Message;
                return StatusCode(500, new { mensaje });
            }
        }
        [HttpPut]
        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cl)
        {
            try
            {
                using (var conexion = new SqlConnection(_Config.GetConnectionString("base")))
                {
                    await conexion.OpenAsync();

                    var parametro = new DynamicParameters();
                    parametro.Add("@Id", cl.Id);
                    parametro.Add("@Nombre", cl.Nombre);
                    parametro.Add("@Direccion", cl.Direccion);
                    parametro.Add("@Telefono", cl.Telefono);

                    // Ejecutar el procedimiento almacenado para actualizar el cliente
                    await conexion.ExecuteAsync("SP_ActualizarCliente", parametro, commandType: CommandType.StoredProcedure);

                    // Devolver el cliente actualizado (puedes cargarlo nuevamente desde la base de datos si es necesario)
                    var clienteActualizado = await conexion.QuerySingleOrDefaultAsync<Cliente>("SELECT * FROM Clientes WHERE Id = @Id", new { Id = cl.Id });

                    if (clienteActualizado != null)
                    {
                        return Ok(clienteActualizado);
                    }
                    else
                    {
                        return NotFound(); // El cliente no se encontró después de la actualización
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver una respuesta de error
                return StatusCode(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }


        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteClientebyID(int ID)
        {
            try
            {
                using (var conexion = new SqlConnection(_Config.GetConnectionString("base")))
                {
                    await conexion.OpenAsync();

                    var parametro = new DynamicParameters();
                    parametro.Add("@Id", ID);
                    await conexion.ExecuteAsync("SP_EliminarCliente", parametro, commandType: CommandType.StoredProcedure);

                    return Ok("Cliente eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error al eliminar el cliente: {ex.Message}");
            }
        }
    }   

}
