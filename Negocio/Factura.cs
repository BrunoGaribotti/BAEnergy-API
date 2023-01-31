using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>
    /// Objeto de Factura - Contiene datos mínimos necesarios de facturación.
    /// </summary>
    public class Factura
    {
        public string F_COMP_CAN { get; set; } //GVA07
        public string N_COMP { get; set; }    //GVA07
        public string IMPORTE { get; set; }  //GVA12   Buscar por T_COMP='FAC' y N_COMP
        public string COD_CLIENT { get; set; } //GVA12
        public string MON_CTE { get; set; } //GVA12
        public string COTIZ { get; set; } //GVA12
        public List<PedidoFac> PEDIDOS = new List<PedidoFac>();

        public string IMPORTE_GR { get; set; }
        public string DESC_ALICUOTA { get; set; }
        public string TIPO_IMPUESTO { get; set; }
        public string PORCENTAJE { get; set; }
        public string TIPO_FACTURA { get; set; }
    }
}
