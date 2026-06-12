/*
 * ════════════════════════════════════════════════════════════════════════════════════
 * CLASE: Paciente
 * Propósito: Representa un paciente en el sistema de la clínica
 * Atributos: Datos personales e identificadores
 * ════════════════════════════════════════════════════════════════════════════════════
 */

/// <summary>
/// Clase que representa a un paciente del sistema
/// Almacena toda la información personal y de contacto
/// </summary>
public class Paciente
{
    // ─────────────────────────────────────────────────────────────────────────────
    // PROPIEDADES
    // ─────────────────────────────────────────────────────────────────────────────

    public int IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }

    // ─────────────────────────────────────────────────────────────────────────────
    // CONSTRUCTOR
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Constructor: Inicializa un nuevo paciente con sus datos personales
    /// </summary>
    /// <param name="id">Identificador único del paciente</param>
    /// <param name="nombre">Nombre del paciente</param>
    /// <param name="apellido">Apellido del paciente</param>
    /// <param name="cedula">Número de cédula único</param>
    /// <param name="telefono">Teléfono de contacto</param>
    /// <param name="email">Correo electrónico</param>
    public Paciente(int id, string nombre, string apellido, string cedula, string telefono, string email)
    {
        IdPaciente = id;
        Nombre = nombre;
        Apellido = apellido;
        Cedula = cedula;
        Telefono = telefono;
        Email = email;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // MÉTODOS
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// ToString: Retorna una representación formateada del paciente para mostrar en consola
    /// </summary>
    /// <returns>String con información del paciente</returns>
    public override string ToString()
    {
        return $"ID: {IdPaciente} | {Nombre} {Apellido} | Cédula: {Cedula} | Tel: {Telefono}";
    }
}
