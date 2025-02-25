public class Solucion : IComparable<Solucion> // Clase Solucion que implementa la interfaz IComparables
{
    public List<(int fila, int columna)> Coords {get; set; } // Lista de posiciones de las Reinas
    public int Coste {get; set;}  //coste acumulado de la solucion

    // Constructor con parametro opcional para la sobrecarga
    public Solucion(List<(int,int)> coords) : this(coords, 0) {} //Coste = 0 por defecto

    public Solucion(List<(int, int)> coords, int coste=0)  
    {
        Coords = new List<(int, int)>(coords);
        this.Coste = coste;
    }
  
    //Comparacion de costes para ordenar en la cola de prioridad
    public int CompareTo(Solucion otra) // -1 si es menor , 0 si son iguales y 1 si es mayor 
    {
        return Coste.CompareTo(otra.Coste);
    }
    
    // Evita estados repetidos comparando coordenadas
    public override bool Equals (object obj)
    {
        if (obj is Solucion otra)
            return string.Join("-", Coords) == string.Join("-", otra.Coords);
        return false;
    }

    // Obtiene el numero de de hash de la solucion. Objetivo buscar ****
    public override int GetHashCode()
    {
        return string.Join("-", Coords).GetHashCode();
    }
    
    public override string ToString()
    {
        return string.Join("-", Coords.Select(c=> $"({c.fila}, {c.columna}"));
    }

}