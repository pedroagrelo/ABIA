using System;

namespace PracticaSemana1;

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
