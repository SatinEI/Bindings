using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Product
        {
            public string Name { get; set; } // Название товара
            public string Description { get; set; } // Описание товара
            public string Manufacturer { get; set; } // Имя производителя
            public decimal Price { get; set; } // Цена
            public int Stock { get; set; } // Остаток на складе
            public string ImagePath { get; set; } // изображениe
        }
        private List<Product> _allProducts; // Исходные данные
        private ObservableCollection<Product> _filteredProducts; // Отфильтрованные данные

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация данных
            _allProducts = new List<Product>
        {
            new Product { Name = "Товар 1", Description = "Описание 1", Manufacturer = "Производитель A", Price = 100, Stock = 10, ImagePath = " " },
            new Product { Name = "Товар 2", Description = "Описание 2", Manufacturer = "Производитель B", Price = 200, Stock = 5, ImagePath = " " },
            new Product { Name = "Товар 3", Description = "Описание 3", Manufacturer = "Производитель A", Price = 150, Stock = 8, ImagePath = " " },
        };

            _filteredProducts = new ObservableCollection<Product>(_allProducts);
            ProductList.ItemsSource = _filteredProducts;

            // Заполнение фильтра производителей
            var manufacturers = _allProducts.Select(p => p.Manufacturer).Distinct().ToList();
            manufacturers.Insert(0, "Все");
            ManufacturerFilter.ItemsSource = manufacturers;
            ManufacturerFilter.SelectedIndex = 0;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ManufacturerFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            _filteredProducts = new ObservableCollection<Product>(_filteredProducts.OrderBy(p => p.Price));
            ProductList.ItemsSource = _filteredProducts;
        }

        private void SortDescending_Click(object sender, RoutedEventArgs e)
        {
            _filteredProducts = new ObservableCollection<Product>(_filteredProducts.OrderByDescending(p => p.Price));
            ProductList.ItemsSource = _filteredProducts;
        }

        private void ApplyFilters()
        {
            var searchText = SearchBox.Text.ToLower();
            var selectedManufacturer = ManufacturerFilter.SelectedItem as string;

            var filtered = _allProducts.Where(p =>
                (string.IsNullOrWhiteSpace(searchText) || p.Name.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText)) &&
                (selectedManufacturer == "Все" || p.Manufacturer == selectedManufacturer)).ToList();

            _filteredProducts.Clear();
            foreach (var product in filtered)
                _filteredProducts.Add(product);
        }
    }




}
