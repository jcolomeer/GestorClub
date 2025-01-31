using System;
using System.Collections.Generic;
using System.IO;


namespace GestorClub
{
    internal class GestorClub
    {
        static string nombreClub;
        static Dictionary<string, string[]> paresEquipoJugadores = new Dictionary<string, string[]>();

        static void Main(string[] args)
        {
            LeerDatosClub();
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1) Dar de alta un equipo");
            Console.WriteLine("2) Dar de baja un equipo");
            Console.WriteLine("3) Dar de alta un jugador");
            Console.WriteLine("4) Dar de baja un jugador");
            Console.WriteLine("5) Salir");

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
                string nombre = infoClub[i].Split(':')[0];
                string[] jugadores = infoClub[i].Split(':')[1].Split(',');

                paresEquipoJugadores.Add(nombre, jugadores);
            }
        }

        public static void DarDeAlta(string s)
        {

        }
        public static void DarDeBaja(string s)
        {

        }

    }
}
