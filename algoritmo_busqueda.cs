using System;
using System.Collections.Generic;
namespace PRACTICASEMANA1;

/// <summary>
/// Clase abstracta que define la estructura básica para un algoritmo de búsqueda.
/// Contiene métodos para calcular la prioridad y ejecutar la búsqueda.
/// </summary>
public abstract class AlgoritmoDeBusqueda
{
    protected IListaCandidatos ListaCandidatos;

    
    /// <summary>
    /// Constructor que inicializa el algoritmo de búsqueda con una lista de candidatos.
    /// </summary>
    public AlgoritmoDeBusqueda(IListaCandidatos lista)
    {
        ListaCandidatos = lista;
    }

    /// <summary>
    /// Calcula la prioridad de un nodo de búsqueda utilizando su coste y una heurística.
    /// </summary>
    /// <param name="nodoInfo">El nodo cuya prioridad se calcula.</param>
    /// <param name="calculoHeuristica">Función heurística opcional.</param>
    /// <returns>Un valor entero representando la prioridad del nodo.</returns>

    public abstract int CalculoDePrioridad(Solucion nodoInfo, Func<Solucion, int>? calculoHeuristica = null); 

    /// <summary>
    /// Método que implementa la búsqueda general.
    /// </summary>
    /// <param name="solucionInicial">Estado inicial de la búsqueda.</param>
    /// <param name="criterioParada">Función que determina si se ha alcanzado el objetivo.</param>
    /// <param name="obtenerVecinos">Función que genera los vecinos de un nodo.</param>
    /// <param name="calculoCoste">Función que calcula el coste entre dos soluciones.</param>
    /// <param name="calculoHeuristica">Función heurística opcional.</param>
    /// <returns>La solución encontrada o null si no existe solución.</returns>
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

/// <summary>
/// Implementación del algoritmo A* que hereda de AlgoritmoDeBusqueda.
/// Utiliza una cola de prioridad para explorar los nodos de manera óptima.
/// </summary>
public class AEstrella : AlgoritmoDeBusqueda
{
    /// <summary>
    /// Constructor que inicializa el algoritmo A* con una cola de prioridad vacía.
    /// </summary>
    public AEstrella() : base(new ColaDePrioridad()) {}

    /// <summary>
    /// Calcula la prioridad de un nodo para el algoritmo A*, sumando el coste acumulado y la heurística si existe.
    /// </summary>
    /// <param name="nodoInfo">El nodo que se evalúa.</param>
    /// <param name="calculoHeuristica">Función heurística opcional.</param>
    /// <returns>Un valor entero representando la prioridad del nodo.</returns>
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

/// <summary>
/// Implementación de la búsqueda en profundidad (DFS), que expande nodos en orden LIFO 
/// Este algoritmo no utiliza una heurística, por lo que la prioridad de los nodos siempre es 0.
/// </summary>
public class BusquedaEnProfundidad : AlgoritmoDeBusqueda
{
    /// <summary>
    /// Constructor que inicializa la búsqueda en profundidad con una pila vacía.
    /// </summary>
    public BusquedaEnProfundidad() : base(new PiladeCandidatos()) {} 

    /// <summary>
    /// Calcula la prioridad de un nodo en la búsqueda en profundidad.
    /// </summary>
    /// <param name="nodoInfo">El nodo cuya prioridad se calcula.</param>
    /// <param name="calculoHeuristica">Función heurística opcional (no utilizada en la búsqueda en profundidad).</param>
    /// <returns>Siempre devuelve 0, ya que la búsqueda en profundidad no utiliza una heurística.</returns>
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

