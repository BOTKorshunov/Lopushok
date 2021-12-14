using Lopushok.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lopushok.Classes
{
    public class Switcher
    {
        private Grid _gridSwitcher = new Grid();
        private StackPanel _spPages = new StackPanel();
        private Dictionary<int, Product[]> _productPages = new Dictionary<int, Product[]>();
        private ListBox _lbProducts;

        private int _countProductsInPage = 5;
        private int _pageNumber = 0;

        public Grid GridSwitcher
        {
            get
            {
                return _gridSwitcher;
            }
        }

        public Switcher(Product[] products, ListBox lbProducts)
        {
            _lbProducts = lbProducts;

            CreateGridSwitcher();
            CreateProductPages(products);

            ShowPages();
        }

        public void CreateGridSwitcher()
        {
            _gridSwitcher.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new System.Windows.GridLength(150);
            _gridSwitcher.ColumnDefinitions.Add(column);
            _gridSwitcher.ColumnDefinitions.Add(new ColumnDefinition());

            TextBlock tblBack = new TextBlock();
            tblBack.Text = "<";
            tblBack.FontSize = 30;
            tblBack.MouseLeftButtonDown += TblBack_MouseLeftButtonDown;
            Grid.SetColumn(tblBack, 0);
            _gridSwitcher.Children.Add(tblBack);

            _spPages.Orientation = Orientation.Horizontal;
            _spPages.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            _spPages.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            Grid.SetColumn(_spPages, 1);
            _gridSwitcher.Children.Add(_spPages);

            TextBlock tblNext = new TextBlock();
            tblNext.Text = ">";
            tblNext.FontSize = 30;
            tblNext.MouseLeftButtonDown += TblNext_MouseLeftButtonDown;
            Grid.SetColumn(tblNext, 2);
            _gridSwitcher.Children.Add(tblNext);
        }

        public void CreateProductPages(Product[] products)
        {
            for (int i = 0; i <= products.Length / _countProductsInPage; i++)
            {
                List<Product> selectProducts = new List<Product>();
                for (int j = i * _countProductsInPage; j < i * _countProductsInPage + _countProductsInPage; j++)
                {
                    if (j == products.Length) break;
                    selectProducts.Add(products[j]);
                }

                _productPages.Add(i, selectProducts.ToArray());
            }
        }

        public TextBlock CreateTblPage(int num)
        {
            TextBlock tblPage = new TextBlock();
            tblPage.Text = (num + 1).ToString();
            tblPage.FontSize = 30;
            tblPage.Margin = new System.Windows.Thickness(5, 0, 5, 0);
            tblPage.Cursor = Cursors.Hand;
            tblPage.MouseLeftButtonDown += TblPage_MouseLeftButtonDown;
            if (num == _pageNumber) tblPage.Foreground = Brushes.Red;

            return tblPage;
        }

        public void ShowPages()
        {
            _spPages.Children.Clear();

            const int countPagesInSwitcher = 4;

            if (_pageNumber < countPagesInSwitcher)
            {
                for (int i = 0; i < countPagesInSwitcher; i++)
                {
                    if (i == _productPages.Count) break;
                    _spPages.Children.Add(CreateTblPage(i));
                }
            }
            else if (_pageNumber >= countPagesInSwitcher && _pageNumber < _productPages.Count - countPagesInSwitcher)
            {
                for (int i = _pageNumber; i < _pageNumber + countPagesInSwitcher; i++)
                {
                    if (i == _productPages.Count) break;
                    _spPages.Children.Add(CreateTblPage(i));
                }
            }
            else if (_pageNumber >= _productPages.Count - countPagesInSwitcher)
            {
                for (int i = _productPages.Count - countPagesInSwitcher; i < _productPages.Count; i++)
                {
                    if (i == _productPages.Count) break;
                    _spPages.Children.Add(CreateTblPage(i));
                }
            }

            _lbProducts.ItemsSource = _productPages[_pageNumber];
        }

        private void TblNext_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_pageNumber < _productPages.Count - 1)
                _pageNumber++;

            ShowPages();
        }

        private void TblBack_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_pageNumber > 0)
                _pageNumber--;

            ShowPages();
        }

        private void TblPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tblPage = (TextBlock)sender;
            _pageNumber = int.Parse(tblPage.Text) - 1;

            ShowPages();
        }
    }
}
