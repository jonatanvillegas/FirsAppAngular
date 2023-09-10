using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;
using System;
using System.Globalization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedido_ClienteController : ControllerBase
    {
        private IConfiguration _Config;

        public Pedido_ClienteController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido_Clientecs>>> GetAllPedidos_clientes()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var oCliente = conexion.Query<Pedido_Clientecs>("SP_ObtenerTodosLosPedidos", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Pedido_Clientecs>>> GetPedido_clientebyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Pedido_Clientecs>("SP_ObtenerPedidoPorID ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pedido_Clientecs>>> CreateN_Pedido_cliente(Pedido_Clientecs pc)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo Date
            if (DateTime.TryParseExact(pc.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                pc.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Cliente_id", pc.Cliente_id);
            parametro.Add("@Producto_id", pc.Producto_id);
            parametro.Add("@Cantidad_Pro", pc.Cantidad_Pro);
            parametro.Add("@Fecha", pc.Fecha);
            parametro.Add("@Empleado_Id", pc.Empleado_Id);
            var oCliente = conexion.Query<Pedido_Clientecs>("SP_InsertarPedidoCliente    ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPut]
        public async Task<ActionResult<List<Pedido_Clientecs>>> UpdatePedido_cliente(Pedido_Clientecs pc)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo DateTime
            if (DateTime.TryParseExact(pc.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                pc.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Id", pc.Id);
            parametro.Add("@Cliente_id", pc.Cliente_id);
            parametro.Add("@Producto_id", pc.Producto_id);
            parametro.Add("@Cantidad_Pro", pc.Cantidad_Pro);
            parametro.Add("@Fecha", pc.Fecha);
            parametro.Add("@Empleado_Id", pc.Empleado_Id);
            var oCliente = conexion.Query<Pedido_Clientecs>("SP_ActualizarPedido  ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Pedido_Clientecs>>> DeletePedido_cliente(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Pedido_Clientecs>("SP_EliminarPedido", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }
}
