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
        private Product[] _products;
        private ListBox _lbProducts;
        private List<TextBlock> _tblPages = new List<TextBlock>();
        private int _countProductsInPage = 5;
        private int _pageNumber = 0;
        private StackPanel _spPages = new StackPanel();

        public Grid GridSwitcher
        {
            get
            {
                return _gridSwitcher;
            }
        }

        public Switcher(Product[] products, ListBox lbProducts)
        {
            _products = products;
            _lbProducts = lbProducts;

            CreateGridSwitcher();
            CreatePages();

            ShowProducts();
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

            TextBlock tblNext = new TextBlock();
            tblNext.Text = ">";
            tblNext.FontSize = 30;
            tblNext.MouseLeftButtonDown += TblNext_MouseLeftButtonDown;
            Grid.SetColumn(tblNext, 2);
            _gridSwitcher.Children.Add(tblNext);
        }

        public void CreatePages()
        {
            _spPages.Orientation = Orientation.Horizontal;
            _spPages.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            _spPages.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            if (_products.Length == 0)
            {
                TextBlock tblPage = new TextBlock();
                tblPage.Text = "1";
                tblPage.FontSize = 30;
                _tblPages.Add(tblPage);
            }
            else
            {
                for (int i = 0; i <= _products.Length / _countProductsInPage; i++)
                {
                    TextBlock tblPage = new TextBlock();
                    tblPage.Text = (i + 1).ToString();
                    tblPage.FontSize = 30;
                    tblPage.Margin = new System.Windows.Thickness(5, 0, 5, 0);
                    tblPage.Cursor = Cursors.Hand;
                    tblPage.MouseLeftButtonDown += TblPage_MouseLeftButtonDown;
                    _tblPages.Add(tblPage);
                }
            }

            _tblPages[_pageNumber].Foreground = Brushes.Red;

            Grid.SetColumn(_spPages, 1);
            _gridSwitcher.Children.Add(_spPages);
        }

        public void ShowProducts()
        {
            List<Product> products = new List<Product>();

            for (int i = _pageNumber * _countProductsInPage;
                i < _pageNumber * _countProductsInPage + _countProductsInPage; i++)
            {
                if (i == _products.Length) break;
                products.Add(_products[i]);
            }

            _lbProducts.ItemsSource = products;
        }

        public void ShowPages()
        {
            _spPages.Children.Clear();

            int countPagesInSwitcher = 4;

            if (_pageNumber < countPagesInSwitcher)
            {
                for (int i = 0; i < countPagesInSwitcher; i++)
                {
                    if (i == _tblPages.Count) break;
                    _spPages.Children.Add(_tblPages[i]);
                }
            }
            else if (_pageNumber >= countPagesInSwitcher && _pageNumber < _tblPages.Count - countPagesInSwitcher)
            {
                for (int i = _pageNumber; i < _pageNumber + countPagesInSwitcher; i++)
                {
                    if (i == _tblPages.Count) break;
                    _spPages.Children.Add(_tblPages[i]);
                }
            }
            else if (_pageNumber >= _tblPages.Count - countPagesInSwitcher)
            {
                for (int i = _tblPages.Count - countPagesInSwitcher; i < _tblPages.Count; i++)
                {
                    if (i == _tblPages.Count) break;
                    _spPages.Children.Add(_tblPages[i]);
                }
            }
        }

        private void TblNext_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_pageNumber < _tblPages.Count - 1)
            {
                _tblPages[_pageNumber].Foreground = Brushes.Black;
                _pageNumber++;
                _tblPages[_pageNumber].Foreground = Brushes.Red;
            }

            ShowProducts();
            ShowPages();

        }

        private void TblBack_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_pageNumber > 0)
            {
                _tblPages[_pageNumber].Foreground = Brushes.Black;
                _pageNumber--;
                _tblPages[_pageNumber].Foreground = Brushes.Red;
            }

            ShowProducts();
            ShowPages();
        }

        private void TblPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tblPage = (TextBlock)sender;
            for (int i = 0; i < _tblPages.Count; i++)
            {
                if (_tblPages[i] == tblPage)
                {
                    _tblPages[_pageNumber].Foreground = Brushes.Black;
                    _pageNumber = i;
                    _tblPages[_pageNumber].Foreground = Brushes.Red;

                    ShowProducts();
                    ShowPages();
                    break;
                }
            }
        }
    }
}
