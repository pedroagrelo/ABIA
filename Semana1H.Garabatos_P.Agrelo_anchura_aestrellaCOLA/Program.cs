// Hugo Garabatos y Pedro Agrelo 

using System;
using System.Collections.Generic;

namespace PracticaSemana1;

/// <summary>
/// Clase principal que implementa la lógica del programa para resolver el problema de las N reinas usando el algoritmo A*.
/// </summary>
class Program
{   
    /// <summary>
    /// Método principal que ejecuta el programa.
    /// </summary>
    static void Main(string[] args)
    {   
         
        int reinas = 4; // Número de reinas a colocar
        bool continuar = true; // flag 
        while (continuar)
        {
            Console.WriteLine($"N = {reinas}  reinas");

            // Solucion inicial, nueva lista vacía sin ninguna reina
            List<(int fila, int columna)> solucionInicial = new List<(int, int)>(); 

            /// <summary>
            /// Función que devuelve un coste uniforme de 1 para cada movimiento.
            /// </summary>
            int CalculoCoste(Solucion solucionActual, Solucion nuevaSolucion) => -1; // Coste uniforme de 1 para cada movimiento

            /// <summary>
            /// Función heurística que devuelve siempre 0.
            /// </summary>
            int CalculoHeuristica(Solucion solucionActual) => 0; // Heurística 0

            /// <summary>
            /// Genera una lista de soluciones vecinas añadiendo una nueva reina en cada columna de la siguiente fila.
            /// </summary>
            List<Solucion> ObtenerVecinos(Solucion solucionActual) //Posibles posiciones de la siguiente reina
            {
                int filaActual = solucionActual.Coords.Count > 0 ? solucionActual.Coords[^1].fila : -1; //Si ya hay reinas colocadas, true asigna la fila de la ultima reina colocada. Flase asigna -1
                // en que fila se colocó la ultima reina, para decidir en que fila colocas la siguiente 
                List<Solucion> vecinosPosibles = new List<Solucion>();
                if (filaActual + 1 < reinas) // si todavía no se han colocado todas las reinas 
                {
                    // Añado todas las posibles columnas para la siguiente reina 
                    for (int columna = 0; columna < reinas; columna++) // itero por las columnas 
                    {
                        //Generamos posible solución con la nueva reina
                        List<(int, int)> nuevaCoords = new List<(int, int)>(solucionActual.Coords) { (filaActual + 1, columna) }; 
                        vecinosPosibles.Add(new Solucion(nuevaCoords));

                    }
                }
                return vecinosPosibles;
            }

            /// <summary>
            /// Verifica si la solución actual cumple con el criterio de parada (todas las reinas colocadas sin conflictos).
            /// </summary>
            bool CriterioParada(Solucion solucionActual) //Verifica que la solución es correcta
            {
                if (solucionActual.Coords.Count < reinas) return false; //Nº de reinas
                for (int i = 0; i < solucionActual.Coords.Count; i++)
                {
                    (int filaI, int columnaI) = solucionActual.Coords[i]; // coordenadas de la reina en i 
                    for (int j = i + 1; j < solucionActual.Coords.Count; j++)
                    {
                        (int filaJ, int columnaJ) = solucionActual.Coords[j]; // coordenadas de la reina en j 
                        if (columnaJ == columnaI || Math.Abs(columnaJ - columnaI) == Math.Abs(filaJ - filaI))
                            return false; //Amenazas entre reinas, si estan en la misma columna o la misma diagonal
                    }
                }
                return true; // sin conflictos 
            }

            /// <summary>
            /// Inicializa el algoritmo A* y ejecuta la búsqueda.
            /// </summary>
            int revisados; //  Variable para almacenar nodos evaluados
            BusquedaAnchura algoritmoAnchura = new BusquedaAnchura();
            Solucion? solucionFinal = algoritmoAnchura.Busqueda(
                new Solucion(solucionInicial), 
                CriterioParada, 
                ObtenerVecinos, 
                CalculoCoste, 
                out revisados,  
                CalculoHeuristica 
            );
            
            
            if (revisados > 1500 )
            {
                Console.WriteLine("La solución ya tiene más de 1500 nodos evaluados.");
                continuar=false; // salimos del bucle
                
            }
            else if (solucionFinal != null)
            {
                Console.WriteLine($"Nodos evaluados: {revisados}");
                Console.WriteLine($"Coordenadas: [{string.Join(", ", solucionFinal.Coords)}]");
                
            }
            else 
            {
                Console.WriteLine("No se encontró solución.");
            }
            reinas ++; // aumentamos reinas para la siguiente iteracion
        }
    }
}