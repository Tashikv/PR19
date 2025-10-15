using Kino_Vozhakova.Classes;
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

namespace Kino_Vozhakova.Pages
{
    public partial class AfishaPage : Page
    {
        public AfishaPage()
        {
            InitializeComponent();
            lstAfishas.ItemsSource = MainWindow.mainWindow.afishas;
            cmbKinoteatr.ItemsSource = MainWindow.mainWindow.kinoteatrs;
            cmbKinoteatr.DisplayMemberPath = "name";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKinoteatr.SelectedItem is KinoteatrClass kt)
            {
                string filmName = Microsoft.VisualBasic.Interaction.InputBox("Название фильма:", "Добавить");
                if (string.IsNullOrEmpty(filmName)) return;

                DateTime sessionTime;
                if (!DateTime.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Время сеанса (yyyy-MM-dd):", "Добавить"), out sessionTime)) return;

                int price;
                if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Стоимость:", "Добавить"), out price)) return;

                string sql = $"INSERT INTO afisha (id_kinoteatr, name, time, price) VALUES ({kt.id}, '{filmName}', '{sessionTime:yyyy-MM-dd}', {price})";
                using (var conn = Connection.OpenConnection())
                {
                    Connection.ExecuteNonQuery(sql, conn);
                }
                MainWindow.mainWindow.RefreshData();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lstAfishas.SelectedItem is AfishaClass selected)
            {
                if (cmbKinoteatr.SelectedItem is KinoteatrClass kt)
                {
                    string filmName = Microsoft.VisualBasic.Interaction.InputBox("Название фильма:", "Редактировать", selected.name);
                    if (string.IsNullOrEmpty(filmName)) return;

                    DateTime sessionTime;
                    if (!DateTime.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Время сеанса (yyyy-MM-dd):", "Редактировать", selected.time.ToString("yyyy-MM-dd")), out sessionTime)) return;

                    int price;
                    if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Стоимость:", "Редактировать", selected.price.ToString()), out price)) return;

                    string sql = $"UPDATE afisha SET id_kinoteatr={kt.id}, name='{filmName}', time='{sessionTime:yyyy-MM-dd}', price={price} WHERE id={selected.id}";
                    using (var conn = Connection.OpenConnection())
                    {
                        Connection.ExecuteNonQuery(sql, conn);
                    }
                    MainWindow.mainWindow.RefreshData();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lstAfishas.SelectedItem is AfishaClass selected && MessageBox.Show("Удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string sql = $"DELETE FROM afisha WHERE id={selected.id}";
                using (var conn = Connection.OpenConnection())
                {
                    Connection.ExecuteNonQuery(sql, conn);
                }
                MainWindow.mainWindow.RefreshData();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPages(MainWindow.Pages.main);
        }
    }
}
