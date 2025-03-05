# Búsqueda Informada

## 1. Introducción

En la práctica de esta semana hemos implementado nuevo modelos de búsqueda no informada para resolver el problema de las n reinas, Búsqueda Avara, Coste Uniforme y una modificación de A*.

## 2. Implementación de A*
Para mejorar el comportamiento del algoritmo de A* hemos modificado CalculoHeuristica para que se penalicen los conflictos entre reinas.
## **Funcionamiento**

### **1. Cálculo de Conflictos**
```csharp
int conflictos = 0;
for (int i = 0; i < solucionActual.Coords.Count; i++)
{
    (int filaI, int columnaI) = solucionActual.Coords[i];
    for (int j = i + 1; j < solucionActual.Coords.Count; j++)
    {
        (int filaJ, int columnaJ) = solucionActual.Coords[j];
        if (columnaJ == columnaI || Math.Abs(columnaJ - columnaI) == Math.Abs(filaJ - filaI))
            conflictos++;
    }
}
```
Recorre todas las reinas y cuenta conflictos si están en la misma columna o diagonal.
### **2. Cálculo del Valor Heurístico
```csharp
return conflictos * 9 + (reinas - solucionActual.Coords.Count);
```
Multiplica los conflictos por 9 para penalizarlos fuertemente y suma las reinas faltantes para favorecer soluciones más completas.

### 2.3 Resultados de A*

# Tabla de Evaluación de N-Reinas

| N° de Reinas | Nodos Evaluados |
|-------------|----------------|
| 4           | 15             |
| 5           | 25             |
| 6           | 31             |
| 7           | 66             |
| 8           | 63             |
| 9           | 197            |
| 10          | 699            |
| 11          | >1500          |

## 3. Implementación de Búsqueda de Coste Uniforme
Para ejecutar la búsqueda de Coste Uniforme hemos utilizado el algoritmo A*, pero usando una heurística=0 para que el algoritmo se comporte como en Búsqueda de Coste Uniforme.

### 3.2 Resultados de Coste Uniforme

# Tabla de Evaluación de N-Reinas (Búsqueda de Costo Uniforme)

| N° de Reinas | Nodos Evaluados |
|-------------|----------------|
| 4           | 112            |
| 5           | 1373           |
| 6           | >1500          |


## 4. Implementación de Búsqueda Avara

### 4.1 Explicación del Algoritmo

Para implementar la Búsqueda Avara hemos usado la heurística de A*, pero estableciendo el coste en 0, para que se escojiese siempre la solución mas prometedora ignorando el coste.

### 4.2 Resultados de Búsqueda Avara

# Tabla de Evaluación de N-Reinas (Búsqueda Avara)

| N° de Reinas | Nodos Evaluados |
|-------------|----------------|
| 4           | 9              |
| 5           | 6              |
| 6           | 32             |
| 7           | 10             |
| 8           | 112            |
| 9           | 66             |
| 10          | 202            |
| 11          | 134            |
| 12          | 404            |
| 13          | 110            |
| 14          | 1349           |
| 15          | >1500          |


## 5. Conclusiones
1. Búsqueda Avara usa solo la heurística h(n), eligiendo el nodo que parece mejor sin considerar el costo acumulado. Es rápida y evalúa menos nodos, pero puede no encontrar la mejor solución.
2. Búsqueda de Costo Uniforme usa solo el costo acumulado g(n), asegurando la solución óptima pero explorando muchos nodos, lo que la hace más lenta.
3. A* combina ambas (f(n) = g(n) + h(n)), balanceando exploración y eficiencia.