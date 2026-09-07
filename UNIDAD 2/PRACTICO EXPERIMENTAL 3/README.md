# Sistema de Biblioteca en C#

Aplicacion de consola desarrollada para la practica de estructuras de datos.

## Estructuras utilizadas

- `Dictionary<string, Libro>`: relaciona cada ISBN con la informacion del libro.
- `HashSet<string>`: almacena las categorias sin duplicados.
- LINQ: filtra libros por categoria y disponibilidad.
- `Stopwatch`: mide el tiempo de busqueda en el diccionario.

## Ejecutar

```powershell
dotnet run
```

El programa carga cinco libros de prueba y permite registrar, consultar, listar, prestar, devolver, eliminar y medir busquedas por ISBN.
