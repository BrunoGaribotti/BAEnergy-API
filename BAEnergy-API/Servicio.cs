using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using BaseDatos;
using System.Threading;
using System.ServiceModel.Web;
using Negocio.PlantillasRecibidas;
using System.IO;

namespace BAEnergy_API
{
    public partial class Servicio : ServiceBase
    {
        WebServiceHost host = new WebServiceHost(typeof(wsServicio), new Uri("http://localhost:9000/"));
        public Procesos oProc = new Procesos();

        Thread procPedidos;

        public Servicio()
        {
            InitializeComponent();
        }

        private void tProcPedidos()
        {
            BaseDeDatos db = new BaseDeDatos();
            //Leer datos del XML (config.xml)
            string msgXML = "";
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()).Remove(0, 6) + "\\CONFIG\\CONFIG.XML"); //Cambiar
                BaseDeDatos.cadenaConexion = ds.Tables[0].Rows[0]["CADENA"].ToString();
                db.Configurar("");
                db.Conectar();
                msgXML = "XML - Lectura de datos correcta. " + DateTime.Now.ToString("hh:mm:ss");
            }
            catch (Exception ex)
            {
                msgXML = "XML - Error al leer los datos. " + DateTime.Now.ToString("hh:mm:ss");
            }

            while (true)
            {
                System.IO.StreamWriter log = new System.IO.StreamWriter(@"C:\Gestion\BAEnergy-API\Log.txt", true); //CAMBIAR
                //System.IO.StreamWriter log = new System.IO.StreamWriter(@"C:\Users\bruno\source\repos\BAEnergy-API\Probador\bin\Debug\Log.txt", true);
                log.WriteLine(msgXML);
                try { oProc.ConsumirBARecibos(db, log); }
                catch (Exception e)
                {
                    log.WriteLine("ConsumirBARecibos falló. Error: " + e.Message);
                }
                try { oProc.ConsumirBAFacturas(db, log); }
                catch (Exception e)
                {
                    log.WriteLine("ConsumirBAFacturas falló. Error: " + e.Message);
                }
                finally { log.Close(); }
                Thread.Sleep(300000); //CAMBIAR

                //Eliminar archivo txt y crearlo de nuevo
                try
                {
                    //Check if file exists with its full path
                    if (File.Exists(@"C:\Gestion\BAEnergy-API\Log.txt"))
                    {
                        // If file found, delete it    
                        //File.Delete("Log.txt");
                        File.Delete(@"C:\Gestion\BAEnergy-API\Log.txt");
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        public void Iniciar()
        {       
            procPedidos = new Thread(new ThreadStart(tProcPedidos));
            procPedidos.IsBackground = true;
            procPedidos.Start();
            host.Open();
        }
        protected override void OnStart(string[] args)
        {
            Iniciar();
        }

        protected override void OnStop()
        {
        }
    }
}
