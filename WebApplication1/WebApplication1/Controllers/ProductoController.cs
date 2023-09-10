using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IConfiguration _Config;

        public ProductoController(IConfiguration Config)
        {
            _Config = Config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAllProductos()
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var oCliente = conexion.Query<Producto>("SP_ObtenerTodosLosProductos", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<List<Producto>>> GetProductobyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Producto>("SP_ObtenerProductoPorID", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPost]
        public async Task<ActionResult<List<Producto>>> CreateN_Producto(Producto pr)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Nombre", pr.Nombre);
            parametro.Add("@Precio", pr.Precio);
            parametro.Add("@@Proveedor_Id", pr.Proveedor_Id);
            var oCliente = conexion.Query<Producto>("SP_InsertarProducto ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpPut]
        public async Task<ActionResult<List<Producto>>> UpdateProducto(Producto pr)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", pr.Id);
            parametro.Add("@Nombre", pr.Nombre);
            parametro.Add("@Precio", pr.Precio);
            parametro.Add("@@Proveedor_Id", pr.Proveedor_Id);
            var oCliente = conexion.Query<Producto>("SP_ActualizarProducto ", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Producto>>> DeleteProducctobyID(int ID)
        {
            using var conexion = new SqlConnection(_Config.GetConnectionString("base"));
            conexion.Open();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", ID);
            var oCliente = conexion.Query<Producto>("SP_EliminarProducto", parametro, commandType: System.Data.CommandType.StoredProcedure);
            return Ok(oCliente);
        }
    }
}
