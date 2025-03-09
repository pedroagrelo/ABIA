using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PRACTICASEMANA1;


public class Solucion : IComparable<Solucion> 
{

    public List<(int fila, int columna)> Coords { get; set; } 
    public int Coste { get; set; }  

    public Solucion(List<(int fila, int columna)> coords) : this(coords, 0) {} 

    public Solucion(List<(int fila, int columna)> coords, int coste = 0)  
    {
        Coords = new List<(int fila, int columna)>(coords);
        Coste = coste;
    } 

    public int CompareTo(Solucion otra)
    {
        return Coste.CompareTo(otra.Coste);
    }

    public override bool Equals(object obj)
    {
        if (obj is Solucion otra)
            return string.Join("-", Coords) == string.Join("-", otra.Coords);
        return false;
    }

    public override int GetHashCode()
    {
        return string.Join("-", Coords).GetHashCode();
    }

    public override string ToString()
    {
        return string.Join("-", Coords.Select(c => $"({c.fila}, {c.columna})"));
    }
    // Método para verificar si el estado es consistente
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
