using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BattleShip
{
    [Serializable]
    class Func_Jugadores
    {

        public static List<Resultados> resultadosJuego { get; set; }
        public static void atacar(Jugadores jugador1, Jugadores jugador2)
        {
            Jugadores JugadorEnTurno;
            Jugadores JugadorEnEspera;

            JugadorEnTurno = jugador1;
            JugadorEnEspera = jugador2;

            bool turnos = true;
            bool turnosRev = true;

            //Scores de los jugadores
            int contadorJugador1 = 0;
            int contadorJugador2 = 0;

            while (contadorJugador1 != 17 || contadorJugador2 != 17)
            {
                if (turnos == turnosRev)
                {
                    if (JugadorEnTurno == jugador1)
                    {
                        JugadorEnTurno = jugador2;
                        JugadorEnEspera = jugador1;
                    }
                    else if (JugadorEnTurno == jugador2)
                    {
                        JugadorEnTurno = jugador1;
                        JugadorEnEspera = jugador2;
                    }
                }
                else
                {
                    turnos = turnosRev;
                }

                Console.WriteLine("Turno del jugador {0}\n", JugadorEnTurno.nombre);

                Matrices.imprimirTableroVacio(JugadorEnEspera.tableroVacio);

                Console.Write("Donde desea atacar del barco: ");
                string posicionAtacada = Console.ReadLine();


                //Verificando si hay barcos en la posicion atacada
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (JugadorEnEspera.tableroVacio[i, j] == posicionAtacada)
                        {
                            if (JugadorEnEspera.tablero[i, j] == " X")
                            {
                                JugadorEnEspera.tableroVacio[i, j] = " 0";
                                Console.Write("Disparo exitoso. Presione cualquier tecla para atacar nuevamente.");
                                Console.ReadKey();
                                if (JugadorEnTurno == jugador1)
                                {
                                    contadorJugador1++;
                                    turnos = false;
                                }
                                else if (JugadorEnTurno == jugador2)
                                {
                                    contadorJugador2++;
                                    turnos = false;
                                }
                                break;
                            }
                        }
                    }
                }

                if (turnos == turnosRev)
                {
                    Console.Write("Disparo fallido. Presione cualquier tecla para ceder turno al otro jugador.");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            asignandoValoresFinales(jugador1, jugador2, contadorJugador1, contadorJugador2);
            guardarResultados(jugador1, jugador2);
        }

        public static void asignandoValoresFinales(Jugadores jugador1, Jugadores jugador2, int puntJugador1, int puntJugador2)
        {
            jugador1.puntuacionFinal = puntJugador1;
            jugador2.puntuacionFinal = puntJugador2;
        }

        public static void guardarResultados(Jugadores jugador1, Jugadores jugador2)
        {
            if (File.Exists("Resultados.dat"))
            {
                resultadosJuego = (List<Resultados>)deserializarResultados();
            }

            Resultados Result = new Resultados();

            Result.nombreGanador = jugador1.puntuacionFinal > jugador2.puntuacionFinal ? jugador1.nombre : jugador2.nombre;
            Result.puntuacionGanador = jugador1.puntuacionFinal > jugador2.puntuacionFinal ? jugador1.puntuacionFinal : jugador2.puntuacionFinal;

            Result.nombrePerdedor = jugador1.puntuacionFinal < jugador2.puntuacionFinal ? jugador1.nombre : jugador2.nombre;
            Result.puntuacionPerdedor = jugador1.puntuacionFinal < jugador2.puntuacionFinal ? jugador1.puntuacionFinal : jugador2.puntuacionFinal;

            resultadosJuego.Add(Result);

            serializarResultados(resultadosJuego);//Guardando los resultados del juego en el archivo binario
        }

        public static void serializarResultados(object Lista)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Resultados.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Lista);
            stream.Close();
        }

        public static object deserializarResultados()
        {
            object lista;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Resultados.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            lista = (object)formatter.Deserialize(stream);
            stream.Close();

            return lista;
        }

        public static void mostrarRecord() 
        {
            if (File.Exists("Resultados.dat"))
            {
                resultadosJuego = (List<Resultados>)deserializarResultados();

                foreach (Resultados item in resultadosJuego) 
                {
                    Console.WriteLine("Resultados del juego #{0}\n", resultadosJuego.IndexOf(item));
                    Console.Write("Ganador: {0}\t", item.nombreGanador);
                    Console.Write("Puntuacion: {0}", item.puntuacionGanador);
                    Console.WriteLine("");

                    Console.Write("Perdedor: {0}\t", item.nombrePerdedor);
                    Console.Write("Puntuacion: {0}", item.nombrePerdedor);

                    Console.WriteLine("\n");
                }
            }
            else 
            {
                Console.WriteLine("Todavia no hay nada guardado para mostrar. Presione cualquier tecla para volver al menu.");
                Console.ReadKey();
                Console.Clear();
                Matrices.menu();
            }
        }
    }
}
