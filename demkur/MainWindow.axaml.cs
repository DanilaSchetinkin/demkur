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


        }

        public MainWindow()
        {


            InitializeComponent();
            using var ctx = new User20Context();

            var tovar = ctx.Products
                .Select(i => new Tovar
                {
                    ImagePath = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "/" + i.ImagePath),
                    ProductName = i.ProductName,
                    Description = i.Description,
                    Manufacturer = i.Manufacturer.ManufacturerName,
                    Price = i.Price,
                    Discount = i.CurrentDiscount
                                    })
                                    .ToList();
        
            /*tovar = tovar.Where(x => x.CurrentDiscount > 0 && x.CurrentDiscount < 9.99m).ToList();
*/              

            TovarBox.ItemsSource = new ObservableCollection<Tovar>(tovar);
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
}