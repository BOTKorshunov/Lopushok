using System;
using System.Collections.Generic;
using System.IO;
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
    // сниппет 

    /// <summary>
    /// Логика взаимодействия для SelectImageWindow.xaml
    /// </summary>
    public partial class SelectImageWindow : Window
    {
        private string _imgUri = null;

        public string ImgUri
        {
            get
            {
                return _imgUri;
            }
        }

        public SelectImageWindow()
        {
            InitializeComponent();

            ShowImages();
        }

        /// <summary>
        /// Отображает картинки
        /// </summary>
        private void ShowImages()
        {
            const int countImageInRow = 5;

            string[] files = GetImages();

            StackPanel spImages = new StackPanel();
            spImages.Orientation = Orientation.Vertical;
            spImages.HorizontalAlignment = HorizontalAlignment.Center;
            spImages.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i <= files.Length / countImageInRow; i++)
            {
                StackPanel spRow = new StackPanel();
                spRow.Orientation = Orientation.Horizontal;
                spRow.Margin = new Thickness(0, 5, 0, 5);

                for (int j = countImageInRow * i; j < countImageInRow * i + countImageInRow; j++)
                {
                    if (j == files.Length) break;

                    Image image = new Image();
                    image.Width = 64;
                    image.Height = 64;
                    image.Margin = new Thickness(5, 0, 5, 0);
                    image.Source = new BitmapImage(new Uri(files[j], UriKind.Relative));
                    image.Cursor = Cursors.Hand;
                    image.Tag = files[j];
                    image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    spRow.Children.Add(image);
                }

                spImages.Children.Add(spRow);
            }

            svImages.Content = spImages;
        }

        /// <summary>
        /// Событие нажатия на картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            _imgUri = image.Tag.ToString();
            DialogResult = true;
            Close();
        }

        /// <summary>
        /// Достаёт картинки из папки
        /// </summary>
        /// <returns></returns>
        private string[] GetImages()
        {
            string[] files = Directory.GetFiles("../../products");

            for (int i = 0; i < files.Length; i++)
                files[i] = files[i].Remove(0, 5);

            return files;
        }
    }
}
