using System;
using System.Collections.Generic;
namespace PRACTICASEMANA1;

// Clase abstracta que define la estructura básica para un algoritmo de búsqueda
public abstract class AlgoritmoDeBusqueda
{
    protected IListaCandidatos ListaCandidatos;

    public AlgoritmoDeBusqueda(IListaCandidatos lista)
    {
        ListaCandidatos = lista;
    }

    //Cálculo de la prioridad de un nodo según su coste y heuristica
    public abstract int CalculoDePrioridad(Solucion nodoInfo, Func<Solucion, int>? calculoHeuristica = null); 

    //Método de búsqueda general
    public Solucion? Busqueda(Solucion solucionInicial, Func<Solucion, bool> criterioParada,
                               Func<Solucion, List<Solucion>> obtenerVecinos, Func<Solucion, Solucion, int> calculoCoste,
                               out int revisados,Func<Solucion, int>? calculoHeuristica = null) 
    {
        ListaCandidatos.Anhadir(solucionInicial, 0); //coste inicial 0
        Dictionary<string, int> vistos = new(); //Diccionario con nodos vistos
        revisados = 0;  //Nº de nodos revisados

        while (ListaCandidatos.Contar > 0) //Mientras haya candidatos
        {
            Solucion actual = ListaCandidatos.ObtenerSiguiente(); 
            vistos[actual.ToString()] = actual.Coste; //obtenemos el siguiente nodo y marcamos como visto
            revisados++;

            if (criterioParada(actual)) //Si se cumple el criterio se devuelve la solucion
            {
                return actual;
            }

            List<Solucion> vecinos = obtenerVecinos(actual);
            foreach (Solucion vecino in vecinos)
            {
                if (!vistos.ContainsKey(vecino.ToString())) //Si el vecino no había sido visitado
                {
                    vecino.Coste = actual.Coste + calculoCoste(actual, vecino);
                    int prioridad = CalculoDePrioridad(vecino, calculoHeuristica);
                    ListaCandidatos.Anhadir(vecino, prioridad); //Actualizamos coste, prioridad y añadimos a candidatos
                }
            }
        }

        return null;  // Si no se encuentra solución
    }
}


public class AEstrella : AlgoritmoDeBusqueda
{
    public AEstrella() : base(new ColaDePrioridad()) {} // Inicializamos con cola de prioridad vacia
    public override int CalculoDePrioridad(Solucion nodoInfo, Func<Solucion, int>? calculoHeuristica = null)
    {
        // Si no se proporciona una heurística, solo devuelve el coste
        if (calculoHeuristica == null)
        {
            return nodoInfo.Coste;  
        }
        // Si hay una heurística, suma el coste acumulado y la heurística
        return nodoInfo.Coste + calculoHeuristica(nodoInfo);
    }

}

public class BusquedaEnProfundidad : AlgoritmoDeBusqueda
{
    public BusquedaEnProfundidad() : base(new PiladeCandidatos()) {} // Inicializamos busqueda en profundidad con pila vacía
    public override int CalculoDePrioridad(Solucion nodoInfo, Func<Solucion, int>? calculoHeuristica = null)
    {
        return 0; // En Búqueda en profundidad no hay heurística 
    }
}

/// <summary>
/// Implementación de la búsqueda en anchura, que expande nodos en orden FIFO.
/// </summary>
public class BusquedaAnchura : AlgoritmoDeBusqueda
{
    /// <summary>
    /// Constructor que inicializa la búsqueda en anchura con una cola FIFO.
    /// </summary>
    public BusquedaAnchura() : base(new ColaFIFO()) {}

    /// <summary>
    /// En búsqueda en anchura, todos los nodos tienen la misma prioridad.
    /// </summary>
    /// <param name="nodoInfo">El nodo cuya prioridad se calcula.</param>
    /// <param name="calculoHeuristica">Función heurística opcional (no utilizada en anchura).</param>
    /// <returns>Siempre devuelve 0, ya que no se usa heurística.</returns>
    public override int CalculoDePrioridad(Solucion nodoInfo, Func<Solucion, int>? calculoHeuristica = null)
    {
        return 0; // En anchura no hay prioridad diferenciada, todos los nodos se procesan FIFO.
    }
}

