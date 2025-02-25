using System;

namespace PracticaSemana1;

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