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
            lvTour.ItemsSource = DataBase.tbe.Tour.ToList();

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



        void Filtres()
        {

            try
            {
               
                //if (cbTypeOfTour != null)
                string name = cbTypeOfTour.SelectedValue.ToString();
                int id = cbTypeOfTour.SelectedIndex;
                Type type = DataBase.tbe.Type.FirstOrDefault(x => x.Name == name);
                
                if (id != 0)
                {
                    id = DataBase.tbe.Type.FirstOrDefault(t => t.Name == name).Id;

                }
                //List<Tour> tours = DataBase.tbe.Tour.Include(x=>x.)



                //}
                    
                
                List<Tour> tourses = DataBase.tbe.Tour.ToList();
                if(cbFiltres !=null)
                    if (cbFiltres.SelectedIndex != 0)
                    {
                           
                        ComboBoxItem comboBoxItem = (ComboBoxItem)cbFiltres.SelectedItem;
                        switch (comboBoxItem.Content)
                        {

                        case "По уиолчанию":
                            {

                                tourses = tourses;
                                break;
                            }

                        case "По возрастанию":
                                {

                                tourses = tourses.OrderBy(x => x.Price).ToList();
                                    break;
                                }
                            case "По убыванию":
                                {

                                tourses = tourses.OrderByDescending(x => x.Price).ToList();
                                    break;
                                }
                   


                        }
                    }
                if (cbActual.IsChecked == true)
                {
                    tourses = tourses.Where(x => x.IsActual == true).ToList();
                }


                lvTour.ItemsSource = tourses;


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
    }
}
