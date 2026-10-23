using System;

namespace Grupo_11
{
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
}
