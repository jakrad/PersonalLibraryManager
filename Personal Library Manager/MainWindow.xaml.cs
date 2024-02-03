using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Personal_Library_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public MainWindow()
        {
            InitializeComponent();
            lvBooks.ItemsSource = Books;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var book = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Year = txtYear.Text,
                ISBN = txtISBN.Text
            };
            Books.Add(book);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvBooks.SelectedItem is Book selectedBook)
            { 
                selectedBook.Title = txtTitle.Text;
                selectedBook.Author = txtAuthor.Text;
                selectedBook.Year = txtYear.Text;
                selectedBook.ISBN = txtISBN.Text;

                lvBooks.Items.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = lvBooks.SelectedItem as Book;
            if (selectedBook != null)
            {
                Books.Remove(selectedBook);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.json)|*.json",
                DefaultExt = "json",
                FileName = "BookLibrary"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var booksJson = JsonSerializer.Serialize(Books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(saveFileDialog.FileName, booksJson);
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.json)|*.json",
                DefaultExt = "json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var booksJson = File.ReadAllText(openFileDialog.FileName);
                var books = JsonSerializer.Deserialize<ObservableCollection<Book>>(booksJson);
                //Books.Clear();
                lvBooks.ItemsSource= books;

            }
        }
    }
}