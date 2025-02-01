using System;
using System.Collections.Generic;
using System.IO;

namespace GestorClub
{
    internal class GestorClub
    {
        static string nombreClub;
        static Dictionary<string, List<string>> paresEquipoJugadores = new Dictionary<string, List<string>>();
        static bool seguirMenu = true;

        static void Main(string[] args)
        {
            while (seguirMenu)
            {
                Menu();
                Console.ReadLine();
            }
        }

        public static void Menu()
        {
            LeerDatosClub();
            Console.Clear();
            Console.WriteLine("1) Dar de alta un equipo");
            Console.WriteLine("2) Dar de baja un equipo");
            Console.WriteLine("3) Dar de alta un jugador");
            Console.WriteLine("4) Dar de baja un jugador");
            Console.WriteLine("5) Listar equipos del club");
            Console.WriteLine("6) Listar jugadores de un equipo");
            Console.WriteLine("7) Guardar");
            Console.WriteLine("8) Salir y guardar");
            Console.WriteLine("9) Salir sin guardar");

            switch (Console.ReadLine())
            {
                case "1":
                    DarDeAlta("equipo");
                    break;
                case "2":
                    DarDeBaja("equipo");
                    break;
                case "3":
                    DarDeAlta("jugador");
                    break;
                case "4":
                    DarDeBaja("jugador");
                    break;
                case "5":
                    ListarEquipos();
                    break;
                case "6":
                    ListarJugadores();
                    break;
                case "7":
                    ActualizarArchivo();
                    break;
                case "8":
                    ActualizarArchivo();
                    seguirMenu = false;
                    break;
                case "9":
                    seguirMenu = false;
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
                string[] equipoYJugadores = infoClub[i].Split(':');
                string nombreEquipo = equipoYJugadores[0];
                string[] jugadoresArray = equipoYJugadores[1].Split(',');

                List<string> jugadoresLista = new List<string>(jugadoresArray);

                if (!paresEquipoJugadores.ContainsKey(nombreEquipo))
                {
                    paresEquipoJugadores.Add(nombreEquipo, jugadoresLista);
                }
            }
        }

        public static void DarDeAlta(string entrada)
        {
            if (entrada == "equipo")
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

            else if (entrada == "jugador")
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
        }

        public static void DarDeBaja(string entrada)
        {
            if (entrada == "equipo")
            {
                ListarEquipos();
                Console.WriteLine("Ingrese el nombre del equipo que desea dar de baja");
                string equipo = Console.ReadLine();

                paresEquipoJugadores.Remove(equipo);
                Console.WriteLine($"El {equipo} ha sido dado de baja.");
            }
            else if (entrada == "jugador")
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

        static void ActualizarArchivo()
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
                            jugadoresString += ",";
                    }
                    writer.WriteLine($"{equipo.Key}:{jugadoresString}");
                }
            }
            Console.WriteLine("Los cambios se han guardado en el archivo.");
        }

    }
}
