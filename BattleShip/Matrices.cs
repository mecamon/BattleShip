using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    class Matrices
    {
        public static Jugadores Jugador1;
        public static Jugadores Jugador2;
        //Crea un jugador asignandole un tablero


        //Enum para saber cuanto se le agrega de posiciones a los barcos
        public enum agregadoABarcos
        {
            BARCOS_DE_DOS = 1,
            BARCOS_DE_TRES = 2,
            BARCOS_DE_CUATRO = 3,
            BARCOS_DE_CINCO = 4,
        }

        //Enum con número impactos de cada barco
        public enum capacidadBarcos
        {
            CAPACIDAD_BARCO2 = 2,
            CAPACIDAD_BARCO3,
            CAPACIDAD_BARCO4,
            CAPACIDAD_BARCO5,
        }

        //Menú principal
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
                        iniciandoJugador1();
                        iniciandoJugador2();
                        Console.Clear();
                        Func_Jugadores.atacar(ref Jugador1, ref Jugador2);
                        break;

                    case 2:
                        Console.Clear();
                        Func_Jugadores.mostrarRecord();
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Elija dentro de las opciones dadas. Presione cualquier tecla para volver al menu.");
                        Console.ReadKey();
                        menu();
                        break;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        //Para asginar un tablero vacio a cada jugador
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
            Console.WriteLine("\nTablero de {0}\n", jugador.nombre);

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
        public static void iniciandoJugador1()
        {
            Jugador1 = inicioDelJuego("Jugador 1");
        }
        public static void iniciandoJugador2()
        {
            Jugador2 = inicioDelJuego("Jugador 2");
        }

        //Creador de jugadores
        public static Jugadores inicioDelJuego(string player)
        {
            string resp = "";
            Jugadores jugador = new Jugadores();
            do
            {
                Console.Clear();
                Console.Write("Inserte el nombre del {0}: ", player);
                try
                {
                    jugador.nombre = Console.ReadLine();

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }

                jugador.barco2 = new string[2];
                jugador.barco3 = new string[3];
                jugador.barco3[0] = "0"; jugador.barco3[1] = "0"; jugador.barco3[2] = "0";
                jugador.barco3_2 = new string[3];
                jugador.barco3_2[0] = "0"; jugador.barco3[1] = "0"; jugador.barco3[2] = "0";
                jugador.barco4 = new string[4];
                jugador.barco5 = new string[5];

                jugador.tablero = asignarTablero();
                jugador.tableroVacio = asignarTablero();

                posicion(jugador);

                Console.Write("Presione 'Enter' si está de acuerdo con el posicionamiento o 'N' para cambiarlo: ");
                resp = Console.ReadLine();
            } while (resp == "N" || resp == "n");

            return jugador;
        }

        //Para posicinamiento inicial de los barcos. Se apoya mucho en el método de orientación de barcos
        public static void posicion(Jugadores jugador)
        {
            int contador = 1;

            while (contador <= 5)
            {

                imprimirCampo(jugador);

                Console.WriteLine("\n");
                Console.Write("Ingrese el número donde desea posicionar del barco: ");
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
                        orientacionBarco(jugador, pos_i, pos_j, (int)capacidadBarcos.CAPACIDAD_BARCO2, 1, 8, (int)agregadoABarcos.BARCOS_DE_DOS);
                        break;

                    case 2:
                        orientacionBarco(jugador, pos_i, pos_j, (int)capacidadBarcos.CAPACIDAD_BARCO3, 2, 7, (int)agregadoABarcos.BARCOS_DE_TRES);
                        break;

                    case 3:
                        orientacionBarco(jugador, pos_i, pos_j, (int)capacidadBarcos.CAPACIDAD_BARCO3, 2, 7, (int)agregadoABarcos.BARCOS_DE_TRES);
                        break;

                    case 4:
                        orientacionBarco(jugador, pos_i, pos_j, (int)capacidadBarcos.CAPACIDAD_BARCO4, 3, 6, (int)agregadoABarcos.BARCOS_DE_CUATRO);
                        break;

                    case 5:
                        orientacionBarco(jugador, pos_i, pos_j, (int)capacidadBarcos.CAPACIDAD_BARCO5, 4, 5, (int)agregadoABarcos.BARCOS_DE_CINCO);
                        break;
                }
                contador++;
                Console.Clear();
            }

            imprimirTodosLosBarcos(jugador);

            imprimirCampo(jugador);

            Console.ReadKey();
        }

        //Para decidir hacia qué dirección desean colocar el barco una vez hecho el posicionamiento inicial
        public static void orientacionBarco(Jugadores jugador, int i, int j, int capacidadBarco, int arriba_izquierda, int abajo_derecha, int aSumar)
        {
            Console.WriteLine("Cuál es la orientación del barco de capacidad de {0}?", capacidadBarco);
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

        //Métodos para validar si hay espacio suficiente hacia donde se quiere colocar el barco
        public static void validacionArriba(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (i < arriba_izquierda)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posición u orientación: ");
                Console.WriteLine(" 1- Posición\n 2- Orientación");
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
                        Console.ReadKey();
                        Console.Clear();
                        imprimirCampo(jugador);
                        orientacionBarco(jugador, i, j, capacidadBarco, arriba_izquierda, abajo_derecha, aSumar);
                        break;
                }
                Console.ReadKey();
            }
            else
            {
                int contador = 1;

                if (jugador.tablero[i, j] != " X")
                {
                    llenadoDeBarcos1(capacidadBarco, i, j, jugador);

                    jugador.tablero[i, j] = " X";
                }
                else
                {
                    Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                    Console.ReadKey();
                    Console.Clear();
                    posicion(jugador);
                }



                for (int a = i - aSumar; a < i; a++)
                {

                    if (jugador.tablero[a, j] != " X")
                    {
                        llenadoDeBarcos2(ref contador, capacidadBarco, a, j, jugador);

                        jugador.tablero[a, j] = " X";
                    }
                    else 
                    {
                        Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                        Console.ReadKey();
                        Console.Clear();
                        posicion(jugador);
                    }
                    
                }
            }
        }
        public static void validacionAbajo(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (i > abajo_derecha)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posición u orientación: ");
                Console.WriteLine(" 1- Posición\n 2- Orientación");
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
                int contador = 1;

                if (jugador.tablero[i, j] != " X") 
                {
                    llenadoDeBarcos1(capacidadBarco, i, j, jugador);

                    jugador.tablero[i, j] = " X";
                }
                else
                {
                    Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                    Console.ReadKey();
                    Console.Clear();
                    posicion(jugador);
                }


                for (int a = i + aSumar; a > i; a--)
                {
                    if (jugador.tablero[a, j] != " X") 
                    {
                        llenadoDeBarcos2(ref contador, capacidadBarco, a, j, jugador);

                        jugador.tablero[a, j] = " X";
                    }
                    else
                    {
                        Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                        Console.ReadKey();
                        Console.Clear();
                        posicion(jugador);
                    }

                }
            }
        }
        public static void validacionDerecha(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (j > abajo_derecha)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posición u orientación: ");
                Console.WriteLine(" 1- Posición\n 2- Orientación");
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
                int contador = 1;

                if (jugador.tablero[i, j] != " X") 
                {
                    llenadoDeBarcos1(capacidadBarco, i, j, jugador);

                    jugador.tablero[i, j] = " X";
                }
                else
                {
                    Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                    Console.ReadKey();
                    Console.Clear();
                    posicion(jugador);
                }


                for (int a = j + aSumar; a > j; a--)
                {
                    if (jugador.tablero[i, a] != " X") 
                    {
                        llenadoDeBarcos2(ref contador, capacidadBarco, i, a, jugador);

                        jugador.tablero[i, a] = " X";
                    }
                    else
                    {
                        Console.WriteLine("Hay un barco estorbando en esa posicion.Presione cualquier tecla para elijir posicion.");
                        Console.ReadKey();
                        Console.Clear();
                        posicion(jugador);
                    }


                }
            }
        }
        public static void validacionIzquierda(Jugadores jugador, int i, int j, int arriba_izquierda, int abajo_derecha, int capacidadBarco, int aSumar)
        {
            if (j < arriba_izquierda)
            {
                Console.WriteLine("No hay espacio suficiente hacia arriba para colocar el barco. Elija otra posición u orientación: ");
                Console.WriteLine(" 1- Posición\n 2- Orientación");
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
                int contador = 1;

                if (jugador.tablero[i, j] != " X") 
                {
                    llenadoDeBarcos1(capacidadBarco, i, j, jugador);

                    jugador.tablero[i, j] = " X";
                }
                else
                {
                    Console.WriteLine("Hay un barco estorbando en esa posición.Presione cualquier tecla para elijir posición.");
                    Console.ReadKey();
                    Console.Clear();
                    posicion(jugador);
                }


                for (int a = j - aSumar; a < j; a++)
                {
                    if (jugador.tablero[i, a] != " X") 
                    {
                        llenadoDeBarcos2(ref contador, capacidadBarco, i, a, jugador);

                        jugador.tablero[i, a] = " X";
                    }
                    else
                    {
                        Console.WriteLine("Hay un barco estorbando en esa posición.Presione cualquier tecla para elegir posición.");
                        Console.ReadKey();
                        Console.Clear();
                        posicion(jugador);
                    }

                }
            }
        }

        //Para imprimir un tablero nuevo que no revele la posición del jugador enemigo al atacante
        public static void imprimirTableroVacio(string[,] tablero)
        {
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
                    Console.Write("{0}", tablero[i, j]);
                    Console.Write(" | ");
                    contador++;
                }
            }

            Console.WriteLine("\n");
        }

        //Para asignar los números de las posiciones a los en el tablero a los barcos
        public static void llenadoDeBarcos1(int capacidadBarco, int a, int j_or_i, Jugadores jugador)
        {
            if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO2)
            {
                jugador.barco2[0] = jugador.tablero[a, j_or_i];
            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO3)
            {
                if (jugador.barco3[0] == "0")
                {
                    jugador.barco3[0] = jugador.tablero[a, j_or_i];
                }
                else 
                {
                    jugador.barco3_2[0] = jugador.tablero[a, j_or_i];
                }
            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO4)
            {
                jugador.barco4[0] = jugador.tablero[a, j_or_i];
            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO5)
            {
                jugador.barco5[0] = jugador.tablero[a, j_or_i];
            }
        }
        public static void llenadoDeBarcos2(ref int contador, int capacidadBarco, int a, int j_or_i, Jugadores jugador)
        {
            if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO2)
            {
                jugador.barco2[contador] = jugador.tablero[a, j_or_i];
                contador++;
            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO3)
            {

                if (jugador.barco3[2] == "0")
                {
                    jugador.barco3[contador] = jugador.tablero[a, j_or_i];
                    contador++;
                }
                else 
                {
                    jugador.barco3_2[contador] = jugador.tablero[a, j_or_i];
                    contador++;
                }

            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO4)
            {
                jugador.barco4[contador] = jugador.tablero[a, j_or_i];
                contador++;
            }
            else if (capacidadBarco == (int)capacidadBarcos.CAPACIDAD_BARCO5)
            {
                jugador.barco5[contador] = jugador.tablero[a, j_or_i];
                contador++;
            }
        }

        public static void imprimirTodosLosBarcos(Jugadores jugador)
        {
            Console.WriteLine("{0}: ", jugador.nombre);

            Console.WriteLine("Posiciones ocupadas por los barcos: ");
            Console.WriteLine("Barco de 2 impactos");
            imprimirPosicionesBarcos(jugador.barco2);
            Console.WriteLine("");

            Console.WriteLine("Barco de 3 impactos");
            imprimirPosicionesBarcos(jugador.barco3);
            Console.WriteLine("");

            Console.WriteLine("Barco de 3 impactos");
            imprimirPosicionesBarcos(jugador.barco3_2);
            Console.WriteLine("");

            Console.WriteLine("Barco de 4 impactos");
            imprimirPosicionesBarcos(jugador.barco4);
            Console.WriteLine("");

            Console.WriteLine("Barco de 5 impactos");
            imprimirPosicionesBarcos(jugador.barco5);
            Console.WriteLine("");
        }
        public static void imprimirPosicionesBarcos(Array arr)
        {
            
            foreach (string item in arr)
            {
                Console.Write("{0} ", item);
            }
        }
    }
}
