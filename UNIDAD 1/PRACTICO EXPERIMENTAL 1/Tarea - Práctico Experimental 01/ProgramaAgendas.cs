using System;
using System.Collections.Generic;
using System.Linq;

/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * APLICACIÓN: SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA
 * INSTITUCIÓN: Universidad Estatal Amazónica
 * ASIGNATURA: Estructura de Datos
 * 
 * DESCRIPCIÓN:
 * Sistema informático que permite el registro, consulta y visualización de información
 * de pacientes, médicos y sus turnos. Implementa Programación Orientada a Objetos y
 * estructuras de datos tales como vectores, matrices, registros y estructuras.
 * 
 * REQUISITOS CUMPLIDOS:
 * ✓ Identificación de clases, atributos y métodos coherentes
 * ✓ Uso de estructuras de datos (vectores, matrices, structs)
 * ✓ Funcionalidades de reportería (agregar, listar, buscar/filtrar)
 * ════════════════════════════════════════════════════════════════════════════════════
 */

// ════════════════════════════════════════════════════════════════════════════════════
// ║ ESTRUCTURA: HorarioDisponible
// ║ Propósito: Almacenar información sobre horarios y su disponibilidad
// ════════════════════════════════════════════════════════════════════════════════════
public struct HorarioDisponible
{
    public string Hora { get; set; }
    public bool Disponible { get; set; }

    /// <summary>
    /// Constructor de la estructura HorarioDisponible
    /// </summary>
    /// <param name="hora">Hora en formato HH:mm</param>
    /// <param name="disponible">Indica si el horario está disponible</param>
    public HorarioDisponible(string hora, bool disponible)
    {
        Hora = hora;
        Disponible = disponible;
    }
}

// ════════════════════════════════════════════════════════════════════════════════════
// ║ CLASE: Paciente
// ║ Propósito: Representa un paciente en el sistema de la clínica
// ║ Atributos: Datos personales e identificadores
// ════════════════════════════════════════════════════════════════════════════════════
public class Paciente
{
    // Propiedades
    public int IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }

    /// <summary>
    /// Constructor: Inicializa un nuevo paciente con sus datos personales
    /// </summary>
    public Paciente(int id, string nombre, string apellido, string cedula, string telefono, string email)
    {
        IdPaciente = id;
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Telefono = telefono;
        Email = email;
    }

    /// <summary>
    /// ToString: Retorna una representación formateada del paciente
    /// </summary>
    public override string ToString()
    {
        return $"ID: {IdPaciente} | {Nombre} {Apellido} | Cédula: {Cedula} | Tel: {Telefono}";
    }
}

// ════════════════════════════════════════════════════════════════════════════════════
// ║ CLASE: Medico
// ║ Propósito: Representa un médico que trabaja en la clínica
// ║ Atributos: Datos profesionales e identificadores
// ════════════════════════════════════════════════════════════════════════════════════
public class Medico
{
    // Propiedades
    public int IdMedico { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Especialidad { get; set; }
    public string NumeroLicencia { get; set; }

    /// <summary>
    /// Constructor: Inicializa un nuevo médico con sus datos profesionales
    /// </summary>
    public Medico(int id, string nombre, string apellido, string especialidad, string numLicencia)
    {
        IdMedico = id;
        Nombre = nombre;
        Apellido = apellido;
        Especialidad = especialidad;
        NumeroLicencia = numLicencia;
    }

    /// <summary>
    /// ToString: Retorna una representación formateada del médico
    /// </summary>
    public override string ToString()
    {
        return $"ID: {IdMedico} | Dr. {Nombre} {Apellido} | Especialidad: {Especialidad}";
    }
}

// ════════════════════════════════════════════════════════════════════════════════════
// ║ CLASE: Turno
// ║ Propósito: Representa una cita médica que relaciona paciente con médico
// ║ Atributos: Información de la cita (fecha, hora, motivo, estado)
// ════════════════════════════════════════════════════════════════════════════════════
public class Turno
{
    // Propiedades
    public int IdTurno { get; set; }
    public Paciente Paciente { get; set; }
    public Medico Medico { get; set; }
    public DateTime Fecha { get; set; }
    public string Hora { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; } // Estados: Pendiente, Confirmado, Completado, Cancelado

    /// <summary>
    /// Constructor: Inicializa un nuevo turno relacionando paciente y médico
    /// </summary>
    public Turno(int id, Paciente paciente, Medico medico, DateTime fecha, string hora, string motivo, string estado = "Pendiente")
    {
        IdTurno = id;
        Paciente = paciente;
        Medico = medico;
        Fecha = fecha;
        Hora = hora;
        Motivo = motivo;
        Estado = estado;
    }

    /// <summary>
    /// ToString: Retorna una representación formateada del turno
    /// </summary>
    public override string ToString()
    {
        return $"Turno #{IdTurno} | Paciente: {Paciente.Nombre} {Paciente.Apellido} | " +
               $"Médico: Dr. {Medico.Nombre} {Medico.Apellido} | " +
               $"Fecha: {Fecha:dd/MM/yyyy} {Hora} | Estado: {Estado}";
    }
}

// ════════════════════════════════════════════════════════════════════════════════════
// ║ CLASE: AgendaClinica
// ║ Propósito: Clase gestora principal del sistema de agenda de turnos
// ║ Responsabilidades: Gestiona pacientes, médicos, turnos y reportería
// ║ Estructuras de datos utilizadas: Listas (vectores) y matriz bidimensional
// ════════════════════════════════════════════════════════════════════════════════════
public class AgendaClinica
{
    // ─────────────────────────────────────────────────────────────────────────────
    // ATRIBUTOS (Estructuras de Datos)
    // ─────────────────────────────────────────────────────────────────────────────

    // Lista de pacientes (vector dinámico)
    private List<Paciente> pacientes;

    // Lista de médicos (vector dinámico)
    private List<Medico> medicos;

    // Lista de turnos (vector dinámico)
    private List<Turno> turnos;

    // Contadores para generar IDs automáticos
    private int proximoIdPaciente = 1;
    private int proximoIdMedico = 1;
    private int proximoIdTurno = 1;

    // Matriz bidimensional para gestionar horarios disponibles
    private HorarioDisponible[,] matrizHorarios;

    // ─────────────────────────────────────────────────────────────────────────────
    // CONSTRUCTOR
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Constructor: Inicializa todas las estructuras de datos del sistema
    /// </summary>
    public AgendaClinica()
    {
        pacientes = new List<Paciente>();
        medicos = new List<Medico>();
        turnos = new List<Turno>();
        InicializarMatrizHorarios();
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // MÉTODO PRIVADO: Inicializar Matriz de Horarios
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Inicializa la matriz de horarios disponibles
    /// Crea una matriz 8x8 con horarios de 08:00 a 17:00 disponibles
    /// </summary>
    private void InicializarMatrizHorarios()
    {
        string[] horas = { "08:00", "09:00", "10:00", "11:00", "14:00", "15:00", "16:00", "17:00" };
        matrizHorarios = new HorarioDisponible[horas.Length, horas.Length];

        for (int i = 0; i < horas.Length; i++)
        {
            for (int j = 0; j < horas.Length; j++)
            {
                matrizHorarios[i, j] = new HorarioDisponible(horas[i], true);
            }
        }
    }

    // ════════════════════════════════════════════════════════════════════════════════════
    // ║ SECCIÓN: OPERACIONES CON PACIENTES
    // ║ Funcionalidades: Agregar, listar y buscar pacientes
    // ════════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Agregar Paciente: Crea un nuevo paciente y lo almacena en la lista
    /// </summary>
    /// <param name="nombre">Nombre del paciente</param>
    /// <param name="apellido">Apellido del paciente</param>
    /// <param name="cedula">Número de cédula único</param>
    /// <param name="telefono">Teléfono de contacto</param>
    /// <param name="email">Correo electrónico</param>
    public void AgregarPaciente(string nombre, string apellido, string cedula, string telefono, string email)
    {
        Paciente paciente = new Paciente(proximoIdPaciente++, nombre, apellido, cedula, telefono, email);
        pacientes.Add(paciente);
        Console.WriteLine($"✓ Paciente '{nombre}' agregado exitosamente.");
    }

    /// <summary>
    /// Listar Todos los Pacientes: Retorna la lista completa de pacientes registrados
    /// </summary>
    /// <returns>Lista de objetos Paciente</returns>
    public List<Paciente> ListarTodosPacientes()
    {
        return pacientes;
    }

    /// <summary>
    /// Buscar Paciente por Cédula: Realiza búsqueda de un paciente específico
    /// </summary>
    /// <param name="cedula">Número de cédula a buscar</param>
    /// <returns>Objeto Paciente encontrado o null</returns>
    public Paciente BuscarPacientePorCedula(string cedula)
    {
        return pacientes.FirstOrDefault(p => p.Cedula == cedula);
    }

    /// <summary>
    /// Buscar Pacientes por Nombre: Filtra pacientes que coincidan con el nombre
    /// </summary>
    /// <param name="nombre">Nombre a buscar (búsqueda parcial)</param>
    /// <returns>Lista de pacientes que coinciden</returns>
    public List<Paciente> BuscarPacientesPorNombre(string nombre)
    {
        return pacientes.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
    }

    // ════════════════════════════════════════════════════════════════════════════════════
    // ║ SECCIÓN: OPERACIONES CON MÉDICOS
    // ║ Funcionalidades: Agregar, listar y buscar médicos
    // ════════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Agregar Médico: Crea un nuevo médico y lo almacena en la lista
    /// </summary>
    /// <param name="nombre">Nombre del médico</param>
    /// <param name="apellido">Apellido del médico</param>
    /// <param name="especialidad">Especialidad médica</param>
    /// <param name="numLicencia">Número de licencia profesional</param>
    public void AgregarMedico(string nombre, string apellido, string especialidad, string numLicencia)
    {
        Medico medico = new Medico(proximoIdMedico++, nombre, apellido, especialidad, numLicencia);
        medicos.Add(medico);
        Console.WriteLine($"✓ Médico Dr. '{nombre}' agregado exitosamente.");
    }

    /// <summary>
    /// Listar Todos los Médicos: Retorna la lista completa de médicos registrados
    /// </summary>
    /// <returns>Lista de objetos Medico</returns>
    public List<Medico> ListarTodosMedicos()
    {
        return medicos;
    }

    /// <summary>
    /// Buscar Médicos por Especialidad: Filtra médicos por su especialidad
    /// </summary>
    /// <param name="especialidad">Especialidad a buscar (búsqueda parcial)</param>
    /// <returns>Lista de médicos con esa especialidad</returns>
    public List<Medico> BuscarMedicosPorEspecialidad(string especialidad)
    {
        return medicos.Where(m => m.Especialidad.ToLower().Contains(especialidad.ToLower())).ToList();
    }

    // ════════════════════════════════════════════════════════════════════════════════════
    // ║ SECCIÓN: OPERACIONES CON TURNOS
    // ║ Funcionalidades: Agregar, listar y buscar turnos con múltiples criterios
    // ════════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Agregar Turno: Crea un nuevo turno relacionando paciente, médico y fecha
    /// Valida que tanto el paciente como el médico existan en el sistema
    /// </summary>
    /// <param name="idPaciente">ID del paciente</param>
    /// <param name="idMedico">ID del médico</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <param name="hora">Hora de la cita</param>
    /// <param name="motivo">Motivo de la consulta</param>
    public void AgregarTurno(int idPaciente, int idMedico, DateTime fecha, string hora, string motivo)
    {
        // Buscar paciente y médico en las listas
        Paciente paciente = pacientes.FirstOrDefault(p => p.IdPaciente == idPaciente);
        Medico medico = medicos.FirstOrDefault(m => m.IdMedico == idMedico);

        // Validar existencia de paciente y médico
        if (paciente == null || medico == null)
        {
            Console.WriteLine("✗ Error: Paciente o Médico no encontrado.");
            return;
        }

        // Crear el turno y agregarlo a la lista
        Turno turno = new Turno(proximoIdTurno++, paciente, medico, fecha, hora, motivo, "Confirmado");
        turnos.Add(turno);
        Console.WriteLine($"✓ Turno agregado exitosamente para {paciente.Nombre}.");
    }

    /// <summary>
    /// Listar Todos los Turnos: Retorna la lista completa de turnos registrados
    /// </summary>
    /// <returns>Lista de objetos Turno</returns>
    public List<Turno> ListarTodosTurnos()
    {
        return turnos;
    }

    /// <summary>
    /// Buscar Turnos por Paciente: Filtra todos los turnos de un paciente específico
    /// </summary>
    /// <param name="idPaciente">ID del paciente</param>
    /// <returns>Lista de turnos del paciente</returns>
    public List<Turno> BuscarTurnosPorPaciente(int idPaciente)
    {
        return turnos.Where(t => t.Paciente.IdPaciente == idPaciente).ToList();
    }

    /// <summary>
    /// Buscar Turnos por Médico: Filtra todos los turnos de un médico específico
    /// </summary>
    /// <param name="idMedico">ID del médico</param>
    /// <returns>Lista de turnos del médico</returns>
    public List<Turno> BuscarTurnosPorMedico(int idMedico)
    {
        return turnos.Where(t => t.Medico.IdMedico == idMedico).ToList();
    }

    /// <summary>
    /// Buscar Turnos por Fecha: Filtra turnos de una fecha específica
    /// </summary>
    /// <param name="fecha">Fecha a buscar</param>
    /// <returns>Lista de turnos en esa fecha</returns>
    public List<Turno> BuscarTurnosPorFecha(DateTime fecha)
    {
        return turnos.Where(t => t.Fecha.Date == fecha.Date).ToList();
    }

    /// <summary>
    /// Buscar Turnos por Estado: Filtra turnos según su estado actual
    /// Estados válidos: Pendiente, Confirmado, Completado, Cancelado
    /// </summary>
    /// <param name="estado">Estado a buscar</param>
    /// <returns>Lista de turnos con ese estado</returns>
    public List<Turno> BuscarTurnosPorEstado(string estado)
    {
        return turnos.Where(t => t.Estado.ToLower() == estado.ToLower()).ToList();
    }

    // ════════════════════════════════════════════════════════════════════════════════════
    // ║ SECCIÓN: REPORTERÍA Y VISUALIZACIÓN
    // ║ Funcionalidades: Generar reportes formateados del sistema
    // ════════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generar Reporte de Turnos: Muestra todos los turnos ordenados por fecha y hora
    /// Utiliza formato de tabla para mejor visualización
    /// </summary>
    public void GenerarReporteTurnos()
    {
        Console.WriteLine("\n" + new string('=', 100));
        Console.WriteLine("REPORTE GENERAL DE TURNOS");
        Console.WriteLine(new string('=', 100));

        if (turnos.Count == 0)
        {
            Console.WriteLine("No hay turnos registrados.");
            return;
        }

        // Ordenar turnos por fecha y hora
        foreach (var turno in turnos.OrderBy(t => t.Fecha).ThenBy(t => t.Hora))
        {
            Console.WriteLine(turno);
        }

        Console.WriteLine(new string('=', 100));
        Console.WriteLine($"Total de turnos: {turnos.Count}\n");
    }

    /// <summary>
    /// Generar Reporte de Ocupación por Médico: Muestra carga de trabajo de cada médico
    /// Permite identificar médicos con mayor carga de turnos
    /// </summary>
    public void GenerarReporteOcupacionPorMedico()
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("REPORTE: OCUPACIÓN POR MÉDICO");
        Console.WriteLine(new string('=', 80));

        // Iterar sobre todos los médicos registrados
        foreach (var medico in medicos)
        {
            var turnosMedico = BuscarTurnosPorMedico(medico.IdMedico);
            Console.WriteLine($"\nDr. {medico.Nombre} {medico.Apellido} ({medico.Especialidad})");
            Console.WriteLine($"  Turnos asignados: {turnosMedico.Count}");

            if (turnosMedico.Count > 0)
            {
                foreach (var turno in turnosMedico)
                {
                    Console.WriteLine($"    - {turno.Fecha:dd/MM/yyyy} {turno.Hora} | {turno.Paciente.Nombre} ({turno.Estado})");
                }
            }
        }
        Console.WriteLine(new string('=', 80) + "\n");
    }
}

// ════════════════════════════════════════════════════════════════════════════════════
// ║ CLASE: Program
// ║ Propósito: Contiene el punto de entrada del programa y métodos de interfaz
// ║ Responsabilidades: Menú principal e interacción con el usuario
// ════════════════════════════════════════════════════════════════════════════════════
class Program
{
    /// <summary>
    /// Main: Punto de entrada del programa
    /// Inicializa el sistema con datos de prueba y muestra el menú interactivo
    /// </summary>
    static void Main()
    {
        // Banner de bienvenida
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA             ║");
        Console.WriteLine("║              Universidad Estatal Amazónica                    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA             ║");
        Console.WriteLine("║              Universidad Estatal Amazónica                    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

        AgendaClinica agenda = new AgendaClinica();
        Console.WriteLine("► Cargando datos iniciales...\n");

        agenda.AgregarPaciente("Juan", "García", "1234567890", "0987654321", "juan@email.com");
        agenda.AgregarPaciente("María", "López", "0987654321", "0912345678", "maria@email.com");
        agenda.AgregarPaciente("Carlos", "Martínez", "1122334455", "0923456789", "carlos@email.com");
        agenda.AgregarPaciente("Ana", "Rodríguez", "5544332211", "0934567890", "ana@email.com");

        agenda.AgregarMedico("Pedro", "Sánchez", "Cardiología", "LIC-001");
        agenda.AgregarMedico("Laura", "Fernández", "Dermatología", "LIC-002");
        agenda.AgregarMedico("Roberto", "González", "Cardiología", "LIC-003");
        agenda.AgregarMedico("Isabel", "Vega", "Pediatría", "LIC-004");

        Console.WriteLine("\n► Creando turnos...\n");
        agenda.AgregarTurno(1, 1, new DateTime(2024, 06, 15), "09:00", "Revisión cardíaca");
        agenda.AgregarTurno(2, 2, new DateTime(2024, 06, 15), "10:00", "Consulta dermatológica");
        agenda.AgregarTurno(3, 1, new DateTime(2024, 06, 16), "14:00", "Electrocardiograma");
        agenda.AgregarTurno(1, 3, new DateTime(2024, 06, 16), "15:00", "Segunda opinión cardíaca");
        agenda.AgregarTurno(4, 4, new DateTime(2024, 06, 17), "08:00", "Revisión pediátrica");

        bool ejecutar = true;
        while (ejecutar)
        {
            MostrarMenu();
            Console.Write("\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarPacientes(agenda);
                    break;
                case "2":
                    MostrarMedicos(agenda);
                    break;
                case "3":
                    agenda.GenerarReporteTurnos();
                    break;
                case "4":
                    BuscarTurnosPorPaciente(agenda);
                    break;
                case "5":
                    BuscarTurnosPorMedico(agenda);
                    break;
                case "6":
                    agenda.GenerarReporteOcupacionPorMedico();
                    break;
                case "7":
                    CrearNuevoTurno(agenda);
                    break;
                case "8":
                    Console.WriteLine("\n¡Hasta luego!");
                    ejecutar = false;
                    break;
                default:
                    Console.WriteLine("✗ Opción no válida.");
                    break;
            }
        }
        AgendaClinica agenda = new AgendaClinica();

    static void MostrarMenu()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│           MENÚ PRINCIPAL                │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ 1. Listar pacientes                     │");
        Console.WriteLine("│ 2. Listar médicos                       │");
        Console.WriteLine("│ 3. Listar todos los turnos              │");
        Console.WriteLine("│ 4. Buscar turnos por paciente           │");
        Console.WriteLine("│ 5. Buscar turnos por médico             │");
        Console.WriteLine("│ 6. Reporte de ocupación por médico      │");
        Console.WriteLine("│ 7. Crear nuevo turno                    │");
        Console.WriteLine("│ 8. Salir                                │");
        Console.WriteLine("└─────────────────────────────────────────┘");
    }

    static void MostrarPacientes(AgendaClinica agenda)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("LISTADO DE PACIENTES");
        Console.WriteLine(new string('=', 80));
        var pacientes = agenda.ListarTodosPacientes();
        if (pacientes.Count == 0)
            Console.WriteLine("No hay pacientes registrados.");
        else
            foreach (var paciente in pacientes)
                Console.WriteLine(paciente);
        Console.WriteLine(new string('=', 80));
    }

    static void MostrarMedicos(AgendaClinica agenda)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("LISTADO DE MÉDICOS");
        Console.WriteLine(new string('=', 80));
        var medicos = agenda.ListarTodosMedicos();
        if (medicos.Count == 0)
            Console.WriteLine("No hay médicos registrados.");
        else
            foreach (var medico in medicos)
                Console.WriteLine(medico);
        Console.WriteLine(new string('=', 80));
    }

    static void BuscarTurnosPorPaciente(AgendaClinica agenda)
    {
        Console.Write("\nIngrese el ID del paciente: ");
        if (int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            var turnos = agenda.BuscarTurnosPorPaciente(idPaciente);
            MostrarResultadosBusqueda(turnos, $"Turnos del paciente (ID: {idPaciente})");
        }
        else
            Console.WriteLine("✗ ID inválido.");
    }

    static void BuscarTurnosPorMedico(AgendaClinica agenda)
    {
        Console.Write("\nIngrese el ID del médico: ");
        if (int.TryParse(Console.ReadLine(), out int idMedico))
        {
            var turnos = agenda.BuscarTurnosPorMedico(idMedico);
            MostrarResultadosBusqueda(turnos, $"Turnos del médico (ID: {idMedico})");
        }
        else
            Console.WriteLine("✗ ID inválido.");
    }

    static void MostrarResultadosBusqueda(List<Turno> turnos, string titulo)
    {
        Console.WriteLine("\n" + new string('=', 100));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', 100));
        if (turnos.Count == 0)
            Console.WriteLine("No se encontraron resultados.");
        else
            foreach (var turno in turnos.OrderBy(t => t.Fecha).ThenBy(t => t.Hora))
                Console.WriteLine(turno);
        Console.WriteLine(new string('=', 100));
    }

    static void CrearNuevoTurno(AgendaClinica agenda)
    {
        Console.WriteLine("\n► CREAR NUEVO TURNO");
        Console.Write("ID del paciente: ");
        if (!int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            Console.WriteLine("✗ ID de paciente inválido.");
            return;
        }
        Console.Write("ID del médico: ");
        if (!int.TryParse(Console.ReadLine(), out int idMedico))
        {
            Console.WriteLine("✗ ID de médico inválido.");
            return;
        }
        Console.Write("Fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
        {
            Console.WriteLine("✗ Fecha inválida.");
            return;
        }
        Console.Write("Hora (HH:mm): ");
        string hora = Console.ReadLine();
        Console.Write("Motivo de consulta: ");
        string motivo = Console.ReadLine();
        agenda.AgregarTurno(idPaciente, idMedico, fecha, hora, motivo);
    }

        // ─────────────────────────────────────────────────────────────────────────────
        // CARGA DE DATOS INICIALES
        // ─────────────────────────────────────────────────────────────────────────────
        
        Console.WriteLine("► Cargando datos iniciales...\n");

        // Cargar pacientes de prueba
        agenda.AgregarPaciente("Juan", "García", "1234567890", "0987654321", "juan@email.com");
        agenda.AgregarPaciente("María", "López", "0987654321", "0912345678", "maria@email.com");
        agenda.AgregarPaciente("Carlos", "Martínez", "1122334455", "0923456789", "carlos@email.com");
        agenda.AgregarPaciente("Ana", "Rodríguez", "5544332211", "0934567890", "ana@email.com");

        // Cargar médicos de prueba
        agenda.AgregarMedico("Pedro", "Sánchez", "Cardiología", "LIC-001");
        agenda.AgregarMedico("Laura", "Fernández", "Dermatología", "LIC-002");
        agenda.AgregarMedico("Roberto", "González", "Cardiología", "LIC-003");
        agenda.AgregarMedico("Isabel", "Vega", "Pediatría", "LIC-004");

        // Cargar turnos de prueba
        Console.WriteLine("\n► Creando turnos...\n");
        agenda.AgregarTurno(1, 1, new DateTime(2024, 06, 15), "09:00", "Revisión cardíaca");
        agenda.AgregarTurno(2, 2, new DateTime(2024, 06, 15), "10:00", "Consulta dermatológica");
        agenda.AgregarTurno(3, 1, new DateTime(2024, 06, 16), "14:00", "Electrocardiograma");
        agenda.AgregarTurno(1, 3, new DateTime(2024, 06, 16), "15:00", "Segunda opinión cardíaca");
        agenda.AgregarTurno(4, 4, new DateTime(2024, 06, 17), "08:00", "Revisión pediátrica");

        // ─────────────────────────────────────────────────────────────────────────────
        // MENÚ INTERACTIVO PRINCIPAL
        // ─────────────────────────────────────────────────────────────────────────────
        
        bool ejecutar = true;
        while (ejecutar)
        {
            MostrarMenu();
            Console.Write("\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarPacientes(agenda);
                    break;
                case "2":
                    MostrarMedicos(agenda);
                    break;
                case "3":
                    agenda.GenerarReporteTurnos();
                    break;
                case "4":
                    BuscarTurnosPorPaciente(agenda);
                    break;
                case "5":
                    BuscarTurnosPorMedico(agenda);
                    break;
                case "6":
                    agenda.GenerarReporteOcupacionPorMedico();
                    break;
                case "7":
                    CrearNuevoTurno(agenda);
                    break;
                case "8":
                    Console.WriteLine("\n¡Hasta luego!");
                    ejecutar = false;
                    break;
                default:
                    Console.WriteLine("✗ Opción no válida.");
                    break;
            }
        }
    }

    /// <summary>
    /// Mostrar Menu: Presenta el menú de opciones disponibles al usuario
    /// </summary>
    static void MostrarMenu()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│           MENÚ PRINCIPAL                │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ 1. Listar pacientes                     │");
        Console.WriteLine("│ 2. Listar médicos                       │");
        Console.WriteLine("│ 3. Listar todos los turnos              │");
        Console.WriteLine("│ 4. Buscar turnos por paciente           │");
        Console.WriteLine("│ 5. Buscar turnos por médico             │");
        Console.WriteLine("│ 6. Reporte de ocupación por médico      │");
        Console.WriteLine("│ 7. Crear nuevo turno                    │");
        Console.WriteLine("│ 8. Salir                                │");
        Console.WriteLine("└─────────────────────────────────────────┘");
    }

    /// <summary>
    /// Mostrar Pacientes: Muestra el listado completo de todos los pacientes registrados
    /// </summary>
    static void MostrarPacientes(AgendaClinica agenda)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("LISTADO DE PACIENTES");
        Console.WriteLine(new string('=', 80));

        var pacientes = agenda.ListarTodosPacientes();
        if (pacientes.Count == 0)
        {
            Console.WriteLine("No hay pacientes registrados.");
        }
        else
        {
            // Mostrar cada paciente con sus datos
            foreach (var paciente in pacientes)
            {
                Console.WriteLine(paciente);
            }
        }
        Console.WriteLine(new string('=', 80));
    }

    /// <summary>
    /// Mostrar Medicos: Muestra el listado completo de todos los médicos registrados
    /// </summary>
    static void MostrarMedicos(AgendaClinica agenda)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("LISTADO DE MÉDICOS");
        Console.WriteLine(new string('=', 80));

        var medicos = agenda.ListarTodosMedicos();
        if (medicos.Count == 0)
        {
            Console.WriteLine("No hay médicos registrados.");
        }
        else
        {
            // Mostrar cada médico con sus datos
            foreach (var medico in medicos)
            {
                Console.WriteLine(medico);
            }
        }
        Console.WriteLine(new string('=', 80));
    }

    /// <summary>
    /// Buscar Turnos Por Paciente: Busca todos los turnos de un paciente específico por ID
    /// </summary>
    static void BuscarTurnosPorPaciente(AgendaClinica agenda)
    {
        Console.Write("\nIngrese el ID del paciente: ");
        if (int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            // Buscar turnos del paciente
            var turnos = agenda.BuscarTurnosPorPaciente(idPaciente);
            MostrarResultadosBusqueda(turnos, $"Turnos del paciente (ID: {idPaciente})");
        }
        else
        {
            Console.WriteLine("✗ ID inválido.");
        }
    }

    /// <summary>
    /// Buscar Turnos Por Medico: Busca todos los turnos de un médico específico por ID
    /// </summary>
    static void BuscarTurnosPorMedico(AgendaClinica agenda)
    {
        Console.Write("\nIngrese el ID del médico: ");
        if (int.TryParse(Console.ReadLine(), out int idMedico))
        {
            // Buscar turnos del médico
            var turnos = agenda.BuscarTurnosPorMedico(idMedico);
            MostrarResultadosBusqueda(turnos, $"Turnos del médico (ID: {idMedico})");
        }
        else
        {
            Console.WriteLine("✗ ID inválido.");
        }
    }

    /// <summary>
    /// Mostrar Resultados Busqueda: Muestra los resultados de una búsqueda de turnos
    /// </summary>
    /// <param name="turnos">Lista de turnos a mostrar</param>
    /// <param name="titulo">Título del reporte de búsqueda</param>
    static void MostrarResultadosBusqueda(List<Turno> turnos, string titulo)
    {
        Console.WriteLine("\n" + new string('=', 100));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', 100));

        if (turnos.Count == 0)
        {
            Console.WriteLine("No se encontraron resultados.");
        }
        else
        {
            // Mostrar turnos ordenados por fecha y hora
            foreach (var turno in turnos.OrderBy(t => t.Fecha).ThenBy(t => t.Hora))
            {
                Console.WriteLine(turno);
            }
        }
        Console.WriteLine(new string('=', 100));
    }

    /// <summary>
    /// Crear Nuevo Turno: Permite al usuario ingresar datos para crear un turno nuevo
    /// Solicita ID paciente, ID médico, fecha, hora y motivo
    /// </summary>
    static void CrearNuevoTurno(AgendaClinica agenda)
    {
        Console.WriteLine("\n► CREAR NUEVO TURNO");
        
        // Solicitar ID del paciente
        Console.Write("ID del paciente: ");
        if (!int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            Console.WriteLine("✗ ID de paciente inválido.");
            return;
        }

        // Solicitar ID del médico
        Console.Write("ID del médico: ");
        if (!int.TryParse(Console.ReadLine(), out int idMedico))
        {
            Console.WriteLine("✗ ID de médico inválido.");
            return;
        }

        // Solicitar fecha
        Console.Write("Fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
        {
            Console.WriteLine("✗ Fecha inválida.");
            return;
        }

        // Solicitar hora
        Console.Write("Hora (HH:mm): ");
        string hora = Console.ReadLine();

        // Solicitar motivo
        Console.Write("Motivo de consulta: ");
        string motivo = Console.ReadLine();

        // Agregar turno al sistema
        agenda.AgregarTurno(idPaciente, idMedico, fecha, hora, motivo);
    }
}