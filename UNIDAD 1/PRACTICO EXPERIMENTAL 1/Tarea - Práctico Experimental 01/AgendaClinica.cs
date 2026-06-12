using System;
using System.Collections.Generic;
using System.Linq;

/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * CLASE: AgendaClinica
 * Propósito: Clase gestora principal del sistema de agenda de turnos
 * Responsabilidades: Gestiona pacientes, médicos, turnos y reportería
 * Estructuras de datos utilizadas: Listas (vectores) y matriz bidimensional
 * ════════════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Clase principal que gestiona todo el sistema de agenda de la clínica
/// Organiza y controla pacientes, médicos, turnos y genera reportes
/// </summary>
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
