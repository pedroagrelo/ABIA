using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PRACTICASEMANA1;

/// <summary>
/// Interfaz para gestionar una lista de soluciones candidatas con operaciones de añadir, borrar y obtener la siguiente solución.
/// </summary>
public interface IListaCandidatos
{
    void Anhadir(Solucion solucion, int prioridad = 0);
    void Borrar(Solucion solucion);
    Solucion ObtenerSiguiente(); // obtiene la siguiente solucion de la cola de prioridad 
    int Contar { get; } // cuenta soluciones de la cola
}

/// <summary>
/// Implementación de una cola de prioridad para gestionar las soluciones candidatas.
/// </summary>
public class ColaDePrioridad : IListaCandidatos
{
    private PriorityQueue<(Solucion solucion, string estado), int> cp; // cola de prioridad 
    private Dictionary<string, (Solucion solucion, string estado)> buscador; //diccionario para gestionar los estados
    private const string REMOVED = "<removed-task>"; // nodos eliminados

    /// <summary>
    /// Constructor que inicializa la cola de prioridad y el diccionario de búsqueda.
    /// </summary>
    public ColaDePrioridad()
    {
        cp = new PriorityQueue<(Solucion, string), int>();
        buscador = new Dictionary<string, (Solucion, string)>();
    }

    /// <summary>
    /// Añade una solución a la cola de prioridad con su respectiva prioridad.
    /// </summary>

    public void Anhadir(Solucion solucion, int prioridad = 0)
    {
        string key = solucion.ToString(); 
        if (buscador.ContainsKey(key)) // si existe solucion 
        {
            if (buscador[key].estado != REMOVED && buscador[key].estado.CompareTo(prioridad) <= 0) // no ha sido eliminada y la prioridad es menor o igual a la nueva, no es necesario añadirla de nuevo 
                return; // sale del metodo void sin añadir ningun valor 
            Borrar(solucion); // si existe con mayor prioridad, se marca eliminada  
        }
        (Solucion solucion, string estado) entrada = (solucion, "activo");
        cp.Enqueue(entrada, prioridad);
        buscador[key] = entrada;

    }
    
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
            (Solucion solucion, string estado) = cp.Dequeue(); // extrae elemento de menor prioridad 
            if (estado != REMOVED && buscador.ContainsKey(solucion.ToString()))  // si no está REMOVED y está en el diccionario
            {
                buscador.Remove(solucion.ToString()); // elimina solucion del diccionario 
                return solucion;
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


public class PiladeCandidatos : IListaCandidatos
{
    private Stack<Solucion> pila = new Stack<Solucion>(); //pila de soluciones
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
        return pila.Pop();
    }
    public int Contar
    {
        get
        {
            return pila.Count;
        }
    }
}

public class ColaFIFO : IListaCandidatos
{
    private Queue<Solucion> cola = new Queue<Solucion>();

    public void Anhadir(Solucion solucion, int prioridad = 0) => cola.Enqueue(solucion);
    public Solucion ObtenerSiguiente() => cola.Dequeue();
    public int Contar => cola.Count;
    public void Borrar(Solucion solucion) {} // no es nesario porque en una cola  no se eliminan elementos arbitrarios 
}