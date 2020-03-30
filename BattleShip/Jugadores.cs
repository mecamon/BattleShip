using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    class Jugadores
    {
        public string nombre { get; set; }
        public string[,] tablero { get; set; }
        public string[,] tableroVacio { get; set; }
        public string[] barco2 { get; set; }
        public string[] barco3 { get; set; }
        public string[] barco3_2 { get; set; }
        public string[] barco4 { get; set; }
        public string[] barco5 { get; set; }
        public int puntuacionFinal { get; set; }
    }
}
