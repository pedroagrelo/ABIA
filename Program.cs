using System;
using System.Collections.Generic;

namespace PRACTICASEMANA1;

class Program
{
    static void Main(string[] args)
    {   
        int reinas = 4; // Número de reinas a colocar
        bool continuar = true; // flag 
        while(continuar)
        {
            Console.WriteLine($"N = {reinas}  reinas");
            // Solucion inicial, nueva lista vacía sin ninguna reina 
            List<(int fila, int columna)> solucionInicial = new List<(int, int)>(); 

            int CalculoCoste(Solucion solucionActual, Solucion nuevaSolucion) => 1; // Coste uniforme de 1 para cada movimiento

            int CalculoHeuristica(Solucion solucionActual) => 0; // Heurística 0

            List<Solucion> ObtenerVecinos(Solucion solucionActual) //Posibles posiciones de la siguiente reina
            {
                int filaActual = solucionActual.Coords.Count > 0 ? solucionActual.Coords[^1].fila : -1;
                List<Solucion> vecinosPosibles = new List<Solucion>();
                if (filaActual + 1 < reinas)
                {
                    for (int columna = 0; columna < reinas; columna++)
                    {
                        //Generamos posible solución con la nueva reina
                        List<(int, int)> nuevaCoords = new List<(int, int)>(solucionActual.Coords) { (filaActual + 1, columna) }; 
                        vecinosPosibles.Add(new Solucion(nuevaCoords));

                    }
                }
                return vecinosPosibles;
            }

            bool CriterioParada(Solucion solucionActual) //Verifica que la solución es correcta
            {
                if (solucionActual.Coords.Count < reinas) return false; //Nº de reinas
                for (int i = 0; i < solucionActual.Coords.Count; i++)
                {
                    (int filaI, int columnaI) = solucionActual.Coords[i];
                    for (int j = i + 1; j < solucionActual.Coords.Count; j++)
                    {
                        (int filaJ, int columnaJ) = solucionActual.Coords[j];
                        if (columnaJ == columnaI || Math.Abs(columnaJ - columnaI) == Math.Abs(filaJ - filaI))
                            return false; //Amenazas entre reinas, si estan en la misma columna o la misma diagonal
                    }
                }
                return true;
            }

            /*Inicio de la busqueda con A*
            AEstrella algoritmoAEstrella = new AEstrella();
            Solucion? solucionFinal = algoritmoAEstrella.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, CalculoCoste, CalculoHeuristica);
            */
            //Inicio de Búsqueda en Profundidad
            BusquedaEnProfundidad busquedaEnProfundidad = new BusquedaEnProfundidad();
            Solucion? solucionFinal = busquedaEnProfundidad.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, CalculoCoste, out int revisados, CalculoHeuristica);


            if (revisados > 1500 )
            {
                Console.WriteLine(" La solución ya tiene más de 1500 nodos evaluados.");
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
            reinas ++;
        }


    }
}