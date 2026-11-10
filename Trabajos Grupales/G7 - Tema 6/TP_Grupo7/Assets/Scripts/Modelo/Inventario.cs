using System.Collections.Generic;

namespace TP_Grupo6
{
    public class Inventario
    {
        private readonly Dictionary<string, int> items = new Dictionary<string, int>();

        public void Agregar(string item)
        {
            items[item] = Cantidad(item) + 1; // Suma 1 al ítem
        }

        public int Cantidad(string item)
        {
            return items.TryGetValue(item, out int cantidad) ? cantidad : 0; // 0 si no existe
        }
    }
}