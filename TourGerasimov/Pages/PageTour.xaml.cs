using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
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
    /// Логика взаимодействия для PageTour.xaml
    /// </summary>
    public partial class PageTour : Page
    {
        public PageTour()
        {
            InitializeComponent();
            lvTour.ItemsSource = Tour.getTours();

            cbTypeOfTour.Items.Add("Все");
            foreach (Type type in DataBase.tbe.Type.ToList())
            {
                cbTypeOfTour.Items.Add(type.Name);
            }
            cbTypeOfTour.SelectedIndex = 0;
        }

        private void btnGoHotel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageHotels());
        }

        private void cbTypeOfTour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtres();
        }


        public double getSumma(List<Tour> tour)
        {
            double sum = 0;
            foreach (Tour t in tour)
            {
                sum = sum + (double)t.Price * (double)t.TicketCount;
            }
            return sum;
        }
        void Filtres()
        {

            try
            {
                List<Tour> tours = DataBase.tbe.Tour.ToList();
                if(cbTypeOfTour != null)
                if (cbTypeOfTour.SelectedIndex != 0)
                {
                    string name = cbTypeOfTour.SelectedValue.ToString();                 
                    Type type = DataBase.tbe.Type.FirstOrDefault(x => x.Name == name);
                    tours = tours.Where(x => x.Type.Any(y => y.Id == type.Id)).ToList();

                }
                if (cbFiltres != null)
                    if (cbFiltres.SelectedIndex != 0)
                    {

                        ComboBoxItem comboBoxItem = (ComboBoxItem)cbFiltres.SelectedItem;
                        if (comboBoxItem != null)
                            switch (comboBoxItem.Content)
                            {

                                case "По умолчанию":
                                    {

                                        tours = tours;
                                        break;
                                    }

                                case "По возрастанию":
                                    {

                                        tours = tours.OrderBy(x => x.Price).ToList();
                                        break;
                                    }
                                case "По убыванию":
                                    {

                                        tours = tours.OrderByDescending(x => x.Price).ToList();
                                        break;
                                    }



                            }
                    }
                if (cbActual.IsChecked == true)
                {
                    tours = tours.Where(x=>x.IsActual == true).ToList();
                }
                if (tbFieldSearch.Text != "")
                {
                    tours = tours.Where(x => x.Name.ToLower().Contains(tbFieldSearch.Text.ToLower())).ToList();
                }
                lvTour.ItemsSource = tours;


         
                Summa.Text = getSumma(tours).ToString("F3") + " РУБ";
            
            }
            catch
            {
                MessageBox.Show("Что-то не так");

            }



        }
      
        private void cbFiltres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtres();

        }

        private void cbActual_Checked(object sender, RoutedEventArgs e)
        {
            Filtres();
        }

        private void cbActual_Unchecked(object sender, RoutedEventArgs e)
        {
            Filtres();
        }

        private void tbFieldSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filtres();
        }
    }
}
