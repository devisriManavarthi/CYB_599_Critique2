using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntMe.Player.FirstAntColony
{
    public class TicketManager
    {
        #region Singleton
        private static TicketManager _instance;

        public static TicketManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TicketManager();
                return _instance;
            }
        }
        #endregion

        private List<English.Sugar> sweet = new List<English.Sugar>();
        private List<FirstAntColonyClass> myant = new List<FirstAntColonyClass>();
        internal void ReportSugar(English.Sugar sugar)
        {
            //throw new NotImplementedException();
            if (!sweet.Contains(sugar))
            {
                sweet.Add(sugar);

            }
        }

        //internal void RegisterAnt(FirstAntColonyClass myant)
        //{
        //    if (!myant.Contains(myant))
        //    {
        //        myant.Add(myant);
        //    }
        //}
    }
}
