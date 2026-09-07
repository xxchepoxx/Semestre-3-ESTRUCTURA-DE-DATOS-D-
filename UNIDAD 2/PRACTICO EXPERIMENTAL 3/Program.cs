using System.Diagnostics;

namespace BibliotecaApp;

public sealed class Libro
{
    public required string Isbn { get; init; }
    public required string Titulo { get; init; }
    public required string Autor { get; init; }
    public required string Categoria { get; init; }
    public bool Disponible { get; set; } = true;
}

public sealed class Biblioteca
{
    private readonly Dictionary<string, Libro> libros = new();
    private readonly HashSet<string> categorias = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, Libro> Libros => libros;
    public IReadOnlySet<string> Categorias => categorias;

    public (bool Exito, string Mensaje) RegistrarLibro(
        string isbn,
        string titulo,
        string autor,
        string categoria)
    {
        if (libros.ContainsKey(isbn))
        {
            return (false, "El ISBN ya esta registrado.");
        }

        var libro = new Libro
        {
            Isbn = isbn,
            Titulo = titulo,
            Autor = autor,
            Categoria = categoria
        };

        libros.Add(isbn, libro);
        categorias.Add(categoria);
        return (true, "Libro registrado correctamente.");
    }

    public Libro? ConsultarLibro(string isbn)
    {
        return libros.GetValueOrDefault(isbn);
    }

    public (bool Exito, string Mensaje) EliminarLibro(string isbn)
    {
        if (!libros.Remove(isbn, out var libro))
        {
            return (false, "El libro no existe.");
        }

        if (!libros.Values.Any(item =>
                item.Categoria.Equals(libro.Categoria, StringComparison.OrdinalIgnoreCase)))
        {
            categorias.Remove(libro.Categoria);
        }

        return (true, "Libro eliminado correctamente.");
    }

    public (bool Exito, string Mensaje) PrestarLibro(string isbn)
    {
        var libro = ConsultarLibro(isbn);

        if (libro is null)
        {
            return (false, "El libro no existe.");
        }

        if (!libro.Disponible)
        {
            return (false, "El libro ya esta prestado.");
        }

        libro.Disponible = false;
        return (true, "Prestamo registrado correctamente.");
    }

    public (bool Exito, string Mensaje) DevolverLibro(string isbn)
    {
        var libro = ConsultarLibro(isbn);

        if (libro is null)
        {
            return (false, "El libro no existe.");
        }

        if (libro.Disponible)
        {
            return (false, "El libro ya estaba disponible.");
        }

        libro.Disponible = true;
        return (true, "Devolucion registrada correctamente.");
    }

    public IEnumerable<KeyValuePair<string, Libro>> LibrosPorCategoria(string categoria)
    {
        return libros.Where(par =>
            par.Value.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<KeyValuePair<string, Libro>> LibrosDisponibles()
    {
        return libros.Where(par => par.Value.Disponible);
    }

    public (int Total, int Disponibles, int Prestados) ObtenerResumen()
    {
        var total = libros.Count;
        var disponibles = libros.Values.Count(libro => libro.Disponible);
        return (total, disponibles, total - disponibles);
    }

    public (TimeSpan Total, TimeSpan Promedio) MedirBusqueda(string isbn, int repeticiones = 10_000)
    {
        var cronometro = Stopwatch.StartNew();

        for (var i = 0; i < repeticiones; i++)
        {
            _ = ConsultarLibro(isbn);
        }

        cronometro.Stop();
        var tiempoPromedio = TimeSpan.FromTicks(cronometro.Elapsed.Ticks / repeticiones);
        return (cronometro.Elapsed, tiempoPromedio);
    }
}

public static class Program
{
    private static readonly Biblioteca biblioteca = new();

    public static void Main()
    {
        CargarDatosDemo();

        while (true)
        {
            MostrarMenu();
            var opcion = LeerTexto("Seleccione una opcion: ");
            Console.WriteLine();

            switch (opcion)
            {
                case "1": RegistrarLibro(); break;
                case "2": ConsultarLibro(); break;
                case "3": MostrarLibros(); break;
                case "4": BuscarPorCategoria(); break;
                case "5": PrestarLibro(); break;
                case "6": DevolverLibro(); break;
                case "7": MostrarDisponibles(); break;
                case "8": MostrarCategorias(); break;
                case "9": MostrarReporte(); break;
                case "10": MedirBusqueda(); break;
                case "11": EliminarLibro(); break;
                case "0": Console.WriteLine("Programa finalizado."); return;
                default: Console.WriteLine("Opcion invalida."); break;
            }

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("======================================");
        Console.WriteLine("       SISTEMA DE BIBLIOTECA");
        Console.WriteLine("======================================");
        Console.WriteLine("1. Registrar libro");
        Console.WriteLine("2. Consultar libro por ISBN");
        Console.WriteLine("3. Mostrar todos los libros");
        Console.WriteLine("4. Buscar por categoria");
        Console.WriteLine("5. Prestar libro");
        Console.WriteLine("6. Devolver libro");
        Console.WriteLine("7. Mostrar libros disponibles");
        Console.WriteLine("8. Mostrar categorias");
        Console.WriteLine("9. Mostrar reporte");
        Console.WriteLine("10. Medir tiempo de busqueda");
        Console.WriteLine("11. Eliminar libro");
        Console.WriteLine("0. Salir");
        Console.WriteLine();
    }

    private static void RegistrarLibro()
    {
        var resultado = biblioteca.RegistrarLibro(
            LeerTexto("ISBN: "),
            LeerTexto("Titulo: "),
            LeerTexto("Autor: "),
            LeerTexto("Categoria: "));

        Console.WriteLine(resultado.Mensaje);
    }

    private static void ConsultarLibro()
    {
        var isbn = LeerTexto("Ingrese el ISBN: ");
        var libro = biblioteca.ConsultarLibro(isbn);

        if (libro is null)
        {
            Console.WriteLine("No se encontro el libro.");
            return;
        }

        MostrarDetalle(libro);
    }

    private static void MostrarLibros()
    {
        if (biblioteca.Libros.Count == 0)
        {
            Console.WriteLine("No existen libros registrados.");
            return;
        }

        Console.WriteLine("========== LIBROS REGISTRADOS ==========");
        foreach (var par in biblioteca.Libros)
        {
            MostrarDetalle(par.Value);
            Console.WriteLine(new string('-', 40));
        }
    }

    private static void BuscarPorCategoria()
    {
        var categoria = LeerTexto("Ingrese la categoria: ");
        var resultados = biblioteca.LibrosPorCategoria(categoria).ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("No existen libros en esa categoria.");
            return;
        }

        Console.WriteLine($"Libros de la categoria '{categoria}':");
        foreach (var par in resultados)
        {
            Console.WriteLine($"{par.Key} - {par.Value.Titulo} - {par.Value.Autor}");
        }
    }

    private static void PrestarLibro()
    {
        var resultado = biblioteca.PrestarLibro(LeerTexto("ISBN del libro a prestar: "));
        Console.WriteLine(resultado.Mensaje);
    }

    private static void DevolverLibro()
    {
        var resultado = biblioteca.DevolverLibro(LeerTexto("ISBN del libro a devolver: "));
        Console.WriteLine(resultado.Mensaje);
    }

    private static void MostrarDisponibles()
    {
        Console.WriteLine("========== LIBROS DISPONIBLES ==========");
        var disponibles = biblioteca.LibrosDisponibles().ToList();

        if (disponibles.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        foreach (var par in disponibles)
        {
            Console.WriteLine($"{par.Key} - {par.Value.Titulo} - {par.Value.Autor}");
        }
    }

    private static void MostrarCategorias()
    {
        Console.WriteLine("========== CATEGORIAS ==========");
        foreach (var categoria in biblioteca.Categorias.Order(StringComparer.OrdinalIgnoreCase))
        {
            Console.WriteLine($"- {categoria}");
        }
    }

    private static void MostrarReporte()
    {
        var resumen = biblioteca.ObtenerResumen();
        Console.WriteLine("========== REPORTE DE BIBLIOTECA ==========");
        Console.WriteLine($"Total de libros: {resumen.Total}");
        Console.WriteLine($"Libros disponibles: {resumen.Disponibles}");
        Console.WriteLine($"Libros prestados: {resumen.Prestados}");
        Console.WriteLine($"Cantidad de categorias: {biblioteca.Categorias.Count}");
        MostrarCategorias();
    }

    private static void MedirBusqueda()
    {
        var isbn = LeerTexto("ISBN a consultar: ");

        if (!biblioteca.Libros.ContainsKey(isbn))
        {
            Console.WriteLine("El ISBN no existe.");
            return;
        }

        const int repeticiones = 10_000;
        var medicion = biblioteca.MedirBusqueda(isbn, repeticiones);
        Console.WriteLine("========== ANALISIS DE TIEMPO ==========");
        Console.WriteLine($"Repeticiones: {repeticiones}");
        Console.WriteLine($"Tiempo total: {medicion.Total.TotalSeconds:F8} segundos");
        Console.WriteLine($"Tiempo promedio: {medicion.Promedio.TotalSeconds:F10} segundos");
    }

    private static void EliminarLibro()
    {
        var resultado = biblioteca.EliminarLibro(LeerTexto("ISBN del libro a eliminar: "));
        Console.WriteLine(resultado.Mensaje);
    }

    private static void MostrarDetalle(Libro libro)
    {
        Console.WriteLine($"ISBN: {libro.Isbn}");
        Console.WriteLine($"Titulo: {libro.Titulo}");
        Console.WriteLine($"Autor: {libro.Autor}");
        Console.WriteLine($"Categoria: {libro.Categoria}");
        Console.WriteLine($"Estado: {(libro.Disponible ? "Disponible" : "Prestado")}");
    }

    private static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            var valor = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            Console.WriteLine("El valor no puede estar vacio.");
        }
    }

    private static void CargarDatosDemo()
    {
        var datos = new[]
        {
            ("9780135957059", "Python Programming", "Autor Uno", "Programacion"),
            ("9781492052203", "Fluent Python", "Autor Dos", "Programacion"),
            ("9780134494166", "Java Programming", "Autor Tres", "Programacion"),
            ("9780262046305", "Inteligencia Artificial", "Autor Cuatro", "Inteligencia Artificial"),
            ("9780132350884", "Clean Code", "Robert Martin", "Ingenieria de Software")
        };

        foreach (var dato in datos)
        {
            biblioteca.RegistrarLibro(dato.Item1, dato.Item2, dato.Item3, dato.Item4);
        }
    }
}
