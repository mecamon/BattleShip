using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace BattleShip
{
    [Serializable]
    class Func_Jugadores
    {

        public static List<Resultados> resultadosJuego = new List<Resultados>();

        // Metodo que maneja los ataques los jugadores y los turnos
        public static void atacar(ref Jugadores jugador1, ref Jugadores jugador2)
        {
            Jugadores JugadorEnTurno;
            Jugadores JugadorEnEspera;

            JugadorEnTurno = jugador1;
            JugadorEnEspera = jugador2;

            bool turnos = true;
            bool turnosRev = true;

            //Scores de los jugadores para identificar quien gana primero
            int contadorJugador1 = 0;
            int contadorJugador2 = 0;

            while (contadorJugador1 < 19 || contadorJugador2 < 19)
            {
                if (contadorJugador1 == 17 || contadorJugador2 == 17)
                {
                    calculandoPuntos(jugador1, jugador2);
                    guardarResultados(jugador1, jugador2);
                    break;
                }

                //Comparando si ha cambiado el booleano para cambiar el turno del jugador
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
                                derriboDeBarcos(ref JugadorEnEspera, i, j, posicionAtacada); //Cambiando los valores de barcos atacados

                                JugadorEnEspera.tableroVacio[i, j] = " 0";
                                Console.Write("Disparo exitoso. Presione cualquier tecla para atacar nuevamente.");
                                //Console.ReadKey();

                                if (JugadorEnTurno == jugador1)
                                {
                                    contadorJugador1++;
                                    Console.WriteLine("\n{0} acierto!", contadorJugador1);
                                    Console.ReadKey();
                                    turnos = false;
                                }
                                else if (JugadorEnTurno == jugador2)
                                {
                                    contadorJugador2++;
                                    Console.WriteLine("\n{0} acierto!", contadorJugador2);
                                    Console.ReadKey();
                                    turnos = false;
                                }
                                break;
                            }
                            else 
                            {
                                JugadorEnEspera.tableroVacio[i, j] = " F";
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

        }

        //Para guardar los resultador del juego en un archivo binario
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

            Console.ReadKey();
        }

        //Serializador
        public static void serializarResultados(object Lista)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Resultados.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Lista);
            stream.Close();
        }

        //Deserializador
        public static object deserializarResultados()
        {
            object lista;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Resultados.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            lista = (object)formatter.Deserialize(stream);
            stream.Close();

            return lista;
        }

        //Para imprimir el record de los juegos previos
        public static void mostrarRecord() 
        {
            if (File.Exists("Resultados.dat"))
            {
                resultadosJuego = (List<Resultados>)deserializarResultados();

                foreach (Resultados item in resultadosJuego) 
                {
                    Console.WriteLine("Resultados del juego #{0}\n", resultadosJuego.IndexOf(item)+1);

                    Console.Write("Ganador: {0}.\t", item.nombreGanador);
                    Console.Write("Puntuación: {0} barcos derribados.", item.puntuacionGanador);
                    Console.WriteLine("");

                    Console.Write("Perdedor: {0}.\t", item.nombrePerdedor);
                    Console.Write("Puntuación: {0} barcos derribados.", item.puntuacionPerdedor);

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

        //Para ir agregando daño a los barcos
        public static void derriboDeBarcos(ref Jugadores JugadorEnEspera, int i, int j, string posicionAtacada) 
        {
            for (int k = 0; k < JugadorEnEspera.barco2.Length; k++) 
            {
                if (JugadorEnEspera.barco2[k] == posicionAtacada) 
                {
                    JugadorEnEspera.barco2[k] = " 0";
                }
            }

            for (int k = 0; k < JugadorEnEspera.barco3.Length; k++)
            {
                if (JugadorEnEspera.barco3[k] == posicionAtacada)
                {
                    JugadorEnEspera.barco3[k] = " 0";
                }
            }

            for (int k = 0; k < JugadorEnEspera.barco3_2.Length; k++)
            {
                if (JugadorEnEspera.barco3_2[k] == posicionAtacada)
                {
                    JugadorEnEspera.barco3_2[k] = " 0";
                }
            }

            for (int k = 0; k < JugadorEnEspera.barco4.Length; k++)
            {
                if (JugadorEnEspera.barco4[k] == posicionAtacada)
                {
                    JugadorEnEspera.barco4[k] = " 0";
                }
            }

            for (int k = 0; k < JugadorEnEspera.barco5.Length; k++)
            {
                if (JugadorEnEspera.barco5[k] == posicionAtacada)
                {
                    JugadorEnEspera.barco5[k] = " 0";
                }
            }
        }

        //Para hacer el calculo final de los barcos hundidos
        public static void calculandoPuntos(Jugadores jugador1, Jugadores jugador2) 
        {
            //Calculando puntos del jugador 2
            if (jugador1.barco2.All(x => x == " 0") == true) 
            {
                jugador2.puntuacionFinal++;
            }

            if (jugador1.barco3.All(x => x == " 0") == true)
            {
                jugador2.puntuacionFinal++;
            }

            if (jugador1.barco3_2.All(x => x == " 0") == true)
            {
                jugador2.puntuacionFinal++;
            }

            if (jugador1.barco4.All(x => x == " 0") == true)
            {
                jugador2.puntuacionFinal++;
            }

            if (jugador1.barco5.All(x => x == " 0") == true)
            {
                jugador2.puntuacionFinal++;
            }

            //Calculando puntos del jugador 1
            if (jugador2.barco2.All(x => x == " 0") == true)
            {
                jugador1.puntuacionFinal++;
            }

            if (jugador2.barco3.All(x => x == " 0") == true)
            {
                jugador1.puntuacionFinal++;
            }

            if (jugador2.barco3_2.All(x => x == " 0") == true)
            {
                jugador1.puntuacionFinal++;
            }

            if (jugador2.barco4.All(x => x == " 0") == true)
            {
                jugador1.puntuacionFinal++;
            }

            if (jugador2.barco5.All(x => x == " 0") == true)
            {
                jugador1.puntuacionFinal++;
            }

            Console.WriteLine("Resultado de la contienda: ");

            if (jugador1.puntuacionFinal > jugador2.puntuacionFinal)
            {
                Console.WriteLine("Ganador: {0} con {1} barcos derribados", jugador1.nombre, jugador1.puntuacionFinal);
                Console.WriteLine("Perdedor: {0} con {1} barcos derribados", jugador2.nombre, jugador2.puntuacionFinal);
            }
            else if (jugador2.puntuacionFinal > jugador1.puntuacionFinal) 
            {
                Console.WriteLine("Ganador: {0} con {1} barcos derribados", jugador2.nombre, jugador2.puntuacionFinal);
                Console.WriteLine("Perdedor: {0} con {1} barcos derribados", jugador1.nombre, jugador1.puntuacionFinal);
            }

        }
 
    }
}
