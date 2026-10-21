// See https://aka.ms/new-console-template for more information


using System;






namespace Grupo_11
{
    public abstract class PersonajeMario
    {
        
        protected string nombre;
        protected int vidas;
        protected int monedas;

        
        public int Vidas
        {
            get { return this.vidas; }
            set
            {
                if (value <= 0)
                {
                    this.vidas = 0;
                    Console.WriteLine($"[GAME OVER] {this.nombre} se ha quedado sin vidas.");
                }
                else
                {
                    this.vidas = value;
                }
            }
        }

        public string Nombre
        {
            get { return this.nombre; }
        }

        public int Monedas
        {
            get { return this.monedas; }
            set { this.monedas = value; }
        }

        
        public PersonajeMario(string nombre, int vidas, int monedas)
        {
            this.nombre = nombre;
            this.Vidas = vidas;
            this.monedas = monedas;
        }

        
        public virtual void Saltar()
        {
            Console.WriteLine($"{this.nombre} realiza un salto estándar.");
        }

        
        public void RecibirDanio()
        {
            Console.WriteLine($"{this.nombre} ha recibido daño.");
            this.Vidas--;
        }
    }

    



    public class Mario : PersonajeMario
    {
        private string estadoTransformacion;

        public Mario(string nombre, int vidas, int monedas, string estadoInicial = "Normal")
            : base(nombre, vidas, monedas)
        {
            this.estadoTransformacion = estadoInicial;
        }

        public override void Saltar()
        {
            Console.WriteLine($"{this.nombre} (Estado: {this.estadoTransformacion}) salta y alcanza 2 bloques de altura.");
        }
    }

    public class Luigi : PersonajeMario
    {
        public Luigi(string nombre, int vidas, int monedas)
            : base(nombre, vidas, monedas)
        {

        }

        public override void Saltar()
        {
            Console.WriteLine($"{this.nombre} salta y alcanza 3 bloques de altura. Además flota un poco por menos tracción");
        }

    }



    public class BloqueInterrogacion
    {
        private bool abierto;

        public bool Abierto
        {
            get { return this.abierto; }
        }

        public BloqueInterrogacion()
        {
            this.abierto = false;
        }

        
        public void Golpear(PersonajeMario pj)
        {
            if (this.abierto)
            {
                Console.WriteLine($"El bloque ya fue abierto previamente. {pj.Nombre} no obtiene nada.");
            }
            else
            {
                this.abierto = true;
                pj.Monedas += 5;
                Console.WriteLine($"¡{pj.Nombre} golpeó el bloque '?'! Consigue 5 monedas (Total: {pj.Monedas} monedas).");
            }
        }
    }





    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(" //////DEMOSTRACIÓN TEMA 1: SUPER MARIO BROS////////  ");
            Console.WriteLine("==================================================\n");

            
            Mario mario = new Mario("Mario", 3, 0, "Super");
            Luigi luigi = new Luigi("Luigi", 3, 0);

            
            PersonajeMario[] hermanos = new PersonajeMario[] { mario, luigi };

            
            BloqueInterrogacion bloque = new BloqueInterrogacion();

            
            Console.WriteLine("--- DEMOSTRACIÓN DE SALTO POLIMÓRFICO ---");
            foreach (PersonajeMario pj in hermanos)
            {
                pj.Saltar();
            }

            Console.WriteLine("\n--- INTERACCIÓN CON BLOQUE DE INTERROGACIÓN ---");
            
            bloque.Golpear(mario);

            
            bloque.Golpear(luigi);

            Console.WriteLine("\n--- DEMOSTRACIÓN DE DAÑO Y VALIDACIÓN DE VIDAS ---");
            mario.RecibirDanio();
            mario.RecibirDanio();
            mario.RecibirDanio(); 

            Console.WriteLine("\nPresione cualquier tecla para finalizar...");
            Console.ReadKey();
        }
    }

}