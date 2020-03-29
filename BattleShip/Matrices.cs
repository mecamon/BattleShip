using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    class Matrices
    {

        //Crea un jugador asignandole un tablero


        //Enum para saber cuanto se le agrega de posiciones a los barcos
        public enum agregadoABarcos 
        {
            BARCOS_DE_DOS = 1,
            BARCOS_DE_TRES = 2,
            BARCOS_DE_CUATRO = 3,
            BARCOS_DE_CINCO = 4,
        }

        public static void menu() 
        {
            Console.WriteLine("Bienvenido a BattleShip. Que funcion desea realizar: \n");
            Console.WriteLine(" 1- Iniciar el juego\n 2- Mostrar record");
            try
            {
                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion) 
                {
                    case 1:
                        inicioDelJuego();
                        break;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        public static string[,] asignarTablero()
        {
            string[,] nombreJugador = new string[10, 10];
            int contador = 1;
            string contadorStr;

            nombreJugador = new string[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    contadorStr = Convert.ToString(contador);
                    if (contadorStr.Length == 1)
                    {
                        contadorStr = "0" + contadorStr;
                    }
                    nombreJugador[i, j] = contadorStr;
                    contador++;
                }
            }

            return nombreJugador;
        }
        public static void imprimirCampo(Jugadores jugador)
        {
            Console.WriteLine("Tablero de {0}\n", jugador.nombre);

            int contador = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (contador == 10)
                    {
                        Console.Write("\n");
                        contador = 0;
                    }
                    Console.Write("{0}", jugador.tablero[i, j]);
                    Console.Write(" | ");
                    contador++;
                }
            }

            Console.WriteLine("\n");
        }
        public static void inicioDelJuego()
        {
            Jugadores jugador = new Jugadores();

            Console.Write("Inserte el nombre del jugador 1: ");
            try
            {
                jugador.nombre = Console.ReadLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            jugador.tablero = asignarTablero();

            posicion(jugador);
        }
        public static void posicion(Jugadores jugador)
        {
            int contador = 1;

            while (contador <= 5)
            {

                imprimirCampo(jugador);

                Console.WriteLine("\n");
                Console.Write("Donde desea posicionar del barco: ");
                string barco2 = Console.ReadLine();

                
                int pos_i = 0;
                int pos_j = 0;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (jugador.tablero[i, j] == barco2)
                        {
                            pos_i = i;
                            pos_j = j;
                            break;
                        }
                    }
                }

                switch (contador)
                {
                    case 1:
                        orientacionBarco(jugador, pos_i, pos_j, 2, 1, 8, (int)agregadoABarcos.BARCOS_DE_DOS);
                        break;

                    case 2:
                        orientacionBarco(jugador, pos_i, pos_j, 3, 2, 7, (int)agregadoABarcos.BARCOS_DE_TRES);
                        break;

                    case 3:
                        orientacionBarco(jugador, pos_i, pos_j, 3, 2, 7, (int)agregadoABarcos.BARCOS_DE_TRES);
                        break;

                    case 4:
                        orientacionBarco(jugador, pos_i, pos_j, 4, 3, 6, (int)agregadoABarcos.BARCOS_DE_CUATRO);
                        break;

                    case 5:
                        orientacionBarco(jugador, pos_i, pos_j, 5, 4, 5, (int)agregadoABarcos.BARCOS_DE_CINCO);
                        break;
                }
                contador++;
                Console.Clear();
            }

            imprimirCampo(jugador);
            Console.ReadKey();
        }
        public static void orientacionBarco(Jugadores jugador, int i, int j, int capacidadBarco, int arriba_izquierda, int abajo_derecha, int aSumar)
        {
            Console.WriteLine("Cual es la orientacion del barco de capacidad de {0}?", capacidadBarco);
            Console.WriteLine(" 1- Arriba\n 2- Abajo\n 3- Izquierda\n 4- Derecha");
            int resp = 0;


            try
            {
                resp = Convert.ToInt32(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        validacionArriba(jugador, i, j, arriba_izquierda, abajo_derecha, capacidadBarco, aSumar);
                        break;

                    case 2:
                        validacionAbajo(jugador, i, j, arriba_izquierda, abajo_derecha, capacidadBarco, aSumar);
                        break;

                    case 3:
                        validacionIzquierda(jugador, i, j, arriba_izquierda, abajo_derecha, capacidadBarco, aSumar);
                        break;

                    case 4:
                        validacionDerecha(jugador, i, j, arriba_izquierda, abajo_derecha, capacidadBarco, aSumar);
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas.");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
        public static void validacionArriba(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (i < arriba_izquierda)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posicion u orientacion: ");
                Console.WriteLine(" 1- Posicion\n 2- Orientacion");
                int resp = Convert.ToInt32(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        Console.Clear();
                        posicion(jugador);
                        break;

                    case 2:
                        Console.Clear();
                        imprimirCampo(jugador);
                        orientacionBarco(jugador, i, j, capacidadBarco, arriba_izquierda, abajo_derecha, aSumar);
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas.");
                        Console.Clear();
                        break;
                }
                Console.ReadKey();
            }
            else
            {
                jugador.tablero[i, j] = " X";
                //jugador.tablero[i - aSumar, j] = " X";
                
                for (int a = i - aSumar; a < i; a++) 
                {
                    jugador.tablero[a, j] = " X";
                }
            }
        }
        public static void validacionAbajo(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (i > abajo_derecha)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posicion u orientacion: ");
                Console.WriteLine(" 1- Posicion\n 2- Orientacion");
                int resp = Convert.ToInt32(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        Console.Clear();
                        posicion(jugador);
                        break;

                    case 2:
                        Console.Clear();
                        imprimirCampo(jugador);
                        orientacionBarco(jugador, i, j, capacidadBarco, arriba_izquierda, abajo_derecha, aSumar);
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas.");
                        Console.Clear();
                        break;

                }
            }
            else
            {
                jugador.tablero[i, j] = " X";

                for (int a = i + aSumar; a > i; a--)
                {
                    jugador.tablero[a, j] = " X";
                }
            }
        }
        public static void validacionDerecha(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (j > abajo_derecha)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posicion u orientacion: ");
                Console.WriteLine(" 1- Posicion\n 2- Orientacion");
                int resp = Convert.ToInt32(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        Console.Clear();
                        posicion(jugador);
                        break;

                    case 2:
                        Console.Clear();
                        imprimirCampo(jugador);
                        orientacionBarco(jugador, i, j, capacidadBarco, arriba_izquierda, abajo_derecha, aSumar);
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas.");
                        Console.Clear();
                        break;

                }
            }
            else
            {
                jugador.tablero[i, j] = " X";

                for (int a = j + aSumar; a > j; a--)
                {
                    jugador.tablero[i, a] = " X";
                }
            }
        }
        public static void validacionIzquierda(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (j < arriba_izquierda)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posicion u orientacion: ");
                Console.WriteLine(" 1- Posicion\n 2- Orientacion");
                int resp = Convert.ToInt32(Console.ReadLine());

                switch (resp)
                {
                    case 1:
                        Console.Clear();
                        posicion(jugador);
                        break;

                    case 2:
                        Console.Clear();
                        imprimirCampo(jugador);
                        orientacionBarco(jugador, i, j, capacidadBarco, arriba_izquierda, abajo_derecha, aSumar);
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas.");
                        Console.Clear();
                        break;

                }
            }
            else
            {
                jugador.tablero[i, j] = " X";

                for (int a = j - aSumar; a < j; a++)
                {
                    jugador.tablero[i, a] = " X";
                }
            }
        }
    }
}
