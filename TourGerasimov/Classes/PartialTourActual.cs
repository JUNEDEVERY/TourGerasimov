using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TourGerasimov.Classes;

namespace TourGerasimov
{
    public partial class Tour
    {
  
        public string Actuality
        {
            get; set;
            
        
        }
        public string Color
        {
            get;
            set;    
        }
        public double Summ
        {
            get;
            set;
        }
        public static List<Tour> getTours()
        {
            List<Tour> list = DataBase.tbe.Tour.ToList();

            foreach(Tour t in list)
            {
              
                if (t.IsActual == true)
                {
                    t.Actuality = "Актуален";
                    t.Color = "Green";
                }
                else
                {
                    t.Actuality = "Неактулен";
                    t.Color = "Red";
                }
                
            }

            return list;

        }
       
    }
}
