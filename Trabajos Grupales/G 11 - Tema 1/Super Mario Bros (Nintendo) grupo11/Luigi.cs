namespace Grupo_11
{
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
}
