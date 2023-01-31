using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>
    /// Objeto de Retorno - Devolución de Factura al sistema BAEnergy.
    /// </summary>
    public class RetornoFactura
    {
        public bool estado = false;
        public String N_COMP = "";
        public String error = "";
    }
}

