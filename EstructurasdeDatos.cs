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
    private PriorityQueue<(Solucion solucion, string estado, int orden), int> cp; // PriorityQueue actuando como pila
    private Dictionary<string, (Solucion solucion, string estado)> buscador; // Rastreador de estados
    private const string REMOVED = "<removed-task>"; // Estado de nodo eliminado
    private int contador = int.MaxValue; // Contador decreciente para simular pila

    /// <summary>
    /// Constructor que inicializa la cola de prioridad y el diccionario de búsqueda.
    /// </summary>
    public ColaDePrioridad()
    {
        cp = new PriorityQueue<(Solucion, string, int), int>();
        buscador = new Dictionary<string, (Solucion, string)>();
    }

    /// <summary>
    /// Añade una solución a la cola de prioridad simulando comportamiento de pila.
    /// </summary>
    public void Anhadir(Solucion solucion, int prioridad = 0)
    {
        string key = solucion.ToString();
        if (buscador.ContainsKey(key)) // Si la solución ya existe
        {
            if (buscador[key].estado != REMOVED)
                return; // No hacer nada si el nodo sigue activo
            Borrar(solucion); // Eliminar solución si ya está en la cola
        }

        // La prioridad disminuye con cada inserción, simulando el comportamiento de LIFO
        (Solucion solucion, string estado, int orden) entrada = (solucion, "activo", contador--);
        cp.Enqueue(entrada, entrada.orden); // Menor número = mayor prioridad (comportamiento de pila)
        buscador[key] = (solucion, "activo");
    }

    /// <summary>
    /// Marca una solución como eliminada en la cola de prioridad.
    /// </summary>
    public void Borrar(Solucion solucion)
    {
        string key = solucion.ToString();
        if (buscador.ContainsKey(key))
        {
            (Solucion solucion, string estado) entrada = buscador[key];
            buscador[key] = (entrada.solucion, REMOVED);
        }
    }

    /// <summary>
    /// Obtiene la siguiente solución activa de la cola de prioridad.
    /// </summary>
    public Solucion ObtenerSiguiente()
    {
        while (cp.Count > 0)
        {
            (Solucion solucion, string estado, int orden) = cp.Dequeue(); // Extrae el elemento con mayor prioridad
            if (estado != REMOVED && buscador.ContainsKey(solucion.ToString()))
            {
                buscador.Remove(solucion.ToString());
                return solucion; // Devuelve el nodo más recientemente insertado
            }
        }
        throw new InvalidOperationException("No hay más soluciones en la cola.");
    }

    /// <summary>
    /// Devuelve el número de soluciones activas en la cola de prioridad.
    /// </summary>
    public int Contar
    {
        get
        {
            int count = 0;
            foreach ((Solucion solucion, string estado) item in buscador.Values)
            {
                if (item.estado != REMOVED)
                    count++;
            }
            return count;
        }
    }
}
