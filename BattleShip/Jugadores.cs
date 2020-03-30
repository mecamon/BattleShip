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
        public int puntuacionFinal { get; set; }
    }
}
