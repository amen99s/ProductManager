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

namespace AminRasoulFinal
{
    /// <summary>
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>
    public partial class AddNewProduct : Window
    {
        public AddNewProduct()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            using (var context = new MYLocalDBEntities())
            {

                var categories = context.Categories.ToList();


                combobox_category.ItemsSource = categories;
                combobox_category.DisplayMemberPath = "CategoryName";
                combobox_category.SelectedValuePath = "CategoryID";
            }
        }

        private void txtbox_productName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtbox_price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void combobox_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddProduct(string productName, decimal price, int categoryId)
        {
            using (var context = new MYLocalDBEntities())
            {
                var newProduct = new Product
                {
                    ProductName = productName,
                    UnitPrice = price,
                    CategoryID = categoryId
                };

                context.Products.Add(newProduct);
                context.SaveChanges();
                MessageBox.Show("Product added successfully!");
            }
        }
            private void btn_addProduct_Click(object sender, RoutedEventArgs e)
            {
                string product_name = txtbox_productName.Text.Trim();
                string price = txtbox_price.Text.Trim();
                var selectedCategoryId = combobox_category.SelectedValue;

                if (string.IsNullOrEmpty(product_name) || product_name.Any(char.IsDigit))
                {
                    MessageBox.Show("Please enter a valid product name (it should not contain numbers).");
                    return;
                }

                if (string.IsNullOrEmpty(price) || !decimal.TryParse(price, out decimal priceNum))
                {
                    MessageBox.Show("Please enter a valid price (it should be a numeric value).");
                    return;
                }
                if (selectedCategoryId == null)
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }
                AddProduct(product_name, priceNum, (int)selectedCategoryId);
            }



        }
    } 

