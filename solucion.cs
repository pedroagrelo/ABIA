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
}
