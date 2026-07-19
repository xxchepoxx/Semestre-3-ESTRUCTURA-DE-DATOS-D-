using System;
using System.Collections.Generic;

namespace PracticoExperimental2
{
    // Representa a una persona que desea comprar un asiento.
    public class Persona
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }

        public Persona(string cedula, string nombre)
        {
            Cedula = cedula;
            Nombre = nombre;
        }
    }

    // Representa uno de los 30 asientos de la atraccion.
    public class Asiento
    {
        public int Numero { get; private set; }
        public Persona Ocupante { get; private set; }

        public bool EstaOcupado
        {
            get { return Ocupante != null; }
        }

        public Asiento(int numero)
        {
            Numero = numero;
            Ocupante = null;
        }

        // Guarda la persona que compra este asiento.
        public void Ocupar(Persona persona)
        {
            Ocupante = persona;
        }
    }

    // Esta clase concentra las reglas del negocio de venta de asientos.
    public class SistemaVentaAsientos
    {
        private const int Capacidad = 30;

        // La cola conserva el orden de llegada: primero en entrar, primero en atenderse.
        private Queue<Persona> colaEspera;

        // El arreglo tiene exactamente 30 posiciones, una por cada asiento disponible.
        private Asiento[] asientos;

        public SistemaVentaAsientos()
        {
            colaEspera = new Queue<Persona>();
            asientos = new Asiento[Capacidad];

            for (int i = 0; i < Capacidad; i++)
            {
                asientos[i] = new Asiento(i + 1);
            }
        }

        public int PersonasEnEspera
        {
            get { return colaEspera.Count; }
        }

        // Agrega una persona al final de la cola de espera.
        public bool AgregarACola(string cedula, string nombre, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(cedula) || string.IsNullOrWhiteSpace(nombre))
            {
                mensaje = "La cedula y el nombre son obligatorios.";
                return false;
            }

            if (BuscarPorCedula(cedula) != null)
            {
                mensaje = "La cedula ya se encuentra registrada en el sistema.";
                return false;
            }

            colaEspera.Enqueue(new Persona(cedula.Trim(), nombre.Trim()));
            mensaje = "Persona agregada correctamente al final de la cola.";
            return true;
        }

        // Atiende a la primera persona de la cola y le entrega el siguiente asiento libre.
        public bool VenderSiguienteAsiento(out string mensaje)
        {
            int posicionLibre = BuscarPosicionLibre();

            if (posicionLibre == -1)
            {
                mensaje = "Los 30 asientos ya fueron vendidos.";
                return false;
            }

            if (colaEspera.Count == 0)
            {
                mensaje = "No hay personas en la cola de espera.";
                return false;
            }

            Persona personaAtendida = colaEspera.Dequeue();
            asientos[posicionLibre].Ocupar(personaAtendida);
            mensaje = "Venta realizada: " + personaAtendida.Nombre +
                      " recibe el asiento " + asientos[posicionLibre].Numero + ".";
            return true;
        }

        // Devuelve la cola como una lista para poder mostrarla sin eliminar elementos.
        public List<Persona> ObtenerColaEspera()
        {
            return new List<Persona>(colaEspera);
        }

        // Devuelve los asientos para el reporte de ventas.
        public Asiento[] ObtenerAsientos()
        {
            return asientos;
        }

        // Busca una persona tanto en la cola como en los asientos vendidos.
        public string BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return null;
            }

            foreach (Persona persona in colaEspera)
            {
                if (persona.Cedula.Equals(cedula.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return persona.Nombre + " esta en espera. Posicion en cola: " +
                           ObtenerPosicionEnCola(persona.Cedula) + ".";
                }
            }

            foreach (Asiento asiento in asientos)
            {
                if (asiento.EstaOcupado &&
                    asiento.Ocupante.Cedula.Equals(cedula.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return asiento.Ocupante.Nombre + " tiene el asiento " + asiento.Numero + ".";
                }
            }

            return null;
        }

        public int CantidadAsientosVendidos()
        {
            int vendidos = 0;

            foreach (Asiento asiento in asientos)
            {
                if (asiento.EstaOcupado)
                {
                    vendidos++;
                }
            }

            return vendidos;
        }

        private int BuscarPosicionLibre()
        {
            for (int i = 0; i < asientos.Length; i++)
            {
                if (!asientos[i].EstaOcupado)
                {
                    return i;
                }
            }

            return -1;
        }

        private int ObtenerPosicionEnCola(string cedula)
        {
            int posicion = 1;

            foreach (Persona persona in colaEspera)
            {
                if (persona.Cedula.Equals(cedula, StringComparison.OrdinalIgnoreCase))
                {
                    return posicion;
                }

                posicion++;
            }

            return 0;
        }
    }

    public class Program
    {
        private static SistemaVentaAsientos sistema = new SistemaVentaAsientos();

        public static void Main(string[] args)
        {
            string opcion;

            do
            {
                MostrarMenu();
                Console.Write("Seleccione una opcion: ");
                opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarPersona();
                        break;
                    case "2":
                        VenderSiguienteAsiento();
                        break;
                    case "3":
                        MostrarColaEspera();
                        break;
                    case "4":
                        MostrarReporteAsientos();
                        break;
                    case "5":
                        ConsultarPersona();
                        break;
                    case "6":
                        MostrarResumen();
                        break;
                    case "0":
                        Console.WriteLine("Programa finalizado.");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

                if (opcion != "0")
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (opcion != "0");
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("  VENTA DE 30 ASIENTOS - PARQUE DE DIVERSIONES");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. Registrar persona en la cola");
            Console.WriteLine("2. Vender asiento a la siguiente persona");
            Console.WriteLine("3. Ver cola de espera");
            Console.WriteLine("4. Ver reporte de asientos");
            Console.WriteLine("5. Consultar persona por cedula");
            Console.WriteLine("6. Ver resumen");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
        }

        private static void RegistrarPersona()
        {
            Console.Write("Cedula: ");
            string cedula = Console.ReadLine();
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            string mensaje;
            sistema.AgregarACola(cedula, nombre, out mensaje);
            Console.WriteLine(mensaje);
        }

        private static void VenderSiguienteAsiento()
        {
            string mensaje;
            sistema.VenderSiguienteAsiento(out mensaje);
            Console.WriteLine(mensaje);
        }

        private static void MostrarColaEspera()
        {
            List<Persona> personas = sistema.ObtenerColaEspera();

            Console.WriteLine("--- COLA DE ESPERA ---");
            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas esperando.");
                return;
            }

            for (int i = 0; i < personas.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + personas[i].Nombre +
                                  " - Cedula: " + personas[i].Cedula);
            }
        }

        private static void MostrarReporteAsientos()
        {
            Console.WriteLine("--- REPORTE DE ASIENTOS ---");

            foreach (Asiento asiento in sistema.ObtenerAsientos())
            {
                if (asiento.EstaOcupado)
                {
                    Console.WriteLine("Asiento " + asiento.Numero + ": " +
                                      asiento.Ocupante.Nombre + " (" + asiento.Ocupante.Cedula + ")");
                }
                else
                {
                    Console.WriteLine("Asiento " + asiento.Numero + ": Disponible");
                }
            }
        }

        private static void ConsultarPersona()
        {
            Console.Write("Ingrese la cedula a consultar: ");
            string cedula = Console.ReadLine();
            string resultado = sistema.BuscarPorCedula(cedula);

            if (resultado == null)
            {
                Console.WriteLine("No se encontro una persona con esa cedula.");
            }
            else
            {
                Console.WriteLine(resultado);
            }
        }

        private static void MostrarResumen()
        {
            int vendidos = sistema.CantidadAsientosVendidos();
            Console.WriteLine("Asientos vendidos: " + vendidos + "/30");
            Console.WriteLine("Asientos disponibles: " + (30 - vendidos));
            Console.WriteLine("Personas en cola: " + sistema.PersonasEnEspera);
        }
    }
}
