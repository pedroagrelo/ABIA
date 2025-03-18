using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PRACTICASEMANA1;

/// <summary>
/// Representa una solución para el problema de las N reinas, incluyendo las coordenadas de las reinas y el coste asociado.
/// </summary>
public class Solucion : IComparable<Solucion> // Clase Solucion que implementa la interfaz IComparables
{

    public List<(int fila, int columna)> Coords { get; set; } // Lista de posiciones de las Reinas
    public int Coste { get; set; }  //coste acumulado de la solucion

    public Solucion(List<(int fila, int columna)> coords) : this(coords, 0) {} 

    /// <summary>
    /// Constructor que inicializa una solución con coordenadas y coste específicos del problema .
    /// </summary>
    public Solucion(List<(int fila, int columna)> coords, int coste = 0)  
    {
        Coords = new List<(int fila, int columna)>(coords);
        Coste = coste;
    } 

    /// <summary>
    /// Compara esta solución con otra según su coste.
    /// </summary>
    public int CompareTo(Solucion otra)
    {
        return Coste.CompareTo(otra.Coste);
    }

    /// <summary>
    /// Determina si dos soluciones son iguales comparando sus coordenadas.
    /// </summary>
    public override bool Equals(object obj)
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
        return string.Join("-", Coords.Select(c => $"({c.fila}, {c.columna})"));
    }
<<<<<<< HEAD
    // Método para verificar si el estado es consistente
=======

        // Método para verificar si el estado es consistente
>>>>>>> 98aada34f102d5d23e512692243f39f9281fed39
    public bool EsConsistente()
    {
        int n = Coords.Count;
        for (int i = 0; i < n; i++)
        {
            (int filaI, int columnaI) = Coords[i];
            for (int j = i + 1; j < n; j++)
            {
                (int filaJ, int columnaJ) = Coords[j];
                
                // Conflicto en columnas
                if (columnaI == columnaJ) return false;
                
                // Conflicto en diagonales
                if (Math.Abs(filaI - filaJ) == Math.Abs(columnaI - columnaJ)) return false;
            }
        }
        return true;
    }
}
