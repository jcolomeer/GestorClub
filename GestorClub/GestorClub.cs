using System;
using System.Collections.Generic;
using System.IO;

namespace GestorClub
{
    internal class GestorClub
    {
        static string nombreClub;
        static Dictionary<string, List<string>> paresEquipoJugadores = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            LeerDatosClub();
            Menu();
            Console.ReadLine();
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
            Console.WriteLine("7) Salir");

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
                    break;
                default:
                    break;
            }
            ActualizarArchivo();

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

                paresEquipoJugadores.Add(nombreEquipo, jugadoresLista);
            }
        }

        static void ListarEquipos()
        {
            foreach (var equipo in paresEquipoJugadores)
            {
                Console.WriteLine($"- {equipo.Key}");
            }
        }

        static void ListarJugadores()
        {
            bool equipoRegistrado = false;
            string equipo = "";

            Console.WriteLine("Introduce el nombre de un equipo:");

            while (!equipoRegistrado)
            {
                equipo = Console.ReadLine();
                if (paresEquipoJugadores.ContainsKey(equipo))
                    equipoRegistrado = true;
                else
                    Console.WriteLine("El equipo no pertenece a este club");
            }

        }

        public static void DarDeAlta(string entrada)
        {
            if (entrada == "equipo")
            {
                Console.WriteLine("Dar de alta un equipo (aún no implementado).");
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
                    Console.WriteLine($"El jugador {jugador} ha sido dado de alta en el equipo {equipo}.");

                }
                else
                {
                    Console.WriteLine("El equipo no existe.");
                }
            }
        }

        public static void DarDeBaja(string entrada)
        {

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
                        {
                            jugadoresString += ",";
                        }
                    }
                    writer.WriteLine($"{equipo.Key}:{jugadoresString}");
                }
            }
            Console.WriteLine("Los cambios se han guardado en el archivo.");
        }

    }
}
