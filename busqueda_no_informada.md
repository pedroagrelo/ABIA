# Búsqueda en Anchura y Profundidad para el Problema de las N Reinas

## Implementación de la Búsqueda en Anchura

### 🔍 Modificación del código para búsqueda en anchura (BFS)

La primera manera que tenemos de modificar el código para que se resuelva por **búsqueda en anchura (BFS)** es modificando el algoritmo `AEstrella`, el cual debe tener la siguiente forma:

#### ✅ 1. Definir la función `CalculoDePrioridad()`

- Se deben establecer:
  - **h(n) = 0** → Heurística nula.
  - **g(n) = 1** → Coste uniforme para cada movimiento.

Esto ya estaba implementado de esta forma, pero es clave para que el algoritmo se comporte como una búsqueda en anchura, ya que no se realiza ninguna estimación heurística y todos los movimientos tienen el mismo coste.

---

#### ✅ 2. Convertir la `ColaDePrioridad` en una cola FIFO

- La `ColaDePrioridad` debe comportarse como una **cola FIFO (First In, First Out)**.
- Para lograrlo:
  - Se añadió un **contador `orden`** dentro de la clase `ColaDePrioridad` para asegurar el comportamiento FIFO en caso de empate en la prioridad (`f(n)`).
  - Esto garantiza que, cuando dos nodos tengan la misma prioridad, se extraerán en el **orden en que fueron añadidos**.

##### 📌 **Modificación clave en `PriorityQueue`:**

```csharp
cp.Enqueue(entrada, prioridad * 10000 + entrada.orden);
```


La segunda manera de afrontar el problema es enfocarlo propiamente como **búsqueda en anchura**. Lo hicimos del siguiente modo:

1. Se implementó una nueva clase `ColaFIFO` que hereda de la interfaz `IListaCandidatos`, reemplazando el uso de la `PriorityQueue` por `Queue<Solucion>`.
2. Se creó la clase `BusquedaAnchura`, heredando de `AlgoritmoDeBusqueda` y forzando `CalculoDePrioridad()` a devolver siempre `0`, ya que en búsqueda en anchura todos los nodos tienen la misma prioridad.

### Otros cambios relevantes en el código

- Se modificó el método `Busqueda()` para devolver `revisados` (nodos evaluados) como un parámetro `out`, permitiendo que `Program.cs` pueda acceder al número de nodos evaluados.
- Este cambio fue necesario para implementar un bucle desde `N reinas = 4` e incrementar progresivamente hasta que los nodos evaluados (`revisados`) superaran **1500**.
- Se usó una **bandera (`continuar = false`)** para detener el bucle cuando se alcanzaba el umbral de **1500 nodos evaluados**.

---

## ¿Cuál de las dos soluciones se debería implementar?

La solución a implementar es la del **algoritmo que usa búsqueda en anchura**. A continuación explicamos por qué.

Aunque ambos algoritmos **funcionan exactamente igual y arrojan los mismos resultados**, la única diferencia relevante es **el tiempo que tardan en llegar a esos resultados**. Esta diferencia en tiempo se debe a la **latencia introducida por las operaciones que realiza cada estructura de datos utilizada** para gestionar la exploración de nodos.

### A* modificado a Búsqueda en Anchura (usando `PriorityQueue`)

- Cada inserción (`Enqueue()`) y extracción (`Dequeue()`) en la `PriorityQueue` tiene una **complejidad de O(log n)** debido a que internamente usa un **Montículo Binario (Binary Heap)** para mantener la prioridad de los nodos.
- A pesar de haber modificado `PriorityQueue` para que se comporte como una **cola FIFO**, sigue realizando **reordenamiento interno** con cada inserción.

### Búsqueda en Anchura (usando `Queue`)

- Utiliza una **cola FIFO** (`Queue<Solucion>`), donde las operaciones `Enqueue()` y `Dequeue()` son **O(1)**.
- Como **no hay reordenamiento de nodos**, los elementos se insertan y se extraen en orden de llegada **sin coste adicional**.
- **El algoritmo avanza más rápido** porque no hay que realizar ninguna comparación entre prioridades.

---

## Diferencias de complejidad

| Algoritmo | Estructura de datos usada | `Enqueue()`  | `Dequeue()`  | Complejidad total en el peor caso (O) |
|-----------|---------------------------|--------------|--------------|----------------------------------------|
| **Búsqueda en Anchura** | `Queue<Solucion>` (FIFO) | **O(1)** | **O(1)** | **O(b^d)** |
| **A* modificado (simulando Anchura)** | `PriorityQueue<Solucion, int>` | **O(log n)** | **O(log n)** | **O(b^d log(b^d))** |

 **Conclusión:**
- **Búsqueda en Anchura** es **más eficiente** porque su complejidad es menor (**O(b^d)** frente a **O(b^d log(b^d))**).
- **No realiza operaciones innecesarias**, ya que `Queue` solo inserta y extrae en O(1), mientras que `PriorityQueue` sigue ordenando los nodos.
- **La solución es inmediata**, sin retrasos causados por reordenamientos internos.

Por estas razones, **la implementación con `Queue<Solucion>` es la mejor solución.** 

---

## **Resultados obtenidos**

Dado que ambos algoritmos encuentran la misma solución, comparamos el número de nodos evaluados por cada valor de `N`:


N = 4  reinas
Nodos evaluados: 200
Coordenadas: [(0, 1), (1, 3), (2, 0), (3, 2)]

N = 5  reinas
Nodos evaluados: 1140
Coordenadas: [(0, 0), (1, 2), (2, 4), (3, 1), (4, 3)]

N = 6  reinas
La solución ya tiene más de 1500 nodos evaluados.

```
**Ambos algoritmos encuentran la misma solución, pero la búsqueda en anchura lo hace más rápido ya que es menos compleja.** 🚀
``` 

##  Implementación de la Búsqueda en Profundidad 

La primera manera que tenemos de modificar el código para que se resuelva por **búsqueda en profundidad** es modificando el algoritmo `AEstrella`, el cual debe tener la siguiente forma:

### ✅ 1. Definir la función `CalculoDePrioridad()`

- Se deben establecer:
  - **h(n) = 0** → Heurística nula.
  - **g(n) = 1** → Coste uniforme para cada movimiento.

Esto ya estaba implementado de esta forma, pero es clave para que el algoritmo se comporte como una búsqueda en profundidad, ya que no se realiza ninguna estimación heurística y todos los movimientos tienen el mismo coste.

---

### ✅ 2. Convertir la `ColaDePrioridad` en una pila LIFO

- La `ColaDePrioridad` debe comportarse como una **pila (Last In, First Out - LIFO)**.
- Para lograrlo:
  - Se añadió un **contador** dentro de la clase `ColaDePrioridad` que decrece en cada inserción, simulando el comportamiento de una pila.
  - Esto garantiza que los nodos con la misma prioridad se extraigan en el **orden inverso** al que fueron añadidos.

##### 📌 **Modificación clave en `PriorityQueue`:**
```csharp
// La prioridad disminuye con cada inserción, simulando el comportamiento de LIFO
        (Solucion solucion, string estado, int orden) entrada = (solucion, "activo", contador--);
        cp.Enqueue(entrada, entrada.orden); // Menor número = mayor prioridad (comportamiento de pila)
        buscador[key] = (solucion, "activo");
```
La segunda manera de afrontar el problema es enfocarlo propiamente como **búsqueda en profundidad** usando una pila explícita. Lo hicimos del siguiente modo:

1.Se implementó una nueva clase **`PiladeCandidatos`** que hereda de la interfaz `IListaCandidatos`, reemplazando el uso de la `PriorityQueue` por **`Stack<Solucion>`**.
2.Se creó la clase **`BusquedaEnProfundidad`**, heredando de `AlgoritmoDeBusqueda`, y forzando que el método **`CalculoDePrioridad()`** devuelva siempre **0**, ya que en búsqueda en profundidad todos los nodos tienen la misma prioridad.

---

## **¿Cuál de las dos soluciones se debería implementar?**

La solución que se debe implementar es la del **algoritmo que usa búsqueda en profundidad con una pila**. 

Igual que al implementar búsqueda en anchura, ambos algoritmos **funcionan exactamente igual y arrojan los mismos resultados**, la diferencia principal es **el tiempo que tardan en llegar a los resultados** debido  a la **latencia introducida por las operaciones que realiza cada estructura de datos utilizada** para gestionar la exploración de nodos.

---

###  **Aestrella modificado a Búsqueda en Profundidad (usando `PriorityQueue`)**

- Cada inserción (`Enqueue()`) y extracción (`Dequeue()`) en la `PriorityQueue` tiene una **complejidad de O(log n)** debido a que internamente usa un **Montículo Binario (Binary Heap)** para mantener la prioridad de los nodos.
- A pesar de haber modificado la `PriorityQueue` para que se comporte como una **pila LIFO**, sigue realizando **reordenamiento interno** con cada inserción.

---

### 🔍 **Búsqueda en Profundidad (usando `Stack`)**

- Utiliza una **pila** (`Stack<Solucion>`), donde las operaciones **`Push()`** y **`Pop()`** son **O(1)**.
- Como **no hay reordenamiento de nodos**, los elementos se insertan y se extraen en orden inverso de llegada **sin coste adicional**.
- El algoritmo avanza **más rápido** porque **no hay que realizar ninguna operación entre prioridades**.

---
