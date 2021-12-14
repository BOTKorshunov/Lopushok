using Lopushok.DB;
using Lopushok.DB.Model;
using Lopushok.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для ProductAddPage.xaml
    /// </summary>
    public partial class ProductAddPage : Page
    {
        private Product _product;
        public ProductAddPage(Product product)
        {
            InitializeComponent();

            _product = product;
            cbProductType.ItemsSource = DbConnect.db.ProductType.ToList();

            DataContext = _product;

            if (_product.ID == 0)
            {
                btnDelete.Visibility = Visibility.Hidden;
                btnAdd.Content = "Добавить";
            }
            else
            {
                btnDelete.Visibility = Visibility.Visible;
                btnAdd.Content = "Сохранить";
            }
        }

        /// <summary>
        /// Событие добавления продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string text = null;

            if (String.IsNullOrEmpty(tbTitle.Text)
                || String.IsNullOrWhiteSpace(tbTitle.Text))
                text += "Название не может быть пустым\n";

            if (String.IsNullOrEmpty(tbArticle.Text)
                || String.IsNullOrWhiteSpace(tbArticle.Text))
                text += "Артикул не может быть пустым\n";
            else if (DbConnect.db.Product.FirstOrDefault(product => product.ArticleNumber == tbArticle.Text) != null
                && _product.ID == 0)
                text += "Такой артикул уже существует\n";

            if (!int.TryParse(tbCountAgents.Text, out _))
                text += "Количество агентов должно быть числом\n";
            else if (int.Parse(tbCountAgents.Text) < 0)
                text += "Количество агентов должно быть положительным\n";

            if (!int.TryParse(tbWorkshopNumber.Text, out _))
                text += "Номер цеха должен быть числом\n";
            else if (int.Parse(tbWorkshopNumber.Text) < 0)
                text += "Номер цеха должен быть положительным\n";

            if (!decimal.TryParse(tbMinCostForAgent.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                text += "Некорректное значение стоимости одного агента\n";
            else if (decimal.Parse(tbMinCostForAgent.Text, CultureInfo.InvariantCulture) < 0)
                text += "Стоимость должна быть положительной\n";

            if (text == null)
            {
                if (_product.ID == 0) DbConnect.db.Product.Add(_product);
                DbConnect.db.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show(text);
            }
        }

        /// <summary>
        /// Событие удаления продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот продукт?",
                "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DbConnect.db.Product.Remove(_product);
                DbConnect.db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Событие выбора картинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            SelectImageWindow selectImageWindow = new SelectImageWindow();
            selectImageWindow.ShowDialog();

            if ((bool)selectImageWindow.DialogResult)
            {
                _product.Image = selectImageWindow.ImgUri;
                DataContext = null;
                DataContext = _product;
            }
        }
    }
}