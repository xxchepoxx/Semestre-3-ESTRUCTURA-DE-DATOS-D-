/*
 * Práctico Experimental #1
 * Nombre: Marcelo Chacón
 * Fecha: 11/06/2026
 * Curso: Estructura de Datos (D)
 * Carrera: TICS "Tecnologías en la información"
 */

using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        try
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();
        }
        catch (System.IO.IOException)
        {
            // No hay consola válida (ej.: depuración sin terminal). Ignorar.
        }

        var agenda = new AgendaClinica();
        InicializarEjemplo(agenda);
        EjecutarMenu(agenda);
    }

    static void InicializarEjemplo(AgendaClinica agenda)
    {
        agenda.AgregarPaciente("Juan", "García", "1234567890", "0987654321", "juan@email.com");
        agenda.AgregarPaciente("María", "López", "0987654321", "0912345678", "maria@email.com");
        agenda.AgregarPaciente("Carlos", "Martínez", "1122334455", "0923456789", "carlos@email.com");

        agenda.AgregarMedico("Pedro", "Sánchez", "Cardiología", "LIC-001");
        agenda.AgregarMedico("Laura", "Fernández", "Dermatología", "LIC-002");
        agenda.AgregarMedico("Isabel", "Vega", "Pediatría", "LIC-003");

        agenda.AgregarTurno(1, 1, DateTime.Today.AddDays(1), "09:00", "Consulta inicial");
        agenda.AgregarTurno(2, 2, DateTime.Today.AddDays(2), "10:00", "Consulta dermatológica");
        agenda.AgregarTurno(1, 3, DateTime.Today.AddDays(3), "14:00", "Control pediátrico para familiar");
    }

    static void EjecutarMenu(AgendaClinica agenda)
    {
        while (true)
        {
            Console.Clear();
            MostrarEncabezado();
            MostrarMenu();
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine() ?? string.Empty;

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
                    BuscarTurnosPorFecha(agenda);
                    break;
                case "7":
                    BuscarPacientePorCedula(agenda);
                    break;
                case "8":
                    agenda.MostrarMatrizHorarios();
                    break;
                case "9":
                    CrearNuevoTurno(agenda);
                    break;
                case "10":
                    CrearNuevoPaciente(agenda);
                    break;
                case "11":
                    CrearNuevoMedico(agenda);
                    break;
                case "12":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void MostrarEncabezado()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA              ║");
        Console.WriteLine("║              Universidad Estatal Amazónica                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");
    }

    static void MostrarMenu()
    {
        Console.WriteLine("1. Listar pacientes");
        Console.WriteLine("2. Listar médicos");
        Console.WriteLine("3. Listar todos los turnos");
        Console.WriteLine("4. Buscar turnos por paciente");
        Console.WriteLine("5. Buscar turnos por médico");
        Console.WriteLine("6. Buscar turnos por fecha");
        Console.WriteLine("7. Buscar paciente por cédula y ver sus turnos");
        Console.WriteLine("8. Mostrar horarios disponibles");
        Console.WriteLine("9. Crear nuevo turno");
        Console.WriteLine("10. Crear nuevo paciente");
        Console.WriteLine("11. Crear nuevo médico");
        Console.WriteLine("12. Salir\n");
    }

    static void MostrarPacientes(AgendaClinica agenda)
    {
        Console.WriteLine("\nLISTADO DE PACIENTES");
        var pacientes = agenda.ListarTodosPacientes();
        if (pacientes.Count == 0)
        {
            Console.WriteLine("No hay pacientes registrados.");
            return;
        }

        foreach (var paciente in pacientes)
        {
            Console.WriteLine(paciente);
        }
    }

    static void MostrarMedicos(AgendaClinica agenda)
    {
        Console.WriteLine("\nLISTADO DE MÉDICOS");
        var medicos = agenda.ListarTodosMedicos();
        if (medicos.Count == 0)
        {
            Console.WriteLine("No hay médicos registrados.");
            return;
        }

        foreach (var medico in medicos)
        {
            Console.WriteLine(medico);
        }
    }

    static void BuscarTurnosPorPaciente(AgendaClinica agenda)
    {
        Console.Write("Ingrese el ID del paciente: ");
        if (!int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var turnos = agenda.BuscarTurnosPorPaciente(idPaciente);
        MostrarResultadosTurnos(turnos, $"Turnos del paciente (ID: {idPaciente})");
    }

    static void BuscarTurnosPorMedico(AgendaClinica agenda)
    {
        Console.Write("Ingrese el ID del médico: ");
        if (!int.TryParse(Console.ReadLine(), out int idMedico))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var turnos = agenda.BuscarTurnosPorMedico(idMedico);
        MostrarResultadosTurnos(turnos, $"Turnos del médico (ID: {idMedico})");
    }

    static void BuscarTurnosPorFecha(AgendaClinica agenda)
    {
        Console.Write("Ingrese la fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime fecha))
        {
            Console.WriteLine("Fecha inválida.");
            return;
        }

        var turnos = agenda.BuscarTurnosPorFecha(fecha);
        MostrarResultadosTurnos(turnos, $"Turnos del día {fecha:dd/MM/yyyy}");
    }

    static void BuscarPacientePorCedula(AgendaClinica agenda)
    {
        Console.Write("Ingrese la cédula del paciente: ");
        string cedula = Console.ReadLine() ?? string.Empty;

        var paciente = agenda.BuscarPacientePorCedula(cedula);
        if (paciente == null)
        {
            Console.WriteLine("No se encontró un paciente con esa cédula.");
            return;
        }

        Console.WriteLine("\nPaciente encontrado:");
        Console.WriteLine(paciente);

        var turnos = agenda.BuscarTurnosPorPaciente(paciente.IdPaciente);
        MostrarResultadosTurnos(turnos, $"Turnos relacionados con {paciente.Nombre} {paciente.Apellido}");
    }

    static void MostrarResultadosTurnos(List<Turno> turnos, string titulo)
    {
        Console.WriteLine($"\n{titulo}");
        Console.WriteLine(new string('=', 100));

        if (turnos.Count == 0)
        {
            Console.WriteLine("No se encontraron turnos.");
            return;
        }

        foreach (var turno in turnos)
        {
            Console.WriteLine(turno);
        }
    }

    static void CrearNuevoTurno(AgendaClinica agenda)
    {
        Console.Write("ID del paciente: ");
        if (!int.TryParse(Console.ReadLine(), out int idPaciente))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Console.Write("ID del médico: ");
        if (!int.TryParse(Console.ReadLine(), out int idMedico))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Console.Write("Fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime fecha))
        {
            Console.WriteLine("Fecha inválida.");
            return;
        }

        Console.Write("Hora (HH:mm): ");
        string hora = Console.ReadLine() ?? string.Empty;

        Console.Write("Motivo: ");
        string motivo = Console.ReadLine() ?? string.Empty;

        agenda.AgregarTurno(idPaciente, idMedico, fecha, hora, motivo);
    }

    static void CrearNuevoPaciente(AgendaClinica agenda)
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? string.Empty;
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine() ?? string.Empty;
        Console.Write("Cédula: ");
        string cedula = Console.ReadLine() ?? string.Empty;
        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine() ?? string.Empty;
        Console.Write("Email: ");
        string email = Console.ReadLine() ?? string.Empty;

        agenda.AgregarPaciente(nombre, apellido, cedula, telefono, email);
    }

    static void CrearNuevoMedico(AgendaClinica agenda)
    {
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? string.Empty;
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine() ?? string.Empty;
        Console.Write("Especialidad: ");
        string especialidad = Console.ReadLine() ?? string.Empty;
        Console.Write("Licencia: ");
        string licencia = Console.ReadLine() ?? string.Empty;

        agenda.AgregarMedico(nombre, apellido, especialidad, licencia);
    }
}
