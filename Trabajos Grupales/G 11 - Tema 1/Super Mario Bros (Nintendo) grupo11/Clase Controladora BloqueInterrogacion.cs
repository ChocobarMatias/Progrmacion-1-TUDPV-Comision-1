// See https://aka.ms/new-console-template for more information



using System;

namespace Grupo_11
{
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
}
