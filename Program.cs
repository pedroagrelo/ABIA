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

            /// <summary>
            /// Calcula una heurística que estima el número de conflictos potenciales basándose en la cantidad de reinas ya colocadas y su distribución.
            /// La heurística suma el número de conflictos actuales entre reinas y el número de reinas faltantes por colocar.
            /// Esto proporciona una estimación más informativa del costo restante para guiar la búsqueda de manera más eficiente.
            /// </summary>
            /// <param name="solucionActual">El estado actual del tablero con las reinas colocadas.</param>
            /// <returns>El valor de la heurística, que estima el costo restante para alcanzar una solución válida.</returns>
            ///sumario feito para o anterior 

            
            int CalculoHeuristica(Solucion solucionActual) 
            {
                int movimientosNecesarios = 0;

                // Calcular movimientos necesarios para resolver conflictos
                for (int i = 0; i < solucionActual.Coords.Count; i++)
                {
                    (int filaI, int columnaI) = solucionActual.Coords[i];
                    bool enConflicto = false;

                    // Verificar si la reina está en conflicto
                    for (int j = 0; j < solucionActual.Coords.Count; j++)
                    {
                        if (i == j) continue; // No comparar la reina consigo misma
                        (int filaJ, int columnaJ) = solucionActual.Coords[j];

                        if (columnaJ == columnaI || Math.Abs(columnaJ - columnaI) == Math.Abs(filaJ - filaI))
                        {
                            enConflicto = true;
                            break;
                        }
                    }

                    // Si la reina está en conflicto, estimar movimientos necesarios
                    if (enConflicto)
                    {
                        // Contar posiciones libres en la fila actual
                        int posicionesLibres = 0;
                        for (int columna = 0; columna < reinas; columna++)
                        {
                            bool posicionValida = true;
                            for (int j = 0; j < solucionActual.Coords.Count; j++)
                            {
                                (int filaJ, int columnaJ) = solucionActual.Coords[j];
                                if (columnaJ == columna || Math.Abs(columnaJ - columna) == Math.Abs(filaJ - filaI))
                                {
                                    posicionValida = false;
                                    break;
                                }
                            }
                            if (posicionValida) posicionesLibres++;
                        }

                        // Sumar movimientos necesarios
                        movimientosNecesarios += (posicionesLibres > 0) ? 1 : 0; // Al menos un movimiento
                    }
                }

                // Sumar reinas faltantes
                int reinasFaltantes = reinas - solucionActual.Coords.Count;
            

                return 15*movimientosNecesarios + reinasFaltantes; // N numero de reinas tot, k = reinas colocadas // * 2 a ver klk en avara 
            } 

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
                        Solucion nuevaSolucion = new Solucion(nuevaCoords);                      
                        vecinosPosibles.Add(nuevaSolucion);
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

            //Inicio de la busqueda con A*
            //AEstrella algoritmoAEstrella = new AEstrella();
            //Solucion? solucionFinal = algoritmoAEstrella.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, CalculoCoste, out int revisados, CalculoHeuristica);

            //Inicio de busqueda Avara. Coste 0 para la búsqueda avara
            AEstrella algoritmoAvara = new AEstrella();
            Solucion? solucionFinal = algoritmoAvara.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, (solucionActual, nuevaSolucion) => 0, out int revisados, CalculoHeuristica);
            
            ///Inicio de Búsqueda en Profundidad
            //BusquedaEnProfundidad busquedaEnProfundidad = new BusquedaEnProfundidad();
            //Solucion? solucionFinal = busquedaEnProfundidad.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, CalculoCoste, out int revisados, null);


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