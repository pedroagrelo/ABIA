// Hugo Garabatos y Pedro Agrelo 


using System;
using System.Collections.Generic;


namespace PracticaSemana1;

/// <summary>
/// Clase abstracta que define la estructura básica para un algoritmo de búsqueda.
/// Contiene métodos para calcular la prioridad y ejecutar la búsqueda.
/// </summary>
public abstract class AlgoritmoDeBusqueda
{
    protected IListaCandidatos ListaCandidatos; // Lista de candidatos para la búsqueda

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
                               out int revisados,Func<Solucion, int>? calculoHeuristica = null)  // revisados obligatorio calculo heuiristica opcional 
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
                // Console.WriteLine($"Nodos evaluados: {revisados}");
                return actual;
            }

            List<Solucion> vecinos = obtenerVecinos(actual);
            foreach (Solucion vecino in vecinos)
            {
                if (!vistos.ContainsKey(vecino.ToString())) //Si el vecino no había sido visitado
                {
                    vecino.Coste = actual.Coste + calculoCoste(actual, vecino); //coste acumulado 
                    int prioridad = CalculoDePrioridad(vecino, calculoHeuristica); //segun coste y heuristica 
                    ListaCandidatos.Anhadir(vecino, prioridad); //Actualizamos coste, prioridad y añadimos a candidatos
                }
            }
        }

        return null;  // Si no se encuentra solución
    }
}

