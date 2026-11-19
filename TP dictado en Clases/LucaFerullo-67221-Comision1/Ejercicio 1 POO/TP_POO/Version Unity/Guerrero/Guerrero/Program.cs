using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Guerrero : MonoBehaviour
{
    private string nombre;
    private int vidaMax;
    private int vidaActual;
    private int escudo;

    
    public string Nombre
    {
        get { return nombre; }
    }

    public int VidaActual
    {
        get { return vidaActual; }
        private set
        {
            
            if (value < 0)
            {
                vidaActual = 0;
            }
            else if (value > vidaMax)
            {
                vidaActual = vidaMax;
            }
            else
            {
                vidaActual = value;
            }
        }
    }

    public int Escudo
    {
        get { return escudo; }
    }

    
    public bool EstaVivo
    {
        get { return vidaActual > 0; }
    }

    
    public GuerreroLogico(string nombre, int vidaMax, int escudoInicial)
    {
        
        this.nombre = nombre;
        this.vidaMax = vidaMax;
        this.escudo = escudoInicial;

        
        this.vidaActual = vidaMax;
    }

   
    public void RecibirDanio(int cantidad)
    {
        
        if (!EstaVivo)
        {
            return;
        }

       
        if (escudo >= cantidad)
        {
            escudo -= cantidad;
        }
       
        else
        {
            int remanente = cantidad - escudo;
            escudo = 0;

            
            VidaActual -= remanente;
        }
    }

    public void Curar(int cantidad)
    {
       
        if (EstaVivo)
        {
            VidaActual += cantidad;
        }
    }
}
