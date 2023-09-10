using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
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
        public async Task<ActionResult<List<Cliente>>> UpdateCliente(Cliente cl)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", cl.Id);
            parametro.Add("@Nombre", cl.Nombre);
            parametro.Add("@Direccion", cl.Direccion);
            parametro.Add("@Telefono", cl.Telefono);
            var oCliente = conexion.Query<Cliente>("SP_ActualizarCliente ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Cliente>>> DeleteClientebyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Cliente>("SP_EliminarCliente", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }   

}
