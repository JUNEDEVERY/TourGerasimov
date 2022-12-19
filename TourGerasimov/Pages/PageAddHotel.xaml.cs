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
    /// Логика взаимодействия для PageAddHotel.xaml
    /// </summary>
    public partial class PageAddHotel : Page
    {
        Hotel hotel;
        bool update = false;
        public PageAddHotel()
        {
            InitializeComponent();
            cbCountry.ItemsSource = DataBase.tbe.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;
        }
        public PageAddHotel(Hotel hotel)
        {
            InitializeComponent();
            this.hotel = hotel;
            update = true;
            cbCountry.ItemsSource = DataBase.tbe.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";
            cbCountry.SelectedIndex = 0;
            btnAddNewHotel.Content = "Изменить данные отеля";
            HeaderTB.Text = "Изменение данных о существующем отеле";
            tbName.Text = hotel.Name;
            tbCountStar.Text = Convert.ToString(hotel.CountOfStars);
            cbCountry.SelectedValue = hotel.CountryCode;
            tbDescr.Text = hotel.Description;



        }
        private void btnAddNewHotel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(tbName.Text))
                {
                    MessageBox.Show("Поле наименование незаполнено!", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
                }
                if (String.IsNullOrEmpty(tbCountStar.Text))
                {
                    MessageBox.Show("Поле с количеством звезд незаполнено!", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
                }
                if (cbCountry.SelectedItem == null)
                {
                    MessageBox.Show("Поле страна отеля не может оставаться пустым", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
                }
                if (String.IsNullOrEmpty(tbDescr.Text))
                {
                    MessageBox.Show("Поле описание должно быть незаполнено!", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
                }
                if (update == false) { hotel = new Hotel(); }
                hotel.Name = Convert.ToString(tbName.Text);
                hotel.CountOfStars = Convert.ToInt32(tbCountStar.Text);
                hotel.CountryCode = Convert.ToString(cbCountry.SelectedValue);
                hotel.Description = Convert.ToString(tbDescr.Text);
                if (update == false)
                {
                    DataBase.tbe.Hotel.Add(hotel);
                }
                DataBase.tbe.SaveChanges();
                if (update == true)
                {
                    MessageBox.Show("Запись изменена!");
                }
                else
                {
                    MessageBox.Show("Запись добавлена!");
                }
                NavigationService.Navigate(new PageHotels());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageHotels());
        }
    }
}
