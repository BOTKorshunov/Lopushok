using Lopushok.Classes;
using Lopushok.DB;
using Lopushok.DB.Model;
using Lopushok.Windows;
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

namespace Lopushok.Pages.Products
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Switcher switcher;
        public ProductPage()
        {
            InitializeComponent();

            CreateCbSort();
            CreateCbFilter();
            FindProducts();
        }

        private void FindProducts()
        {
            List<Product> products = DbConnect.db.Product.ToList();

            if (!String.IsNullOrEmpty(tbFind.Text) || !String.IsNullOrWhiteSpace(tbFind.Text))
                products = products.Where(product =>
                    product.Title.ToLower().Contains(tbFind.Text.ToLower()) ||
                    product.Description.ToLower().Contains(tbFind.Text.ToLower())).ToList();

            ShowOrHideSort();

            if (rbAsc.IsChecked == true)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1: products = products.OrderBy(product => product.Title).ToList(); break;
                    case 2: products = products.OrderBy(product => product.ProductionWorkshopNumber).ToList(); break;
                    case 3: products = products.OrderBy(product => product.MinCostForAgent).ToList(); break;
                }
            }
            else if (rbDesc.IsChecked == true)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1: products = products.OrderByDescending(product => product.Title).ToList(); break;
                    case 2: products = products.OrderByDescending(product => product.ProductionWorkshopNumber).ToList(); break;
                    case 3: products = products.OrderByDescending(product => product.MinCostForAgent).ToList(); break;
                }
            }

            if (cbFilter.SelectedIndex > 0)
                products = products.Where(product => product.ProductType.Title == cbFilter.SelectedItem.ToString()).ToList();

            switcher = new Switcher(products.ToArray(), lbProducts);
            spSwitcher.Children.Clear();
            spSwitcher.Children.Add(switcher.GridSwitcher);
        }

        /// <summary>
        /// Показывает или прячет сортировку "По возрастанию" и "По убыванию"
        /// </summary>
        private void ShowOrHideSort()
        {
            spSort.Visibility = cbSort.SelectedIndex > 0 ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Создаёт ComboBox сортировку
        /// </summary>
        private void CreateCbSort()
        {
            cbSort.Items.Add("< Нет >");
            cbSort.Items.Add("Наименование");
            cbSort.Items.Add("Номер производственного цеха");
            cbSort.Items.Add("Минимальная стоимость для агента");

            cbSort.SelectedIndex = 0;
        }

        /// <summary>
        /// Создаёт ComboBox фильтр
        /// </summary>
        private void CreateCbFilter()
        {
            cbFilter.Items.Add("< Все типы >");
            
            foreach (var productType in DbConnect.db.ProductType)
            {
                cbFilter.Items.Add(productType.Title);
            }

            cbFilter.SelectedIndex = 0;
        }

        private void tbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindProducts();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProducts();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProducts();
        }

        private void rbAsc_Checked(object sender, RoutedEventArgs e)
        {
            FindProducts();
        }

        private void rbDesc_Checked(object sender, RoutedEventArgs e)
        {
            FindProducts();
        }

        private void lbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnChangeCost.Visibility =
                lbProducts.SelectedItems.Count > 1 ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void btnChangeCost_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            foreach (var selectedItem in lbProducts.SelectedItems)
            {
                products.Add(selectedItem as Product);
            }

            ChangeCostWindow changeCostWindow = new ChangeCostWindow(products.ToArray());
            changeCostWindow.ShowDialog();

            if (changeCostWindow.DialogResult == true)
            {
                DbConnect.db.SaveChanges();
                FindProducts();
            }
                
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductAddPage(new Product()));
        }

        private void lbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbProducts.SelectedItem != null)
                NavigationService.Navigate(new ProductAddPage(lbProducts.SelectedItem as Product));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            FindProducts();
        }
    }
}
