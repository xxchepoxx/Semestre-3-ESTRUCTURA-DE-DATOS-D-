/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * CLASE: Medico
 * Propósito: Representa un médico que trabaja en la clínica
 * Atributos: Datos profesionales e identificadores
 * ════════════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Clase que representa a un médico del sistema
/// Almacena toda la información profesional y de contacto
/// </summary>
public class Medico
{
    // ─────────────────────────────────────────────────────────────────────────────
    // PROPIEDADES
    // ─────────────────────────────────────────────────────────────────────────────

    public int IdMedico { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Especialidad { get; set; }
    public string NumeroLicencia { get; set; }

    // ─────────────────────────────────────────────────────────────────────────────
    // CONSTRUCTOR
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Constructor: Inicializa un nuevo médico con sus datos profesionales
    /// </summary>
    /// <param name="id">Identificador único del médico</param>
    /// <param name="nombre">Nombre del médico</param>
    /// <param name="apellido">Apellido del médico</param>
    /// <param name="especialidad">Especialidad médica (Cardiología, Dermatología, etc.)</param>
    /// <param name="numLicencia">Número de licencia profesional</param>
    public Medico(int id, string nombre, string apellido, string especialidad, string numLicencia)
    {
        IdMedico = id;
        Nombre = nombre;
        Apellido = apellido;
        Especialidad = especialidad;
        NumeroLicencia = numLicencia;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // MÉTODOS
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// ToString: Retorna una representación formateada del médico para mostrar en consola
    /// </summary>
    /// <returns>String con información del médico</returns>
    public override string ToString()
    {
        return $"ID: {IdMedico} | Dr. {Nombre} {Apellido} | Especialidad: {Especialidad}";
    }
}
