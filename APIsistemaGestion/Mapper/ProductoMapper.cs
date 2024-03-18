using APIsistemaGestion.Models;
using APIsistemaGestion.DTO;
namespace APIsistemaGestion.Mapper
{
    public class ProductoMapper
    {
        public static Producto MapearProductoDTOAProducto(ProductoDTO productoDTO)
        {

            Producto producto = new Producto();

            producto.Id = productoDTO.Id;
            producto.Descripciones = productoDTO.Descripciones;
            producto.Costo = productoDTO.Costo;
            producto.PrecioVenta = productoDTO.PrecioVenta;
            producto.Stock = productoDTO.Stock;
            producto.IdUsuario = productoDTO.IdUsuario;

            return producto;
        }

        public static ProductoDTO MapearProductoAProductoDTO(Producto producto)
        {

            ProductoDTO productoDTO = new ProductoDTO();

            productoDTO.Id = producto.Id;
            productoDTO.Descripciones = producto.Descripciones;
            productoDTO.Costo = producto.Costo;
            productoDTO.PrecioVenta = producto.PrecioVenta;
            productoDTO.Stock = producto.Stock;
            productoDTO.IdUsuario = producto.IdUsuario;

            return productoDTO;
        }


        public static Producto MapearProductoDTOAProducto(ProductoDTO productoDTO, Producto producto)
        {

            producto.Id = productoDTO.Id;
            producto.Descripciones = productoDTO.Descripciones;
            producto.Costo = productoDTO.Costo;
            producto.PrecioVenta = productoDTO.PrecioVenta;
            producto.Stock = productoDTO.Stock;
            producto.IdUsuario = productoDTO.IdUsuario;

            return producto;
        }
    }
}
