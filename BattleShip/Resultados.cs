using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BattleShip
{
    [Serializable]
    class Resultados
    {
        public string nombreGanador { get; set; }
        public string nombrePerdedor { get; set; }
        public int puntuacionGanador { get; set; }
        public int puntuacionPerdedor { get; set; }
    }
}
