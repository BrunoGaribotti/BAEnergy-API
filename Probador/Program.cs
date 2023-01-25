using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using BAEnergy_API;

namespace Probador
{
    class Program
    {
        static void Main(string[] args)
        {
            Servicio cw = new Servicio();
            cw.Iniciar();
            Console.WriteLine("Servicio BAEnergy Activado.");
            Console.WriteLine();
            Console.WriteLine("<Presione una tecla para Salir>");
            Console.ReadKey();
        }
    }
}
