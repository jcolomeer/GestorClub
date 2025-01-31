using System;

namespace GestorClub
{
    internal class GestorClub
    {
        static void Main(string[] args)
        {
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

        public static void DarDeAlta(string s)
        {

        }
        public static void DarDeBaja(string s)
        {

        }
    }
}
