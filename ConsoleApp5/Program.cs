using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            NodoSede sanJuanDeLurigancho = CrearSedeConDoctores("San Juan de Lurigancho", new string[] { "Dr. Juan Perez", "Dra. Maria Sanchez", "Dr. Julio " });
            NodoSede Magdalena = CrearSedeConDoctores("Magdalena", new string[] { "Dr. Carlos Gomez", "Dra. Ana Lopez" });
            NodoSede Ate = CrearSedeConDoctores("Ate", new string[] { "Dr. Pablo Ramirez", "Dr. Jose Vilchez " });
            NodoSede Comas = CrearSedeConDoctores("Comas", new string[] { "Dra. Julia Rojas", "Dr. Roberto Fernandez" });
            NodoSede Surquillo = CrearSedeConDoctores("Surquillo", new string[] { "Dr. Luis Castro", "Dra. Carmen Rivera" });


            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("***********************************************");
                Console.WriteLine("*------- CLINICA OFTALMOLOGICA VISION --------*");
                Console.WriteLine("***********************************************");
                Console.WriteLine("*                                     *********");
                Console.WriteLine("*       ███████████████████           *********");
                Console.WriteLine("*       █_▄▄▄▄ ______▄▄▄▄_█           *********");
                Console.WriteLine("*       █_▐┼┼▌_█████_▐┼┼▌_█           *********");
                Console.WriteLine("*       █_▐┼┼▌_█████_▐┼┼▌_█           *********");
                Console.WriteLine("*       █______█████______█           *********");
                Console.WriteLine("*                                     *********");
                Console.WriteLine("***********************************************");
                Console.WriteLine("¡Bienvenido!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[1] San Juan de Lurigancho");
                Console.WriteLine("[2] Magdalena ");
                Console.WriteLine("[3] Ate");
                Console.WriteLine("[4] Comas");
                Console.WriteLine("[5] Surquillo");
                Console.WriteLine("[6] SALIR ");
                Console.WriteLine("- ¿En qué sede desea reservar su cita? : ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        MostrarSede(sanJuanDeLurigancho);
                        break;
                    case "2":
                        MostrarSede(Magdalena);
                        break;
                    case "3":
                        MostrarSede(Ate);
                        break;
                    case "4":
                        MostrarSede(Comas);
                        break;
                    case "5":
                        MostrarSede(Surquillo);
                        break;
                    case "6":
                        salir = true;
                        break;
                }

                Console.ReadLine();
                Console.ReadKey();
            }
        }

        static NodoSede CrearSedeConDoctores(string nombreSede, string[] nombresDoctores)
        {
            NodoSede sede = new NodoSede { Nombre = nombreSede };

            NodoDoctor ultimoDoctor = null;
            foreach (string nombreDoctor in nombresDoctores)
            {
                NodoDoctor nuevoDoctor = new NodoDoctor
                {
                    Nombre = nombreDoctor,
                    Especialidad = "Oftalmología",
                    Horarios = new List<Horario>
                    {
                        new Horario { DiaSemana = "Lunes", HoraInicio = "8:00 am", HoraFin = "8:45 am", Disponible = true },
                        new Horario { DiaSemana = "Lunes", HoraInicio = "9:00 am", HoraFin = "9:45 am", Disponible = true },
                        new Horario { DiaSemana = "Martes", HoraInicio = "10:00 am", HoraFin = "10:45 am", Disponible = true }
                    }
                };

                if (ultimoDoctor == null)
                {
                    sede.Doctores = nuevoDoctor;
                }
                else
                {
                    ultimoDoctor.Siguiente = nuevoDoctor;
                }

                ultimoDoctor = nuevoDoctor;
            }

            return sede;
        }

       
        static void MostrarSede(NodoSede sede)
        {
            Console.Clear();
            Console.WriteLine("Sede seleccionada: " + sede.Nombre);
            Console.WriteLine("Doctores disponibles:");

            NodoDoctor doctorActual = sede.Doctores;
            if (doctorActual == null)
            {
                Console.WriteLine("No hay doctores asignados a esta sede.");
            }
            else
            {
                while (doctorActual != null)
                {
                    Console.WriteLine("- " + doctorActual.Nombre + " (Especialidad: " + doctorActual.Especialidad + ")");
                    Console.WriteLine("  Horarios disponibles:");

                    for (int i = 0; i < doctorActual.Horarios.Count; i++)
                    {
                        if (doctorActual.Horarios[i].Disponible)
                        {
                            Console.WriteLine("    [" + (i + 1) + "] " + doctorActual.Horarios[i].DiaSemana + " de "
                                              + doctorActual.Horarios[i].HoraInicio + " a " + doctorActual.Horarios[i].HoraFin);
                        }
                    }

                    Console.WriteLine("Seleccione un horario para reservar (o 0 para cancelar):");
                    string opcion = Console.ReadLine();
                    int horarioSeleccionado;

                    if (int.TryParse(opcion, out horarioSeleccionado) && horarioSeleccionado > 0 && horarioSeleccionado <= doctorActual.Horarios.Count)
                    {
                        if (doctorActual.Horarios[horarioSeleccionado - 1].Disponible)
                        {
                            doctorActual.Horarios[horarioSeleccionado - 1].Disponible = false;  // Marcar el horario como ocupado
                            Console.WriteLine("Cita reservada exitosamente.");
                        }   
                        else
                        {
                            Console.WriteLine("El horario ya está ocupado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Reserva cancelada.");
                    }

                    doctorActual = doctorActual.Siguiente;
                }
            }

            Console.WriteLine("Presione Enter para regresar al menú principal...");
            Console.ReadLine();
        }
    }

    
        class NodoSede
        {
            public string Nombre;
            public NodoDoctor Doctores;
            public NodoSede Siguiente;
        }

        class NodoDoctor
        {
            public string Nombre;
            public string Especialidad;
            public NodoDoctor Siguiente;
            public List<Horario> Horarios { get; set; } = new List<Horario>();
        }

        class Horario
        {
            public string DiaSemana { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFin { get; set; }
            public bool Disponible { get; set; }  // Indica si el horario está disponible
        }
}