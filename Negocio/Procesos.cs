using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using BaseDatos;
using Newtonsoft.Json;

namespace Negocio
{
    public class Procesos
    {
        DataTable dt = new DataTable();
        DataTable dtFac = new DataTable();
        DataTable dtAux = new DataTable();
        DataTable dtImp = new DataTable();
        public bool ExistePedido(string nro)
        {
            BaseDeDatos db = new BaseDeDatos();
            db.Configurar("");
            db.Conectar();


            db.CrearComando(@"select nro_pedido from GVA21
                                where nro_pedido = '" + nro + "'");
            DbDataReader dr = db.EjecutarConsulta();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count == 1)
            {
                db.Desconectar();
                return true;
            }

            db.Desconectar();
            return false;
        }
        public GVA21 LlenarPedido(PlantillasRecibidas.Pedido p)
        {
            GVA21 P = new GVA21();

            P.NRO_PEDIDO = p.nro_pedido;
            P.COD_CLIENT = p.cod_cliente;
            P.FECHA_PEDI = p.fecha;
            P.FECHA_INGRESO = p.fecha;
            P.N_LISTA = p.n_lista;
            
            //Esto puede cambiar si me envía la lista de precios en vez de la moenda corriente
            switch(p.n_lista)
            {
                case "1": P.MON_CTE = "1"; break;
                case "2": P.MON_CTE = "0"; break;
            }

            decimal total_pedido = 0;
            foreach (PlantillasRecibidas.Renglon r in p.renglones)
            {
                GVA03 R = new GVA03();
                R.COD_ARTICU = r.cod_articu;
                R.PRECIO = r.precio;
                R.PRECIO_LISTA = r.precio;
                R.N_RENGLON = r.renglon;
                P.cGVA03.Add(R);
                total_pedido += Decimal.Parse(r.precio);
            }

            P.TOTAL_PEDI = total_pedido.ToString();

            return P;
        }
        public GVA21 GetCodTrans(BaseDeDatos db, DbDataReader dr, GVA21 P)
        {
            db.CrearComando(@"SELECT COD_CLIENT, COD_TRANSP, COND_VTA FROM GVA14 WHERE COD_CLIENT = '" + P.COD_CLIENT + "'");
            dr = db.EjecutarConsulta();
            dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count == 1)
            {
                P.COD_TRANSP = dt.Rows[0]["COD_TRANSP"].ToString();
                P.COND_VTA = dt.Rows[0]["COND_VTA"].ToString();
            }
            else P.Retorno.errores.Add("El código de cliente no corresponde a ningún cliente.");
            dt.Dispose();
            return P;
        }
        public GVA21 GetDireccionEntrega(BaseDeDatos db, DbDataReader dr, GVA21 P)
        {
            db.CrearComando(@"select ID_DIRECCION_ENTREGA from direccion_entrega where HABITUAL = 'S' and HABILITADO = 'S' and COD_CLIENTE = '" + P.COD_CLIENT + "'");
            dr = db.EjecutarConsulta();
            dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count != 0)
            {
                P.ID_DIRECCION_ENTREGA = dt.Rows[0]["ID_DIRECCION_ENTREGA"].ToString();
            }
            else P.Retorno.errores.Add("La dirección de entrega es inválida.");
            dt.Dispose();
            return P;
        }

        internal GVA21 GetCotizacion(BaseDeDatos db, DbDataReader dr, GVA21 P)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Remove(0, 6) + "\\CONFIG\\CONFIG.XML"); //Cambiar
            string cod_cotizacion = ds.Tables[0].Rows[0]["COD_COTIZACION"].ToString();
            string cod_moneda = ds.Tables[0].Rows[0]["COD_MONEDA"].ToString();

            //db.CrearComando(@"select top(1) moneda.cod_moneda, fecha_hora, cotizacion.id_moneda, cotizacion from cotizacion
            //inner join moneda
            //on cotizacion.ID_MONEDA = moneda.ID_MONEDA 
            //order by fecha_hora desc");
            db.CrearComando(@"SELECT TOP(1) COD_MONEDA,COD_TIPO_COTIZACION,COTIZACION FROM COTIZACION
            LEFT OUTER JOIN MONEDA
            ON MONEDA.ID_MONEDA = COTIZACION.ID_MONEDA
            LEFT OUTER JOIN TIPO_COTIZACION
            ON TIPO_COTIZACION.ID_TIPO_COTIZACION = COTIZACION.ID_TIPO_COTIZACION
            WHERE COD_MONEDA = '"+cod_moneda+@"'
            AND COD_TIPO_COTIZACION = '"+cod_cotizacion+@"'
            ORDER BY FECHA_HORA DESC");
            dr = db.EjecutarConsulta();
            dt = new DataTable();
            dt.Load(dr);
            if (dt.Rows.Count != 0)
            {
                P.COTIZ = dt.Rows[0]["COTIZACION"].ToString().Replace(",", ".");
            }
            else P.Retorno.errores.Add("La cotización en Tango es inválida.");
            dt.Dispose();
            return P;
        }

        public GVA21 GetUnidadMedida(BaseDeDatos db, DbDataReader dr, GVA21 P)
        {
            foreach (GVA03 R in P.cGVA03)
            {
                db.CrearComando(@"select COD_ARTICU, ID_MEDIDA_STOCK, ID_MEDIDA_VENTAS from sta11 WHERE COD_ARTICU = '" + R.COD_ARTICU + "'");
                dr = db.EjecutarConsulta();
                dt = new DataTable();
                dt.Load(dr);
                R.ID_MEDIDA_STOCK = dt.Rows[0]["ID_MEDIDA_STOCK"].ToString();
                R.ID_MEDIDA_VENTAS = dt.Rows[0]["ID_MEDIDA_VENTAS"].ToString();
                R.NRO_PEDIDO = P.NRO_PEDIDO;
                dt.Dispose();
            }
            return P;
        }
        public List<Recibo> ConsultarRecibos(BaseDeDatos db, System.IO.StreamWriter log)
        {
            List<Recibo> cRecibos = new List<Recibo>();
            DateTime fecha = new DateTime();

            db.CrearComando(@"SELECT 
            COMP_REL.FILLER,
            COMPROBANTE.COD_CLIENT,
            COMPROBANTE.N_COMP,
            COMPROBANTE.FECHA_EMIS,
            COMPROBANTE.IMPORTE,
            COMPROBANTE.MON_CTE,
            COMPROBANTE.COTIZ,
            COMP_REL.N_COMP AS N_COMP_REL,
            COMP_REL.IMPORT_CAN AS IMPORTE_CAN,
            COMP_REL.T_COMP_CAN AS T_COMP_CAN,
            RETEN.COD_RET AS T_RETEN,
            RETEN.IMPORTE_RE AS IMPORTE_RETEN
            FROM GVA12 COMPROBANTE
            INNER JOIN GVA07 COMP_REL
            ON COMPROBANTE.N_COMP = COMP_REL.N_COMP_CAN
            AND COMPROBANTE.T_COMP = COMP_REL.T_COMP_CAN
            LEFT OUTER JOIN GVA67 RETEN
            ON COMPROBANTE.T_COMP = RETEN.T_COMP
            AND COMPROBANTE.N_COMP = RETEN.N_COMP
            WHERE COMPROBANTE.T_COMP = 'REC'
            AND COMP_REL.FILLER = ''");
            DbDataReader dr = db.EjecutarConsulta();
            dt.Clear();
            dt.Load(dr);
            try
            {
                string en = ""; //CAMBIAR
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Recibo R = new Recibo();
                    en = "COD_CLIENT";
                    R.COD_CLIENT = dt.Rows[i]["COD_CLIENT"].ToString();
                    en = "N_COMP";
                    R.N_COMP = dt.Rows[i]["N_COMP"].ToString();
                    //R.FECHA_EMIS = dt.Rows[i]["FECHA_EMIS"].ToString();//.Substring(0,10);
                    //fecha = (R.FECHA_EMIS, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    en = "FECHA_EMIS";
                    fecha = Convert.ToDateTime(dt.Rows[i]["FECHA_EMIS"].ToString());
                    R.FECHA_EMIS = fecha.ToString(@"dd/MM/yyyy H:mm:ss");
                    en = "IMPORTE";
                    R.IMPORTE = dt.Rows[i]["IMPORTE"].ToString().Replace(",", ".");
                    en = "MON_CTE";
                    if ((bool)dt.Rows[i]["MON_CTE"]) R.MON_CTE = "1";
                    else R.MON_CTE = "0";
                    //R.MON_CTE = dt.Rows[i]["MON_CTE"].ToString();
                    en = "COTIZ";
                    R.COTIZ = dt.Rows[i]["COTIZ"].ToString().Replace(",", ".");
                    en = "N_COMP_REL";
                    R.N_COMP_REL = dt.Rows[i]["N_COMP_REL"].ToString();
                    en = "T_COMP_CAN";
                    R.T_COMP_CAN = dt.Rows[i]["T_COMP_CAN"].ToString();
                    en = "IMPORTE_CAN";
                    R.IMPORTE_CAN = dt.Rows[i]["IMPORTE_CAN"].ToString().Replace(",", ".");
                    en = "T_RETEN";
                    R.T_RETEN = dt.Rows[i]["T_RETEN"].ToString();
                    en = "IMPROTE_RETEN";
                    R.IMPORTE_RETEN = dt.Rows[i]["IMPORTE_RETEN"].ToString().Replace(",", ".");

                    cRecibos.Add(R);
                }
            }
            finally
            {
                dr.Close();
                dt.Dispose();
            }
            return cRecibos;
        }

        public List<Factura> ConsultarFacturas(BaseDeDatos db) //ACUMULA PEDIDOS
        {
            List<Factura> cFacturas = new List<Factura>();
            DateTime fecha = new DateTime();

            db.CrearComando(@"select 
            NCOMP_V
            ,FECHA_FAC
            ,gva12.IMPORTE
            ,COD_CLIENT
            ,MON_CTE
            ,COTIZ
            ,IMPORTE_GR
            ,NRO_PEDIDO
            ,FECHA_PED
            from GVA105
            inner join gva12
            on gva12.N_COMP = gva105.NCOMP_V
            and gva12.T_COMP = gva105.TCOMP_V
            where TCOMP_V = 'FAC'
            and GVA12.FILLER = ''");

            DbDataReader dr = db.EjecutarConsulta();
            dtFac.Clear();
            dtFac.Load(dr);

            try
            {
                string NCOMPANTERIOR = "";
                for (int i = 0; i < dtFac.Rows.Count; i++)
                {
                    if (dtFac.Rows[i]["NCOMP_V"].ToString() != NCOMPANTERIOR)
                    {
                        Factura F = new Factura();
                        F.F_COMP_CAN = dtFac.Rows[i]["FECHA_FAC"].ToString();
                        //fecha = DateTime.ParseExact(F.F_COMP_CAN, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                        fecha = Convert.ToDateTime(F.F_COMP_CAN.ToString());
                        F.F_COMP_CAN = fecha.ToString("dd/MM/yyyy H:mm:ss");
                        F.N_COMP = dtFac.Rows[i]["NCOMP_V"].ToString();
                        //if(dtFac.Rows[i]["MON_CTE"].ToString().Equals("1"))
                        //{
                        //    F.IMPORTE = ((decimal)dtFac.Rows[i]["IMPORTE"] / (decimal)dtFac.Rows[i]["COTIZ"]).ToString().Replace(",", ".");
                        //    F.IMPORTE_GR = ((decimal)dtFac.Rows[i]["IMPORTE_GR"] / (decimal)dtFac.Rows[i]["COTIZ"]).ToString().Replace(",", ".");
                        //}
                        //else
                        //{
                            F.IMPORTE = dtFac.Rows[i]["IMPORTE"].ToString().Replace(",", ".");
                            F.IMPORTE_GR = dtFac.Rows[i]["IMPORTE_GR"].ToString().Replace(",", ".");
                        //} (B) 19-03-2020

                        F.COD_CLIENT = dtFac.Rows[i]["COD_CLIENT"].ToString();
                        F.MON_CTE = dtFac.Rows[i]["MON_CTE"].ToString();
                        F.COTIZ = dtFac.Rows[i]["COTIZ"].ToString().Replace(",", ".");
                        F.TIPO_FACTURA = F.N_COMP.Substring(0, 1);


                        db.CrearComando(@"
                        select FECHA_PED, NRO_PEDIDO from gva105
                        where NCOMP_V = '" + F.N_COMP + @"'");
                        DbDataReader drAux = db.EjecutarConsulta();
                        dtAux.Clear();
                        dtAux.Load(drAux);
                        try
                        {
                            //Llenar pedidos
                            for (int y = 0; y < dtAux.Rows.Count; y++)
                            {
                                PedidoFac P = new PedidoFac();
                                P.FECHA_PED = dtAux.Rows[y]["FECHA_PED"].ToString();
                                //fecha = DateTime.ParseExact(P.FECHA_PED, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                                fecha = Convert.ToDateTime(P.FECHA_PED.ToString());
                                P.FECHA_PED = fecha.ToString("dd/MM/yyyy H:mm:ss");
                                P.NRO_PEDIDO = dtAux.Rows[y]["NRO_PEDIDO"].ToString();
                                F.PEDIDOS.Add(P);
                            }
                        }
                        finally
                        {
                            drAux.Close();
                            //dtAux.Dispose();
                        }

                        //Llenar impuestos
                        db.CrearComando(@"select
                        COD_ALICUO
                        ,N_COMP
                        ,PORCENTAJE
                        ,ALICUOTA.TIPO_IMPUESTO
                        ,ALICUOTA.DESC_ALICUOTA
                        from gva42
                        inner join ALICUOTA
                        on ALICUOTA.COD_ALICUOTA = gva42.COD_ALICUO
                        where T_COMP = 'FAC'
                        and N_COMP = '" + F.N_COMP + @"'
                        ");

                        DbDataReader drImp = db.EjecutarConsulta();
                        dtImp.Clear();
                        dtImp.Load(drImp);

                        try
                        {
                            if (dtImp.Rows.Count > 0)
                            {
                                F.DESC_ALICUOTA = dtImp.Rows[0]["DESC_ALICUOTA"].ToString();
                                F.TIPO_IMPUESTO = dtImp.Rows[0]["TIPO_IMPUESTO"].ToString();
                                F.PORCENTAJE = dtImp.Rows[0]["PORCENTAJE"].ToString();
                            }
                            else F.IMPORTE_GR = F.IMPORTE;

                            NCOMPANTERIOR = F.N_COMP;
                            cFacturas.Add(F);
                        }
                        finally
                        {
                            drImp.Close();
                            //dtImp.Dispose();
                        }
                    }
                }
            }
            finally
            {
                dr.Close();
                //dt.Dispose();
            }

            return cFacturas;
        }

        public void MarcarFiller(BaseDeDatos db, string comando)
        {
            db.CrearComando(comando);
            db.EjecutarComando();
        }

        public List<RetornoRecibo> EnviarRecibos(List<Recibo> cRecibos)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
               delegate { return true; }
            );
            //URL del webservice del BAEnergy
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost/RecibirConsultas"); //CAMBIAR
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://bundat102/WEBS/GuardarRecibosWS.php");
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://190.210.251.35:8082/WEBS/GuardarRecibosWS.php");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //string obj = "{ \"Recibos\": [";
                //foreach (Recibo R in cRecibos)
                //{
                //    obj += "     { ";
                //    obj += "     \"COD_CLIENT\": \"" + R.COD_CLIENT + "\",";
                //    obj += "     \"N_COMP\": \"" + R.N_COMP + "\",";
                //    obj += "     \"FECHA_EMIS\": \"" + R.FECHA_EMIS + "\",";
                //    obj += "     \"IMPORTE\": \"" + R.IMPORTE + "\",";
                //    obj += "     \"MON_CTE\": \"" + R.MON_CTE + "\",";
                //    obj += "     \"COTIZ\": \"" + R.COTIZ + "\",";
                //    obj += "     \"N_COMP_REL\": \"" + R.N_COMP_REL + "\",";
                //    obj += "     \"T_COMP_CAN\": \"" + R.T_COMP_CAN + "\",";
                //    obj += "     \"IMPORTE_CAN\": \"" + R.IMPORTE_CAN + "\",";
                //    obj += "     \"T_RETEN\": \"" + R.T_RETEN + "\",";
                //    obj += "     \"IMPORTE_RETEN\": \"" + R.IMPORTE_RETEN + "\"";
                //    obj += "     },";
                //}
                //obj = obj.TrimEnd(',');
                //obj += "]}";

                //Serializar el objeto a enviar. Para esto uso la libreria Newtonsoft
                string sb = JsonConvert.SerializeObject(cRecibos);

                //Convertir el objeto serializado a arreglo de byte
                Byte[] bt = Encoding.UTF8.GetBytes(sb);

                streamWriter.Write(sb);
                streamWriter.Flush();
                streamWriter.Close();
            }

            List<RetornoRecibo> Retor = new List<RetornoRecibo>();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Retor = JsonConvert.DeserializeObject<List<RetornoRecibo>>(result);
                return Retor;
            }
        }

        public List<RetornoFactura> EnviarFacturas(List<Factura> cFacturas)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate { return true; }
            );
            //URL del webservice del BAEnergy
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost/RecibirFacturas"); //CAMBIAR
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://bundat102/WEBS/GuardarFacturasWS.php");
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://190.210.251.35:8082/WEBS/GuardarFacturasWS.php");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //string obj = "{ \"Recibos\": [";
                //foreach (Recibo R in cRecibos)
                //{
                //    obj += "     { ";
                //    obj += "     \"COD_CLIENT\": \"" + R.COD_CLIENT + "\",";
                //    obj += "     \"N_COMP\": \"" + R.N_COMP + "\",";
                //    obj += "     \"FECHA_EMIS\": \"" + R.FECHA_EMIS + "\",";
                //    obj += "     \"IMPORTE\": \"" + R.IMPORTE + "\",";
                //    obj += "     \"MON_CTE\": \"" + R.MON_CTE + "\",";
                //    obj += "     \"COTIZ\": \"" + R.COTIZ + "\",";
                //    obj += "     \"N_COMP_REL\": \"" + R.N_COMP_REL + "\",";
                //    obj += "     \"T_COMP_CAN\": \"" + R.T_COMP_CAN + "\",";
                //    obj += "     \"IMPORTE_CAN\": \"" + R.IMPORTE_CAN + "\",";
                //    obj += "     \"T_RETEN\": \"" + R.T_RETEN + "\",";
                //    obj += "     \"IMPORTE_RETEN\": \"" + R.IMPORTE_RETEN + "\"";
                //    obj += "     },";
                //}
                //obj = obj.TrimEnd(',');
                //obj += "]}";

                //Serializar el objeto a enviar. Para esto uso la libreria Newtonsoft
                string sb = JsonConvert.SerializeObject(cFacturas);

                //Convertir el objeto serializado a arreglo de byte
                Byte[] bt = Encoding.UTF8.GetBytes(sb);

                streamWriter.Write(sb);
                streamWriter.Flush();
                streamWriter.Close();
            }

            List<RetornoFactura> Retor = new List<RetornoFactura>();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Retor = JsonConvert.DeserializeObject<List<RetornoFactura>>(result);
                return Retor;
            }
        }

        public void ConsumirBARecibos(BaseDeDatos db, System.IO.StreamWriter log)
        {
            //Consulta si hay recibos nuevos
            try
            {
                string comando;
                string marca;
                List<Recibo> cRecibos = ConsultarRecibos(db, log);
                log.WriteLine("Recibos - Fase 1 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                if (cRecibos.Count != 0)
                {
                    //se lo enviamos a baenergy y consumimos su webservice
                    List<RetornoRecibo> Retor = EnviarRecibos(cRecibos);
                    log.WriteLine("Recibos - Fase 2 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                    foreach (RetornoRecibo R in Retor)
                    {
                        log.WriteLine("Recibos - Fase 3 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                        log.WriteLine("Recibos - Fase 4 - "+ R.estado + " " + DateTime.Now.ToString("hh:mm:ss"));
                        if (R.estado) //r.estado.Equals("1");
                        {//si devuelve ok
                            marca = DateTime.Now.ToString("yyyy'-'mm'-'dd't'hh':'mm':'ss");
                            comando = @"update gva12 set filler='" + marca + "' where n_comp = '" + R.N_COMP + @"' and t_comp='REC'";
                            MarcarFiller(db, comando);
                            log.WriteLine("Recibos - Fase 4 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLine("Recibos - Fase Fallida " + ex.Message + " " + DateTime.Now.ToString("hh:mm:ss"));
            }
        }

        public void ConsumirBAFacturas(BaseDeDatos db, System.IO.StreamWriter log)
        {
            //Consultar si hay facturas nuevas
            //System.IO.StreamWriter log = new System.IO.StreamWriter(@"C:\Users\bruno\source\repos\BAEnergy-API\Probador\bin\Debug\FacturasLog.txt", true);
            //System.IO.StreamWriter log = new System.IO.StreamWriter(@"C:\Gestion\BAEnergy-API\FacturasLog.txt", true);
            try
            {
                string marca;
                string comando;
                List<Factura> cFacturas = ConsultarFacturas(db);

                log.WriteLine("Facturas - Fase 1 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                if (cFacturas.Count != 0)
                {
                    //Se lo enviamos a BAEnergy y consumimos su webservice
                    List<RetornoFactura> Retor = EnviarFacturas(cFacturas);
                    log.WriteLine("Facturas - Fase 2 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                    foreach (RetornoFactura R in Retor)
                    {
                        log.WriteLine("Facturas - Fase 3 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                        log.WriteLine("Facturas - Fase 4 - " + R.estado + " " + DateTime.Now.ToString("hh:mm:ss"));
                        if (R.estado) //Si vuelve sin errores marca las facturas (Estado "true") r.estado.Equals("1");
                        {
                            marca = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            comando = @"UPDATE GVA12 SET FILLER='" + marca + "' WHERE N_COMP = '" + R.N_COMP + @"' AND T_COMP='FAC'";
                            MarcarFiller(db, comando);
                            log.WriteLine("Facturas - Fase 4 - Completada " + DateTime.Now.ToString("hh:mm:ss"));
                        }
                    }
                }
                //No hay facturas nuevas
            }
            catch(Exception ex)
            {
                log.Write("Facturas - Fase Fallida " + ex.Message + " " + DateTime.Now.ToString("hh:mm:ss")); ;
            }
        }
    }
}
