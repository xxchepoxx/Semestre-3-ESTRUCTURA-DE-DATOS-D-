/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * ESTRUCTURA: HorarioDisponible
 * Propósito: Almacenar información sobre horarios y su disponibilidad
 * ════════════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Estructura que representa un horario disponible en la clínica
/// Almacena la hora y su estado de disponibilidad
/// </summary>
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
