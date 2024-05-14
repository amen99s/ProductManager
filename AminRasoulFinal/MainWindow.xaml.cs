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

namespace AminRasoulFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void LoadCategories()
        {
            using (var context = new MYLocalDBEntities()) 
            {
                
                var categories = context.Categories.ToList();

                
                combobox_getproductByCategory.ItemsSource = categories;
                combobox_getproductByCategory.DisplayMemberPath = "CategoryName"; 
                combobox_getproductByCategory.SelectedValuePath = "CategoryID"; 
            }
        }

        private void combobox_getproductByCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (combobox_getproductByCategory.SelectedValue != null)
            {
                int selectedCategoryId = (int)combobox_getproductByCategory.SelectedValue;
                using (var context = new MYLocalDBEntities())
                {
                    var products = context.Products.Where(p => p.CategoryID == selectedCategoryId).ToList();
                    grid_data.ItemsSource = products;
                }
            }

        }


        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtBox_search.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                using (var context = new MYLocalDBEntities()) 
                {
                    var products = context.Products.Where(p => p.ProductName.Contains(searchTerm)).ToList();
                    if (products.Count > 0)
                    {
                        grid_data.ItemsSource = null;
                        grid_data.ItemsSource = products;
                    }
                    else
                    {
                        MessageBox.Show("No products found with the given search term.");
                        grid_data.ItemsSource = null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a search term.");
            }
        }

        private void btn_addNewProdut_Click(object sender, RoutedEventArgs e)
        {
            AddNewProduct addnewproduct = new AddNewProduct();
            addnewproduct.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        //this btn of get All product
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            LoadCategories();

            if(combobox_getproductByCategory.SelectedValue == null)
            {
                using (var context = new MYLocalDBEntities())
                {
                    var products = context.Products.ToList();
                    grid_data.ItemsSource = products;
                }
            }
        }

        //btn of Clear Data
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(grid_data.ItemsSource != null)
            {
                grid_data.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("Its already empty");
            }
            
        }

        private void txtBox_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
