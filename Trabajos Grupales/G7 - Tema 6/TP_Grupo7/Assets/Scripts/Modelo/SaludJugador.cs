namespace TP_Grupo7
{
    public class SaludJugador
    {
        public int VidaMaxima { get; private set; }
        public int VidaActual { get; private set; }

        public bool EstaMuerto => VidaActual <= 0;

        public SaludJugador(int vidaMaxima = 100)
        {
            VidaMaxima = vidaMaxima;
            VidaActual = vidaMaxima;
        }

        public void RecibirDanio(int cantidad)
        {
            if (EstaMuerto) return;
            VidaActual -= cantidad;
            if (VidaActual < 0) VidaActual = 0;
        }

        public void Curar(int cantidad)
        {
            if (EstaMuerto) return;
            VidaActual += cantidad;
            if (VidaActual > VidaMaxima) VidaActual = VidaMaxima;
        }
    }
}