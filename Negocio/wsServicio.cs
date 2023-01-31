using BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public partial class wsServicio : wsServicioI
    {
        Procesos oProc = new Procesos();
        DataTable dt = new DataTable();

        /// <summary>
        /// Guarda el pedido en Tango 
        /// <para>Tablas tocadas: GVA21 y GVA03.</para>
        /// </summary>
        public Retorno GuardarPedido(PlantillasRecibidas.Pedido p)
        {

            BaseDeDatos db = new BaseDeDatos();
            db.Configurar("");
            db.Conectar();
            GVA21 P = new GVA21();

            try
            {
                P = oProc.LlenarPedido(p);
            }
            catch(Exception e)
            {
               P.Retorno.errores.Add("Error en el formato de los datos enviados. ");
            }

            if (oProc.ExistePedido(P.NRO_PEDIDO))
            {
                P.Retorno.errores.Add("El pedido ya existe. ");
            }
            else
            {
                db.ComenzarTransaccion();
                try
                {
                    db.CrearComando("SELECT TOP(1) * FROM GVA12");
                    DbDataReader dr = db.EjecutarConsulta();
                    dt.Load(dr);
                    //Conseguir el ID del asiento modelo
                    /*
                    db.CrearComando(@"select ID_ASIENTO_MODELO_GV, COD_ASIENTO_MODELO_GV from ASIENTO_MODELO_GV WHERE COD_ASIENTO_MODELO_GV = '" + P.TIPO_ASIEN + "'");
                    DbDataReader dr = db.EjecutarConsulta();
                    dt.Load(dr);
                    P.ID_ASIENTO_MODELO_GV = dt.Rows[0]["ID_ASIENTO_MODELO_GV"].ToString();
                    dt.Dispose();
                    */

                    //Conseguir la unidad de medida en STA11
                    try
                    {
                        P = oProc.GetUnidadMedida(db, dr, P);
                    }
                    catch (Exception e)
                    {
                        P.Retorno.errores.Add("Error en la unidad de medida. ");
                    }

                    //Conseguir  código  de transporte, de cliente y condición de venta en GVA14  
                    try
                    {
                        P = oProc.GetCodTrans(db, dr, P);
                    }
                    catch (Exception e)
                    {
                        P.Retorno.errores.Add("Error en el código de transporte o el código de cliente. ");
                    }

                    //Conseguir dirección de entrega
                    try
                    {
                        P = oProc.GetDireccionEntrega(db, dr, P);
                    }
                    catch (Exception e)
                    {
                        P.Retorno.errores.Add("Error en la dirección de entrega del cliente. ");
                    }

                    try
                    {
                        P = oProc.GetCotizacion(db, dr, P);
                    }
                    catch(Exception e)
                    {
                        P.Retorno.errores.Add("Error en la cotización de Tango.");
                    }

                    //Inserta el pedido y los renglones si no hay error, sino cancela la transacción
                    if (P.Retorno.errores.Count == 0)
                    {
                        db.CrearComando(P.Insert());
                        db.EjecutarComando();

                        foreach (GVA03 renglon in P.cGVA03)
                        {
                            db.CrearComando(renglon.Insert());
                            db.EjecutarComando();
                        }
                        db.ConfirmarTransaccion();
                    }
                    else db.CancelarTransaccion();
                }
                catch(Exception e)
                {
                    P.Retorno.errores.Add("Hubo un error en el formato al insertar los datos en la tabla de Tango.");
                    db.CancelarTransaccion();
                }
            db.Desconectar();
            }

            //Devuelvo el mensaje con los datos pactados (nro de pedido y list de errores)
            Retorno R = new Retorno();
            R.N_COMP = P.NRO_PEDIDO;
            if (P.Retorno.errores.Count > 0)
            {
                R.ESTADO = false;
                foreach (string error in P.Retorno.errores)
                {
                    R.errores.Add(error);
                }
            }
            else R.ESTADO = true;
            return R;
        }

    }
}
