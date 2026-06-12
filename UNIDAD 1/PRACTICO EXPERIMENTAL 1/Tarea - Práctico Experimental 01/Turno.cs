using System;

/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * CLASE: Turno
 * Propósito: Representa una cita médica que relaciona paciente con médico
 * Atributos: Información de la cita (fecha, hora, motivo, estado)
 * ════════════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Clase que representa un turno médico del sistema
/// Relaciona a un paciente con un médico en una fecha y hora específica
/// </summary>
public class Turno
{
    // ─────────────────────────────────────────────────────────────────────────────
    // PROPIEDADES
    // ─────────────────────────────────────────────────────────────────────────────

    public int IdTurno { get; set; }
    public Paciente Paciente { get; set; }
    public Medico Medico { get; set; }
    public DateTime Fecha { get; set; }
    public string Hora { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; } // Estados: Pendiente, Confirmado, Completado, Cancelado

    // ─────────────────────────────────────────────────────────────────────────────
    // CONSTRUCTOR
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Constructor: Inicializa un nuevo turno relacionando paciente y médico
    /// </summary>
    /// <param name="id">Identificador único del turno</param>
    /// <param name="paciente">Referencia al objeto Paciente</param>
    /// <param name="medico">Referencia al objeto Medico</param>
    /// <param name="fecha">Fecha de la cita</param>
    /// <param name="hora">Hora de la cita en formato HH:mm</param>
    /// <param name="motivo">Motivo o descripción de la consulta</param>
    /// <param name="estado">Estado actual del turno (por defecto Pendiente)</param>
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

    // ─────────────────────────────────────────────────────────────────────────────
    // MÉTODOS
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// ToString: Retorna una representación formateada del turno para mostrar en consola
    /// </summary>
    /// <returns>String con información detallada del turno</returns>
    public override string ToString()
    {
        return $"Turno #{IdTurno} | Paciente: {Paciente.Nombre} {Paciente.Apellido} | " +
               $"Médico: Dr. {Medico.Nombre} {Medico.Apellido} | " +
               $"Fecha: {Fecha:dd/MM/yyyy} {Hora} | Estado: {Estado}";
    }
}
