using System;
using System.Collections.Generic;

using UnityEngine;

using Pac_Man_Main;
namespace Pac_Man_Main
{
    public class PacMan 
    {
        protected string nombre;
        protected int vidas;
        protected int puntuacion;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Vidas
        {
            get { return vidas; }
            set 
            {
                if(value >= 0)
                {
                    vidas = value;
                }
                else
                {
                    vidas = 0; //Evitamos vidas negativas
                }


            }
        }

        public int Puntuacion
        {
            get { return puntuacion; }
            set 
            { 
                if(value >= 0)
                {
                    puntuacion = value;
                }
            }
        }

       

        //Dentro del constructor inicializamos al jugador con 3 vidas
        //Dentro del constructor inicializamos la puntuacion del jugador con 0 puntos
        public PacMan(string nombre)
        {
            this.nombre = nombre;
            this.vidas = 3;
            this.puntuacion = 0;
        }

        //Metodo publico para sumar puntos cuando come un punto en el laberinto
        public void ComerPunto(int puntosExtra)
        {
            this.puntuacion = puntuacion + puntosExtra;
           Debug.Log($"{this.nombre} comio un punto.");
            Debug.Log($"Puntuacion actual: {this.puntuacion}");
        }

        //Metodo publico para que pacman pierda vida en cada encuentro con los fantasmas
        public void PerderVida()
        {
            this.vidas--;
            Debug.Log($"{this.nombre} perdio una vida");
            Debug.Log($"Vidas restantes: {this.vidas}");
        }

        // Metodo publico para cuando pacman coma la pildora de poder
        //cambie el estado vulnerable a los fantasmas
        public void ComerPildoraPoder(List<Fantasma> fantasmas)
        {
            this.puntuacion += 50;
            Debug.Log($"{this.nombre} comio la PILDORA DE PODER");
            Debug.Log($"Puntuacion actual: {this.puntuacion}");
            foreach (Fantasma f in fantasmas)
            {
                f.EstadoVulnerable = true;
            }


        }


    }
}
