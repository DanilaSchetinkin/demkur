using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using demkur.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace demkur
{
    public partial class MainWindow : Window
    {
        public class Tovar
        {
            public Bitmap? ImagePath { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public string Manufacturer { get; set; }
            public decimal Price { get; set; }
            public decimal Discount { get; set; }
            public decimal FinalPrice => Price * (1 - Discount / 100);
        }

        private ObservableCollection<Tovar> _allTovar;
        private int _totalItems;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();

            // Set up combo boxes
            discountComboBox.SelectionChanged += FilterComboBox_SelectionChanged;
            priceComboBox.SelectionChanged += SortComboBox_SelectionChanged;

            // Initialize combo boxes
            discountComboBox.SelectedIndex = 0;
            priceComboBox.SelectedIndex = 0;

            UpdateItemCount();
        }

        private void LoadData()
        {
            using var ctx = new User20Context();

            _allTovar = new ObservableCollection<Tovar>(ctx.Products
                .Select(i => new Tovar
                {
                    ImagePath = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/" + i.ImagePath),
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Manufacturer = i.Manufacturer.ManufacturerName,
                    Price = i.Price,
                    Discount = i.CurrentDiscount
                })
                .ToList());

            _totalItems = _allTovar.Count;
            TovarBox.ItemsSource = _allTovar;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filtered = _allTovar.ToList();

            switch (discountComboBox.SelectedIndex)
            {
                case 1: 
                    filtered = filtered.Where(x => x.Discount > 0 && x.Discount < 10).ToList();
                    break;
                case 2: 
                    filtered = filtered.Where(x => x.Discount >= 10 && x.Discount < 15).ToList();
                    break;
                case 3: 
                    filtered = filtered.Where(x => x.Discount >= 15).ToList();
                    break;
                default: 
                    break;
            }

            TovarBox.ItemsSource = new ObservableCollection<Tovar>(filtered);
            UpdateItemCount(filtered.Count);

            
            SortComboBox_SelectionChanged(null, null);
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentItems = TovarBox.ItemsSource as ObservableCollection<Tovar> ?? _allTovar;
            var sorted = currentItems.ToList();

            switch (priceComboBox.SelectedIndex)
            {
                case 1: 
                    sorted = sorted.OrderBy(x => x.Price).ToList();
                    break;
                case 2: 
                    sorted = sorted.OrderByDescending(x => x.Price).ToList();
                    break;
                default: 
                    break;
            }

            TovarBox.ItemsSource = new ObservableCollection<Tovar>(sorted);
            UpdateItemCount(currentItems.Count);
        }

        private void UpdateItemCount(int? filteredCount = null)
        {
            itemCountText.Text = $"{filteredCount ?? _allTovar.Count} из {_totalItems}";
        }
    }

    public class DiscountToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal discount && discount > 15)
                return new SolidColorBrush(Color.Parse("#7fff00"));
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class PriceTextDecorationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal discount && discount > 0)
                return TextDecorations.Strikethrough;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DiscountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal discount && discount > 0)
                return true; // Visible
            return false; // Hidden
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


}