using System;

namespace TheLegendOfZelda
{

    //###GRAMAJO MATIAS###
    public abstract class ItemZelda
    {
        private string nombre;
        protected int durabilidad;

        public string Nombre
        {
            get {return this.nombre;}
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre del objeto esta vacio");
                }
                this.nombre = value;
            }
        }

        public int Durabilidad
        {

            get{return this.durabilidad;}

        }
        
        //Constructor, con un if dentro en durabilidad para evitar que se ingresen datos erroneos
        protected ItemZelda(string nombre, int durabilidad)
        {
            this.nombre = nombre;

            if(durabilidad < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(durabilidad), "la durabilidad es negativa");
            
            }
            this.durabilidad = durabilidad;
        }

        //Verificacion si la durabilidad del objeto permite su uso y, en caso de que si, disminuir esta misma
        protected bool IniciarUso()
        {
            if (this.durabilidad <= 0)
            {
                Console.WriteLine("El objeto esta por romperse");
                return false;
            }
            this.durabilidad--;
            return true;
        }

        public virtual void Usar(Link link)
        {
            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));

            }

            if (!this.IniciarUso())
            {
                return;
            }

            Console.WriteLine("Usaste: {this.Nombre}.");
        }
    }

    
}
