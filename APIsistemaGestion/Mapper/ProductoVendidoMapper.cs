using APIsistemaGestion.DTO;
using APIsistemaGestion.Models;

namespace APIsistemaGestion.Mapper
{
    public class ProductoVendidoMapper
    {
        public static ProductoVendido MapearProductoVendidoDTOAProductoVendido(ProductoVendidoDTO productoVendidoDTO)
        {

            ProductoVendido productoVendido = new ProductoVendido();

            productoVendido.Id = productoVendidoDTO.Id;
            productoVendido.Stock = productoVendidoDTO.Stock;
            productoVendido.IdProducto = productoVendidoDTO.IdProducto;
            productoVendido.IdVenta = productoVendidoDTO.IdVenta;

            return productoVendido;
        }

        public static ProductoVendidoDTO MapearProductoVendidoAProductoVendidoDTO(ProductoVendido productoVendido)
        {

            ProductoVendidoDTO productoVendidoDTO = new ProductoVendidoDTO();

            productoVendidoDTO.Id = productoVendido.Id;
            productoVendidoDTO.Stock = productoVendido.Stock;
            productoVendidoDTO.IdProducto = productoVendido.IdProducto;
            productoVendidoDTO.IdVenta = productoVendido.IdVenta;

            return productoVendidoDTO;
        }
    }
}
