// Hugo Garabatos y Pedro Agrelo 

using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticaSemana1;

/// <summary>
/// Representa una solución para el problema de las N reinas, incluyendo las coordenadas de las reinas y el coste asociado.
/// </summary>
public class Solucion : IComparable<Solucion> // Clase Solucion que implementa la interfaz IComparables
{

    public List<(int fila, int columna)> Coords {get; set; } // Lista de posiciones de las Reinas
    public int Coste {get; set;}  //coste acumulado de la solucion

    /// <summary>
    /// Constructor que inicializa una solución con coordenadas y coste específicos del problema .
    /// </summary>
    public Solucion(List<(int, int)> coords, int coste=0)  
    {
        Coords = new List<(int, int)>(coords);
        this.Coste = coste;
    }
  
    /// <summary>
    /// Compara esta solución con otra según su coste.
    /// </summary>
    public int CompareTo(Solucion otra) // -1 si es menor , 0 si son iguales y 1 si es mayor 
    {
        return Coste.CompareTo(otra.Coste);
    }
    
    /// <summary>
    /// Determina si dos soluciones son iguales comparando sus coordenadas.
    /// </summary>
    public override bool Equals (object obj)
    {
        if (obj is Solucion otra)
            return string.Join("-", Coords) == string.Join("-", otra.Coords);
        return false;
    }

    /// <summary>
    /// Devuelve un código hash para esta solución basado en sus coordenadas.
    /// </summary>
    public override int GetHashCode()
    {
        return string.Join("-", Coords).GetHashCode();
    }

    /// <summary>
    /// Representa esta solución como una cadena de texto con las coordenadas de las reinas.
    /// </summary>   
    public override string ToString()
    {
        return string.Join("-", Coords.Select(c=> $"({c.fila}, {c.columna}"));
    }

}


