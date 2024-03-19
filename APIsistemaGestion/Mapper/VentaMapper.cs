using APIsistemaGestion.DTO;
using APIsistemaGestion.Models;

namespace APIsistemaGestion.Mapper
{
    public class VentaMapper
    {
        public int Id { get; set; }
        public string? Comentarios { get; set; }
        public int IdUsuario { get; set; }

        public static Ventum MapearVentaDTOAVenta(VentaDTO ventaDTO)
        {

            Ventum venta = new Ventum();
            venta.Id = ventaDTO.Id;
            venta.Comentarios = ventaDTO.Comentarios;
            venta.IdUsuario = ventaDTO.IdUsuario;
            return venta;
        }

        public static VentaDTO MapearVentaAVentaDTO(Ventum venta)
        {

            VentaDTO ventaDTO = new VentaDTO();

            ventaDTO.Id = venta.Id;
            ventaDTO.Comentarios = venta.Comentarios;
            ventaDTO.IdUsuario = venta.IdUsuario;

            return ventaDTO;
        }
    }
}
