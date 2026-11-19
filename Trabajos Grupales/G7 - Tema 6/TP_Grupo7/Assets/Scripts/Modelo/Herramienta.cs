using System;

namespace TP_Grupo6
{
    public abstract class Herramienta
    {
        protected string material;
        private int durabilidad;

        public int DurabilidadMaxima { get; protected set; } // Propiedad añadida para la UI (Slider)

        public int Durabilidad
        {
            get => durabilidad;
            protected set => durabilidad = value < 0 ? 0 : value;
        }

        protected Herramienta(string material, int durabilidad)
        {
            this.material = material;
            DurabilidadMaxima = durabilidad;
            Durabilidad = durabilidad;
        }

        public abstract void PicarBloque(Bloque b);

        public override string ToString() => $"{GetType().Name} ({material}) - Durabilidad: {Durabilidad}/{DurabilidadMaxima}";
    }
}