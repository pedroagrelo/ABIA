using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PRACTICASEMANA1;


public interface IListaCandidatos
{
    void Anhadir(Solucion solucion, int prioridad = 0);
    void Borrar(Solucion solucion);
    Solucion ObtenerSiguiente(); // obtiene la siguiente solucion de la cola de prioridad 
    int Contar { get; } // cuenta soluciones de la cola
}

public class ColaDePrioridad : IListaCandidatos
{ 
    private Stack<Solucion> pila; //pila el lugar de PriorityQueue

    public ColaDePrioridad()
    {
        pila = new Stack<Solucion>();
    }
    public void Anhadir(Solucion solucion, int prioridad = 0)
    {
        pila.Push(solucion);
    }
    public void Borrar(Solucion solucion)
    {
        //No se implementa en el caso de la pila
    }
    public Solucion ObtenerSiguiente()
    {
        if (pila.Count > 0)
        
            return pila.Pop();
        throw new InvalidOperationException("No hay mas elementos en la pila");
        
    }
    public int Contar
    {
        get
        {
            return pila.Count;
        }
    }

}