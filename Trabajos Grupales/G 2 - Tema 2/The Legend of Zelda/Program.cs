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

    
    // Santillan Jorge Alfredo - 67125 ====================================================================
    // CLASE HIJA: EspadaMaestra
    // ====================================================================================================


    public class EspadaMaestra : ItemZelda
    {
        // Campo privado que guarda el daño sagrado del arma
        private int danioSagrado;
        // Propiedad pública con validación: no permite valores menores o iguales a cero
        public int DanioSagrado
        {
            get { return this.danioSagrado; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(danioSagrado), "El daño sagrado debe ser mayor que cero");
                }
                this.danioSagrado = value;
            }
        }
        // Base, ejecuta el constructor de ItemZelda, y le pasa los parametros nombre y durabilidad, mientras que el parametro danioSagrado se asigna a la propiedad DanioSagrado
        public EspadaMaestra(string nombre, int durabilidad, int danioSagrado) : base(nombre, durabilidad)
        {
            this.DanioSagrado = danioSagrado;
        }
        // Sobrescritura del método virtual
        public override void Usar(Link link)
        {
            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));
            }

            if (!this.IniciarUso())
            {
                return;
            }

            if (link.TieneSaludMaxima())
            {
                Console.WriteLine(link.Nombre + " Utiliza la Espada Maestra y lanza rayos de energia sagrada que causan " + this.DanioSagrado + " puntos de daño" );
            }
            else
            {
                Console.WriteLine(link.Nombre + " Ataca con la Esapda Maestr y no puede lanzar rayos porque no tiene salud maxima, causando " + this.DanioSagrado + " puntos de daño");
            }
        }
        
    }
    // ====================================================================================================
    //  CLASE HIJA: ArcoFuerza
    // ====================================================================================================

    public class ArcoFuerza : ItemZelda
    {
        // Campo privado que lleva la cuenta de las flechas disponibles
        private int flechasRestantes;
        // Propiedad pública con validación: impide que las flechas queden en negativo
        public int FlechasRestantes
        {
            get { return this.flechasRestantes; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El número de flechas restantes no puede ser negativo")
                }

                this.flechasRestantes = value;
            }
        }
        // Base, ejecuta el constructor de ItemZelda
        public ArcoFuerza (string nombre, int durabilidad, int flechasRestantes) : base(nombre, durabilidad)
        {
            this.FlechasRestantes = flechasRestantes;
        }
        // Sobrescritura del método virtual
        public override void Usar(Link link)
        {
            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));
            }
            // Validamos las flechas antes de utilizar el arco
            if (this.FlechasRestantes <= 0)
            {
                Console.WriteLine(link.Nombre + " No tiene flechas para disparar con el Arco de Fuerza");
                return;
            }
            if (!this.IniciarUso())
            {
                return;
            }

            this.FlechasRestantes--;

            Console.WriteLine(link.Nombre + " Dispara una flecha con el Arco de Fuerza, causando daño a su objetivo. Flechas restantes: " + this.FlechasRestantes);
        }
    }
}
