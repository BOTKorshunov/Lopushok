using Lopushok.DB;
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
using System.Windows.Shapes;

namespace Lopushok.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeCostWindow.xaml
    /// </summary>
    public partial class ChangeCostWindow : Window
    {
        private Product[] _products;

        public ChangeCostWindow(Product[] products)
        {
            InitializeComponent();

            _products = products;

            decimal avgCost = _products.Average(product => product.MinCostForAgent);
            avgCost = Math.Round(avgCost, 2);
            tbCost.Text = avgCost.ToString();

            tbCost.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal cost = 0;
            if (decimal.TryParse(tbCost.Text, out cost))
            {
                foreach(var product in _products)
                {
                    product.MinCostForAgent += cost;
                }

                DialogResult = true;
                Close();
            }
            else
                MessageBox.Show("Некорректые данные");
        }
    }
}
