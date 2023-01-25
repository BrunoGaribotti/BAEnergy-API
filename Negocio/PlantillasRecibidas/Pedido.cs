using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.PlantillasRecibidas
{
 
    public class Pedido
    {

        public List<PlantillasRecibidas.Renglon> renglones = new List<PlantillasRecibidas.Renglon>();
 
        public string fecha { get; set; }
 
        public string cod_cliente { get; set; }
 
        public string n_lista { get; set; }
 
        public string nro_pedido { get; set; }

    }
}
