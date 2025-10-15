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
    public partial class KinoteatrPage : Page
    {
        public KinoteatrPage()
        {
            InitializeComponent();
            lstKinoteatrs.ItemsSource = MainWindow.mainWindow.kinoteatrs;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Название кинотеатра:", "Добавить");
            if (string.IsNullOrEmpty(name)) return;

            int count_zal;
            if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Количество залов:", "Добавить"), out count_zal)) return;

            int count;
            if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Количество мест:", "Добавить"), out count)) return;

            string sql = $"INSERT INTO kinoteatr (name, count_zal, count) VALUES ('{name}', {count_zal}, {count})";
            using (var conn = Connection.OpenConnection())
            {
                Connection.ExecuteNonQuery(sql, conn);
            }
            MainWindow.mainWindow.RefreshData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (lstKinoteatrs.SelectedItem is KinoteatrClass selected)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox("Новое название:", "Редактировать", selected.name);
                if (string.IsNullOrEmpty(name)) return;

                int count_zal;
                if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Количество залов:", "Редактировать", selected.count_zal.ToString()), out count_zal)) return;

                int count;
                if (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Количество мест:", "Редактировать", selected.count.ToString()), out count)) return;

                string sql = $"UPDATE kinoteatr SET name='{name}', count_zal={count_zal}, count={count} WHERE id={selected.id}";
                using (var conn = Connection.OpenConnection())
                {
                    Connection.ExecuteNonQuery(sql, conn);
                }
                MainWindow.mainWindow.RefreshData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lstKinoteatrs.SelectedItem is KinoteatrClass selected && MessageBox.Show("Удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string sql = $"DELETE FROM kinoteatr WHERE id={selected.id}";
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
