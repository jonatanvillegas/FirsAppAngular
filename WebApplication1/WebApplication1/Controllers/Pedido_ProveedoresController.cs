using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Globalization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedido_ProveedoresController : ControllerBase
    {
        private IConfiguration _Config;

        public Pedido_ProveedoresController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido_Proveedores>>> GetAllPedido_Proveedores()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var oCliente = conexion.Query<Pedido_Proveedores>("SP_ObtenerPedidosProveedores", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Pedido_Proveedores>>> GetPedido_ProveedoresbyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Pedido_Proveedores>("SP_ObtenerPedido_ProveedoresPorID ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pedido_Proveedores>>> CreateN_Pedido_Proveedores(Pedido_Proveedores pp)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo Date
            if (DateTime.TryParseExact(pp.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                pp.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Nombre_Producto", pp.Nombre_Producto);
            parametro.Add("@Cantidad", pp.Cantidad);
            parametro.Add("@Fecha", pp.Fecha);
            parametro.Add("@Empleado_Id", pp.Empleado_Id);
            var oCliente = conexion.Query<Pedido_Proveedores>("SP_InsertarPedido_Proveedores ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPut]
        public async Task<ActionResult<List<Pedido_Proveedores>>> UpdatePedido_Proveedores(Pedido_Proveedores pp)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();

            // Convertir la propiedad "Fecha" a tipo DateTime
            if (DateTime.TryParseExact(pp.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                pp.Fecha = fechaConvertida.ToString("yyyy-MM-dd"); // Ajustar el formato de fecha si es necesario
            }
            else
            {
                return BadRequest("Formato de fecha inválido.");
            }

            var parametro = new DynamicParameters();
            parametro.Add("@Id", pp.Id);
            parametro.Add("@Nombre_Producto", pp.Nombre_Producto);
            parametro.Add("@Cantidad", pp.Cantidad);
            parametro.Add("@Fecha", pp.Fecha);
            parametro.Add("@Empleado_Id", pp.Empleado_Id);
            var oCliente = conexion.Query<Pedido_Proveedores>("SP_ActualizarPedidoProveedores", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Pedido_Proveedores>>> DeletePedido_Proveedores(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Pedido_Proveedores>("SP_EliminarPedidoProveedores", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }
}
