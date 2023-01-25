using Negocio.PlantillasRecibidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    [ServiceContract]
    public interface wsServicioI
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Retorno GuardarPedido(Pedido p);

        //GET consulta
        //POST inserta



    }
}

