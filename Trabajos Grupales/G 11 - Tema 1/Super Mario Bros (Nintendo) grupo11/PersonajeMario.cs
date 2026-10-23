// See https://aka.ms/new-console-template for more information


using System;






namespace Grupo_11
{
    public abstract class PersonajeMario
    {
        // Atributos protegidos (Encapsulamiento)
        protected string nombre;
        protected int vidas;
        protected int monedas;

        // Propiedad Vidas con validación mediante 'value'
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

        // Constructor parametrizado con uso explícito de 'this'
        public PersonajeMario(string nombre, int vidas, int monedas)
        {
            this.nombre = nombre;
            this.Vidas = vidas;
            this.monedas = monedas;
        }

        // Método virtual para polimorfismo :0
        public virtual void Saltar()
        {
            Console.WriteLine($"{this.nombre} realiza un salto estándar.");
        }

        // Método para recibir daño :)
        public void RecibirDanio()
        {
            Console.WriteLine($"{this.nombre} ha recibido daño.");
            this.Vidas--;
        }
    }

    









}
