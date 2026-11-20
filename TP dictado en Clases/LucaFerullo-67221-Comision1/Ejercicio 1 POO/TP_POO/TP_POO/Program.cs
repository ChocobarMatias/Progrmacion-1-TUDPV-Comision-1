using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    /* TP de POO de una clase (version C# puro).
     Luca Ferullo 67221. */

    public class Personaje
    {
        private string nombre;
        
        public string Nombrar()
        {
            Console.WriteLine("Establece el nombre del personaje:");
            nombre = Console.ReadLine();

            return nombre;
        }

    }

    public class Salud : Personaje
    {
        private int salud;
        private int saludMax = 200;

        public int ConsultarSalud()
        {
            Console.WriteLine("Establece tu salud actual:");
            salud = int.Parse(Console.ReadLine());

            if (salud >= saludMax)
            {
                salud = saludMax;
            }

            Console.WriteLine("Salud actual: " + salud);

            return salud;

        }
    }

    public class Escudo : Personaje
    {
        public int escudo { get; set; } = 100;
        public bool tEscudo { get; set; }
        public string op;

        public string InfoEscudo()
        {
            Console.WriteLine("¿Tienes escudo?");
            op = Console.ReadLine();

            while (op != "Si" && op != "si" && op != "No" && op != "no")
            {
                Console.WriteLine("OYE!");
                Console.WriteLine("Es necesario responder esta pregunta.");
                Console.WriteLine("Por favor, asegurese si tiene o no escudo.");
                op = Console.ReadLine();
            }

            if (op == "Si" && op == "si")
            {
                tEscudo = true;

                Console.WriteLine("Si tiene escudo.");
            }

            else if (op == "No" && op == "no")
            {
                escudo -= 100;
                tEscudo = false;

                Console.WriteLine("No tiene escudo.");

            }

            return op;
        }
    }

    public class Danio : Escudo
    {
        public int danio;
        public bool tEscudo { get; set; }
        private int salud { get; set; }
        public int escudo;
        public bool estaVivo;

        public int Proteger()
        {
            if (tEscudo == true)
            {
                Console.WriteLine("Se encuentra protegido.");
                escudo -= 50;
                Console.WriteLine("Se usó el escudo.");
                Console.WriteLine("Escudo: " + escudo);
            }

            else if (tEscudo == false)
            {
                Console.WriteLine("Se obtiene escudo.");
                escudo += 100;
                Console.WriteLine("Escudo: " + escudo);
            }

            return escudo;
            
        }

        public int RecibirDanio()
        {
            if (tEscudo == true)
            {
                danio = 15;

                Console.WriteLine("Daño recibido: " + danio);

                salud = salud - danio;

                Console.WriteLine("Salud en total: " + salud);
            }

            else if (tEscudo == false)
            {
                danio = 55;

                Console.WriteLine("Daño recibido: " + danio);

                salud = salud - danio;

                Console.WriteLine("Daño recibido: " + danio);
            }

            return danio;
            return salud;
        }

        public bool EstadoJugador()
        {
            if (salud >= 0)
            {
                estaVivo = true;
                Console.WriteLine("Estas vivo.");
            }

            else if (salud <= 0)
            {
                estaVivo = false;
                Console.WriteLine("Estas muerto.");
            }

            return estaVivo;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
           
            Personaje pj = new Personaje();

            string P = pj.Nombrar();

            Console.WriteLine("Personaje: " + P);

            Salud ps = new Salud();

            int sal = ps.ConsultarSalud();

            Escudo ep = new Escudo();

            string opEsc = ep.InfoEscudo();

            Danio dp = new Danio();

            int proc = dp.Proteger();

            int dan = dp.RecibirDanio();

            bool vm = dp.EstadoJugador();



            Console.WriteLine("Presiona cualquier tecla para finalizar el programa.");
            Console.ReadKey();
        }
    }
}
