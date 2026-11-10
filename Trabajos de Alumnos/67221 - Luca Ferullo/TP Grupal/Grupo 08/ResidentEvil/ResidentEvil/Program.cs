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
            Console.WriteLine(new string('=', 30));
            Console.WriteLine("---- RESIDENT EVIL ----");
            Console.WriteLine(new string('=', 30));
            Console.WriteLine("Ingresa el nombre del personaje:");
            nombre = Console.ReadLine();
            return nombre;
        }
    }
        public class HierbaVerde : ConsumibleSurvival
        {
            public int vida { get; set; } = 100;
            public bool estadoAlterado { get; set; } = true;

            public virtual int Consumir()
            {
                Console.WriteLine("Consiguió hierba verde.");
                vida += 40;
                estadoAlterado = false;
            return vida;
            }
        }

        public class SprayPrimerosAuxilios : ConsumibleSurvival
        {
            public int sprays { get; set; } = 1;
        public int vida { get; set; } = 100;
        
            
            public int RegSpray(int sprays, int vida)
            {
            if (sprays > 0)
            {
                Console.WriteLine("Usó el spray de primeros auxilios.");
                vida = 100;
                sprays--;
            }

            else
            {
                Console.WriteLine("No queda sprays.");
            }

            return vida;
            }
        }

    partial class Superviviente : HierbaVerde
    {

        public override int Consumir()
        {
            Console.WriteLine("Vida: " + vida);

            if (!estadoAlterado)
            {
                Console.WriteLine("No se encuentra afectado.");
                
            }

            else
            {
                Console.WriteLine("Se encuentra afectado.");
                Console.WriteLine("Usando hierba verde.");

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(".");
                    
                }

                vida += 40;
                estadoAlterado = false;

                Console.WriteLine("Vida en total: " + vida);
            }

            return vida;

            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Superviviente s = new Superviviente();

            string nomPersonaje = s.Nombre();

            Console.WriteLine("Personaje seleccionado: " + nomPersonaje);

            Console.WriteLine("Consumir hierba verde.");
            s.Consumir();

            Console.WriteLine("Selecciona alguna tecla para finalizar con el programa.");

            Console.ReadKey();
        }
    }
}
