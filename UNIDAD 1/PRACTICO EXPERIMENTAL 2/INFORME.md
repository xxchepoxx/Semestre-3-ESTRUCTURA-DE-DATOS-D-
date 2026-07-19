# Informe de práctica experimental 2

## Asignación de 30 asientos en orden de llegada

**Asignatura:** Estructura de Datos  
**Estudiante:** [Escriba su nombre completo]  
**Docente:** [Escriba el nombre del docente]  
**Curso/paralelo:** [Escriba su curso o paralelo]  
**Fecha:** [Escriba la fecha de entrega]

---

## Introducción

Las estructuras de datos permiten organizar información para que un programa pueda almacenarla, consultarla y procesarla de acuerdo con las reglas de una situación determinada. En este caso se aborda la asignación de 30 asientos para una atracción de un parque de diversiones. El problema se relaciona con una situación cotidiana: las personas llegan, se ubican en una fila y deben ser atendidas en el mismo orden en que llegaron.

La estructura de datos seleccionada es la **cola**. Una cola sigue el principio FIFO (*First In, First Out*), o “primero en entrar, primero en salir”. En .NET, `Queue<T>` representa una colección con este comportamiento, por lo que resulta apropiada cuando se deben procesar elementos respetando su orden de llegada (Microsoft, 2025a). En el sistema, el método `Enqueue` registra a cada persona al final de la cola y `Dequeue` atiende a la persona situada al inicio.

La solución se desarrolló en C# aplicando Programación Orientada a Objetos (POO). Este paradigma organiza el programa mediante clases, objetos, propiedades y métodos que representan entidades y comportamientos del problema (Microsoft, 2025b). Además, la POO se fundamenta en principios como abstracción y encapsulación: se modelan los elementos relevantes del sistema y se controla el acceso a sus datos internos mediante métodos y propiedades (Microsoft, 2025c).

Como antecedente, los sistemas de atención por turnos —por ejemplo, filas en servicios, venta de entradas o acceso a atracciones— requieren una regla clara que evite que una persona sea atendida antes que otra que llegó primero. Por esta razón, se justifica utilizar una cola en lugar de una pila: una pila atendería al último registrado, mientras que la cola mantiene el criterio de llegada. El sistema también utiliza un arreglo de 30 posiciones para representar los asientos, porque la capacidad de la atracción es fija.

El objetivo de la práctica fue diseñar e implementar una aplicación de consola en C# que registre personas, asigne los 30 asientos disponibles según el orden de llegada y permita visualizar, consultar y reportar la información almacenada.

## Desarrollo

### Escenario de desarrollo

La actividad se desarrolló en un **entorno de escritorio**, utilizando una computadora con sistema operativo Windows, Visual Studio Code y la terminal integrada de PowerShell. La aplicación se ejecutó como un programa de consola con .NET 8. La ubicación física de trabajo corresponde a **[indique aquí: aula de computación, laboratorio o domicilio]**. No se requirieron espacios abiertos ni instalaciones externas, debido a que se trata de una simulación informática.

Las características importantes del escenario fueron las siguientes:

- Un editor de código para crear y modificar los archivos C#.
- La terminal para compilar y ejecutar el programa mediante el comando `dotnet run`.
- Un menú de consola para que el usuario ingrese los datos y seleccione cada operación.

### Análisis y diseño de la solución

Se seleccionó el caso “Asignación de 30 asientos en orden de llegada”. La solución se dividió en las siguientes clases:

| Clase | Responsabilidad principal |
|---|---|
| `Persona` | Almacena la cédula y el nombre de cada visitante. |
| `Asiento` | Representa un asiento numerado y guarda su ocupante cuando es vendido. |
| `SistemaVentaAsientos` | Contiene la cola, el arreglo de asientos y las reglas de registro, venta, búsqueda y reportes. |
| `Program` | Presenta el menú e interactúa con el usuario desde la consola. |

La relación entre los elementos del sistema es la siguiente:

```text
Persona llega → Queue<Persona> (cola de espera) → Dequeue() → Asiento disponible
```

La cola es la estructura principal. El arreglo `Asiento[30]` crea los 30 lugares disponibles, numerados del 1 al 30. Al vender, el programa busca el primer asiento no ocupado y asigna a la primera persona de la cola. De esta manera se conserva el orden de registro y se impide vender más de 30 asientos.

### Procedimiento realizado

1. Se creó un proyecto de consola C# con el archivo de configuración `P2.csproj`.
2. Se definieron las clases `Persona` y `Asiento` para representar los datos del problema.
3. Se creó la clase `SistemaVentaAsientos` y se declaró una `Queue<Persona>` llamada `colaEspera`.
4. Se inicializó un arreglo de 30 objetos `Asiento`.
5. Se programó la operación de registro. Esta valida que la cédula y el nombre no estén vacíos, evita cédulas repetidas y coloca a la persona al final de la cola.
6. Se programó la venta. Si hay una persona esperando y aún existen asientos libres, se retira el primer elemento con `Dequeue` y se le asigna el siguiente asiento disponible.
7. Se agregaron las opciones de reportería: visualizar la cola, mostrar los 30 asientos, consultar una persona por cédula y presentar un resumen de ventas.
8. Se compiló el proyecto con `dotnet build`, obteniendo **0 errores y 0 advertencias**.
9. Para ejecutarlo, se utiliza el comando `dotnet run` desde la carpeta del proyecto.

## Resultados

Se obtuvo una aplicación de consola funcional para simular la venta de asientos por orden de llegada. El menú implementado permite registrar personas, vender el siguiente asiento, ver la cola de espera, mostrar el reporte de asientos, realizar consultas por cédula y revisar un resumen general.

Como prueba de escritorio, se consideró el registro consecutivo de las personas Ana Torres, Luis Pérez y Marta Ruiz. El comportamiento esperado y comprobable al ejecutar el programa se presenta a continuación:

| Actividad | Resultado esperado |
|---|---|
| Registrar a Ana, Luis y Marta | Los tres registros quedan en la cola en ese mismo orden. |
| Vender el primer asiento | Ana Torres recibe el asiento 1 y sale de la cola. |
| Vender el segundo asiento | Luis Pérez recibe el asiento 2 y sale de la cola. |
| Consultar a Marta antes de venderle | El sistema indica que está en espera y muestra la posición 1. |
| Mostrar la cola después de dos ventas | Solo aparece Marta Ruiz. |
| Mostrar reporte de asientos | Los asientos 1 y 2 aparecen ocupados; los restantes aparecen disponibles. |
| Intentar vender cuando no hay personas | El sistema informa que no hay personas en la cola. |
| Intentar vender después de ocupar los 30 asientos | El sistema informa que los 30 asientos ya fueron vendidos. |

Los resultados demuestran que la cola satisface el requisito principal de la guía: cada persona recibe un asiento de acuerdo con su orden de llegada. La reportería permite observar la estructura sin eliminar sus elementos, y la consulta por cédula permite conocer si una persona se encuentra esperando o ya tiene un asiento asignado.

> **Nota para la entrega:** ejecute estas pruebas en su equipo y, si utiliza nombres o cédulas diferentes, actualice la tabla con los datos que haya utilizado. Puede incluir una o dos capturas de pantalla del menú, la cola y el reporte de asientos como evidencia.

## Conclusiones

1. La estructura de datos cola fue adecuada para resolver el problema porque aplica FIFO, garantizando que la primera persona registrada sea la primera en recibir un asiento.
2. La Programación Orientada a Objetos permitió separar las responsabilidades del sistema mediante las clases `Persona`, `Asiento`, `SistemaVentaAsientos` y `Program`, facilitando la comprensión y el mantenimiento del código.
3. El arreglo de 30 asientos permitió controlar la capacidad máxima de la atracción y evitar asignaciones superiores a la cantidad disponible.
4. Las funciones de reporte y consulta permiten visualizar el estado de la cola y de los asientos, por lo que se cumple el requerimiento de reportería indicado en la guía de prácticas.
5. La compilación correcta del proyecto confirma que la solución es ejecutable; las pruebas de uso permiten verificar el orden de atención y los mensajes ante situaciones sin personas en espera o sin asientos disponibles.

## Agente de IA utilizado

Se utilizó **Codex de OpenAI** como apoyo para proponer la estructura inicial del código, los comentarios y la redacción inicial del informe. El porcentaje estimado de código generado con apoyo del agente fue de **100 %**. El estudiante debe revisar, comprender y poder explicar la lógica, las clases y el funcionamiento de la cola antes de presentar la práctica.

## Bibliografía

Microsoft. (2025a). *Colecciones y estructuras de datos - .NET*. Microsoft Learn. https://learn.microsoft.com/es-es/dotnet/standard/collections/

Microsoft. (2025b). *Exploración de la programación orientada a objetos con clases y objetos*. Microsoft Learn. https://learn.microsoft.com/es-es/dotnet/csharp/fundamentals/tutorials/classes

Microsoft. (2025c). *Programación orientada a objetos (C#)*. Microsoft Learn. https://learn.microsoft.com/es-es/dotnet/csharp/fundamentals/tutorials/oop
