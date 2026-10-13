using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TP POO.
  Temática: Resident Evil.
  Alumno: Luca Ferullo.
  Nro de legajo: 67221.
  Comisión 1. */

namespace ResidentEvil
{

    public class ConsumibleSurvival
    {
        private string nombre;
        public  string  Nombre()
        {
            Console.WriteLine("Ingresa el nombre del personaje:");
            nombre = Console.ReadLine();
            return nombre;
        }
    }
        public class HierbaVerde : ConsumibleSurvival
        {
            public int vida = 100;
            public bool estadoAlterado = true;

            public virtual void Consumir(int vida, bool estadoAlterado)
            {
                Console.WriteLine("Consiguió hierba verde.");
                vida += 40;
                estadoAlterado = false;
            }
        }

        public class SprayPrimerosAuxilios : ConsumibleSurvival
        {
            public int sprays = 1;
        
            
            public int RegSpray(int sprays, int vida)
            {
            return 0;
            }
        }

    partial class Superviviente : HierbaVerde
    {

        public override void Consumir(int vida, bool estadoAlterado)
        {
            Console.WriteLine("Vida: " + vida);

            if (estadoAlterado == true)
            {
                Console.WriteLine("No se encuentra afectado.");
            }

            else
            {
                Console.WriteLine("Se encuentra afectado.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsumibleSurvival P = new ConsumibleSurvival();

            string nomPersonaje = P.Nombre();
        }
    }
}
