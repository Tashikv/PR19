using Kino_Vozhakova.Classes;
using Kino_Vozhakova.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Kino_Vozhakova
{
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public enum Pages
        {
            main,
            kinoteatr,
            afisha
        }
        public ObservableCollection<KinoteatrClass> kinoteatrs = new ObservableCollection<KinoteatrClass>();
        public ObservableCollection<AfishaClass> afishas = new ObservableCollection<AfishaClass>();
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            LoadData();
            OpenPages(Pages.main);
        }
        private void LoadData()
        {
            LoadKinoteatrs();
            LoadAfishas();
        }

        private void LoadKinoteatrs()
        {
            string sql = "SELECT * FROM kinoteatr";
            using (var conn = Connection.OpenConnection())
            {
                using (var reader = Connection.Query(sql, conn))
                {
                    kinoteatrs.Clear();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");
                        int count_zal = reader.GetInt32("count_zal");
                        int count = reader.GetInt32("count");
                        kinoteatrs.Add(new KinoteatrClass(id, name, count_zal, count));
                    }
                }
            }
        }

        private void LoadAfishas()
        {
            string sql = "SELECT a.*, k.name AS kinoteatr_name FROM afisha a JOIN kinoteatr k ON a.id_kinoteatr = k.id";
            using (var conn = Connection.OpenConnection())
            {
                using (var reader = Connection.Query(sql, conn))
                {
                    afishas.Clear();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int id_kinoteatr = reader.GetInt32("id_kinoteatr");
                        string name = reader.GetString("name");
                        DateTime time = reader.GetDateTime("time");
                        int price = reader.GetInt32("price");
                        afishas.Add(new AfishaClass(id, id_kinoteatr, name, time, price));
                    }
                }
            }
        }

        public void OpenPages(Pages page)
        {
            switch (page)
            {
                case Pages.main:
                    frame.Navigate(new MainPage());
                    break;
                case Pages.kinoteatr:
                    frame.Navigate(new KinoteatrPage());
                    break;
                case Pages.afisha:
                    frame.Navigate(new AfishaPage());
                    break;
            }
        }
        
        public void RefreshData()
        {
            LoadData();
        }

        private void Kinoteatr_Click(object sender, RoutedEventArgs e)
        {
            OpenPages(Pages.kinoteatr);
        }

        private void Afisha_Click(object sender, RoutedEventArgs e)
        {
            OpenPages(Pages.afisha);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
