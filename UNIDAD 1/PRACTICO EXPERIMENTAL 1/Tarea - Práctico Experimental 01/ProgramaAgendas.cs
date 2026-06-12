using System;

// ProgramaAgendas.cs — contiene únicamente el punto de entrada `Program`.
// Las clases `Paciente`, `Medico`, `Turno`, `HorarioDisponible` y `AgendaClinica`
// están definidas en sus propios archivos separados.

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
        Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║      SISTEMA DE AGENDA DE TURNOS - CLÍNICA MÉDICA              ║");
        Console.WriteLine("║              Universidad Estatal Amazónica                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

        var agenda = new AgendaClinica();

        // Datos de ejemplo mínimos — utiliza los métodos de AgendaClinica
        agenda.AgregarPaciente("Juan", "García", "1234567890", "0987654321", "juan@email.com");
        agenda.AgregarMedico("Pedro", "Sánchez", "Cardiología", "LIC-001");
        agenda.AgregarTurno(1, 1, DateTime.Now.Date.AddDays(1), "09:00", "Consulta inicial");

        // Muestra un reporte simple para comprobar ejecución
        agenda.GenerarReporteTurnos();

        Console.WriteLine("\nPresione Enter para finalizar...");
        Console.ReadLine();
    }
}
