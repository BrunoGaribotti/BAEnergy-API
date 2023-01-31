using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    /// <summary>
    /// Objeto de Retorno - Devolución OK/ERROR al sistema BAEnergy.
    /// </summary>
    public class Retorno
    {
        public bool ESTADO = false;
        public String N_COMP = "";
        public List<String> errores = new List<String>();
    }
}
