using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourGerasimov.Classes;

namespace TourGerasimov.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageHotels.xaml
    /// </summary>
    public partial class PageHotels : Page
    {
        List<Hotel> hotels = new List<Hotel>();
        PageChange pc = new PageChange();
        bool First = false;

      

        public PageHotels()
        {
            InitializeComponent();
            dg.ItemsSource = DataBase.tbe.Hotel.ToList();
            tbCountRecords.Text = Convert.ToString(DataBase.tbe.Hotel.ToList().Count);
            hotels = DataBase.tbe.Hotel.ToList();
            pc.CountPage = DataBase.tbe.Hotel.ToList().Count;
            tbCountPages.Text = pc.CountPages.ToString();
            tbCurrentPage.Text = pc.CurrentPage.ToString();
            DataContext = pc;
            tbCountPage.Text = "10";
      
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int i = Convert.ToInt32(button.Uid);
            Hotel hotel = DataBase.tbe.Hotel.FirstOrDefault(x => x.Id == i);
            NavigationService.Navigate(new PageAddHotel(hotel));
        }
        public PageHotels(string k)
        {
            InitializeComponent();
            dg.ItemsSource = DataBase.tbe.Hotel.ToList();
            tbCountRecords.Text = Convert.ToString(DataBase.tbe.Hotel.ToList().Count);
            hotels = DataBase.tbe.Hotel.ToList();
            pc.CountPage = DataBase.tbe.Hotel.ToList().Count;
            tbCountPages.Text = pc.CountPages.ToString();
            tbCurrentPage.Text = pc.CurrentPage.ToString();
            DataContext = pc;
            tbCountPage.Text = k;
     
            
        }
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTour());
        }

        private void tbCountPages_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(tbCountPage.Text);
            }
            catch
            {
                pc.CountPage = hotels.Count;
            }
            pc.Countlist = hotels.Count;
            dg.ItemsSource = hotels.Skip(0).Take(pc.CountPage).ToList();
            if (First == true) { pc.CurrentPage = 1; }
            else { First = true; }
            tbCountPages.Text = pc.CountPages.ToString();
            tbCurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void txt1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            dg.ItemsSource = hotels.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbCurrentPage.Text = pc.CurrentPage.ToString();


        }

        private void txtNextFirst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = 1;
            dg.ItemsSource = hotels.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbCurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void txtNextLast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = pc.CountPages;
            dg.ItemsSource = hotels.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            tbCurrentPage.Text = pc.CurrentPage.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            int countDeleteHotels = 0;
            if (dg.SelectedItems.Count != 0)
            {
                foreach(Hotel hotel in dg.SelectedItems)
                {
                    List<HotelOfTour> hotelOfTour = DataBase.tbe.HotelOfTour.Where(x => x.HotelId == hotel.Id).ToList();

                    foreach(HotelOfTour hotelOfTour1 in hotelOfTour)
                    {
                        if (hotelOfTour1.Tour.IsActual == true)
                        {
                            MessageBox.Show("Отель" + hotel.Name + "не может быть удален из этого списка, т.к он относится к числу актуальных туров"); return;
                        }
                       
                    }
                    if (MessageBox.Show("Вы уверены что хотите удалить отель:" + hotel.Name + "?", "Администратор", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        DataBase.tbe.Hotel.Remove(hotel);
                        MessageBox.Show("Отель " + hotel.Name + " был удалён");
                        countDeleteHotels++;
                    }
                    else
                    {
                        MessageBox.Show("Это ваш выбор..");
                    }
                }
                if (countDeleteHotels != 0)
                {
                    DataBase.tbe.SaveChanges();
                    NavigationService.Navigate(new PageHotels(tbCountPage.Text));
               
                }


            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления!!!");
            }
        }
        

        private void btnGoAddHotel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddHotel());
        }
    }
}
