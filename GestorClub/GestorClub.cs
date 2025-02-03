using System;
using System.Collections.Generic;
using System.IO;



namespace GestorClub
{
    internal class GestorClub
    {
        static string nombreClub;
        static Dictionary<string, List<string>> paresEquipoJugadores = new Dictionary<string, List<string>>();
        static bool menuActivo = true;
        static char separadorJugadores = ';', separadorEquipo = ':';

        static void Main(string[] args)
        {
            LeerDatosClub();

            while (menuActivo)
            {
                Menu();
            }

            GuardarDatosClub();
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1) Dar de alta un equipo");
            Console.WriteLine("2) Dar de baja un equipo");
            Console.WriteLine("3) Dar de alta un jugador");
            Console.WriteLine("4) Dar de baja un jugador");
            Console.WriteLine("5) Listar equipos del club");
            Console.WriteLine("6) Listar jugadores de un equipo");
            Console.WriteLine("7) Guardar");
            Console.WriteLine("8) Salir");


            switch (Console.ReadLine())
            {
                case "1":
                    DarDeAltaEquipo();
                    break;
                case "2":
                    DarDeBajaEquipo();
                    break;
                case "3":
                    DarDeAltaJugador();
                    break;
                case "4":
                    DarDeBajaJugador();
                    break;
                case "5":
                    ListarEquipos();
                    break;
                case "6":
                    ListarJugadores();
                    break;
                case "7":
                    GuardarDatosClub();
                    break;
                case "8":
                    menuActivo = false;
                    break;
                default:
                    break;
            }
        }

        static void LeerDatosClub()
        {
            string[] infoClub = File.ReadAllLines("../../FundacionEsplai.txt");
            nombreClub = infoClub[0];

            for (int i = 1; i < infoClub.Length; i++)
            {
                string[] equipoYJugadores = infoClub[i].Split(separadorEquipo);
                string nombreEquipo = equipoYJugadores[0];
                string[] jugadoresArray = equipoYJugadores[1].Split(separadorJugadores);

                List<string> jugadoresLista = new List<string>(jugadoresArray);

                if (!paresEquipoJugadores.ContainsKey(nombreEquipo))
                {
                    paresEquipoJugadores.Add(nombreEquipo, jugadoresLista);
                }
            }
        }

        public static void DarDeAltaJugador()
        {
            ListarEquipos();
            Console.WriteLine("Ingrese el nombre del equipo en el que desea dar de alta un jugador:");
            string equipo = Console.ReadLine();

            if (paresEquipoJugadores.ContainsKey(equipo))
            {
                Console.WriteLine("Ingrese el nombre del jugador a dar de alta:");
                string jugador = Console.ReadLine();

                paresEquipoJugadores[equipo].Add(jugador);
                Console.WriteLine($"El jugador {jugador} ha sido dado de alta en el {equipo}.");
            }
            else
                Console.WriteLine("El equipo no existe.");
        }

        public static void DarDeAltaEquipo()
        {
            Console.WriteLine("Ingrese el nombre del equipo que desea dar de alta");
            string equipo = Console.ReadLine();

            if (!paresEquipoJugadores.ContainsKey(equipo))
            {
                paresEquipoJugadores.Add(equipo, new List<string>());
                Console.WriteLine($"El {equipo} ha sido dado de alta.");
            }
            else
                Console.WriteLine($"El {equipo} ya existe.");
        }

        public static void DarDeBajaJugador()
        {
            ListarEquipos();
            Console.WriteLine("Ingrese el nombre del equipo en el que desea dar de baja un jugador:");
            string equipo = Console.ReadLine();

            if (paresEquipoJugadores.ContainsKey(equipo))
            {
                ListarJugadores();
                Console.WriteLine("Ingrese el nombre del jugador a dar de baja:");
                string jugador = Console.ReadLine();

                paresEquipoJugadores[equipo].Remove(jugador);
                Console.WriteLine($"El jugador {jugador} ha sido dado de baja en el {equipo}.");
            }
            else
                Console.WriteLine("El equipo no existe.");
        }

        public static void DarDeBajaEquipo()
        {
            ListarEquipos();
            Console.WriteLine("Ingrese el nombre del equipo que desea dar de baja");
            string equipo = Console.ReadLine();

            paresEquipoJugadores.Remove(equipo);
            Console.WriteLine($"El {equipo} ha sido dado de baja.");
        }

        static void ListarEquipos()
        {
            foreach (var equipo in paresEquipoJugadores)
                Console.WriteLine($"- {equipo.Key}");
        }

        static void ListarJugadores()
        {
            ListarEquipos();

            Console.WriteLine("Introduce el nombre de un equipo:");
            string equipo = Console.ReadLine();

            if (paresEquipoJugadores.ContainsKey(equipo))
            {
                if (paresEquipoJugadores[equipo].Count == 0)
                {
                    Console.WriteLine($"El {equipo} no tiene jugadores.");
                }
                else
                {
                    Console.WriteLine($"Jugadores en el {equipo}:");
                    foreach (var jugador in paresEquipoJugadores[equipo])
                        Console.WriteLine($"- {jugador}");
                }
            }
            else
                Console.WriteLine("El equipo no pertenece a este club.");
        }

        static void GuardarDatosClub()
        {
            using (StreamWriter writer = new StreamWriter("../../FundacionEsplai.txt"))
            {
                writer.WriteLine(nombreClub);
                foreach (var equipo in paresEquipoJugadores)
                {
                    string jugadoresString = "";
                    for (int i = 0; i < equipo.Value.Count; i++)
                    {
                        jugadoresString += equipo.Value[i];
                        if (i < equipo.Value.Count - 1)
                            jugadoresString += separadorJugadores;
                    }
                    writer.WriteLine($"{equipo.Key}{separadorEquipo}{jugadoresString}");
                }
            }
            Console.WriteLine("Los cambios se han guardado en el archivo.");
        }

    }
}
