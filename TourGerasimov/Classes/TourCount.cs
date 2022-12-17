using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourGerasimov.Classes;
namespace TourGerasimov
{
    public partial class Hotel
    {

        public int CountTour
        {
            get
            {
                List<HotelOfTour> list = DataBase.tbe.HotelOfTour.Where(x => x.HotelId == Id ).ToList();
                return list.Count;
            }
        }



    }
}
