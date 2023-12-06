using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.WpfClient.ViewModels
{
    public class BooksWindowViewModel : ObservableRecipient
    {
        public RestCollection<Book> Books { get; set; }

        private Book selectedBook;
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                if (value != null)
                {
                    selectedBook = new Book()
                    {
                        Title = value.Title,
                        BookId = value.BookId,
                        Price = value.Price,
                        AuthorId = value.AuthorId,
                        Release = value.Release,
                        Rating = value.Rating
                    };
                    OnPropertyChanged();
                    (DeleteBookCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public BooksWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Books = new RestCollection<Book>("http://localhost:23125/", "Book", "hub");

                CreateBookCommand = new RelayCommand(() =>
                {
                    Books.Add(new Book()
                    {
                        Title = SelectedBook.Title
                    });
                });

                UpdateBookCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Books.Update(SelectedBook);
                    }
                    catch (ArgumentException ex)
                    {

                    }

                });

                DeleteBookCommand = new RelayCommand(() =>
                {
                    Books.Delete(SelectedBook.BookId);
                },
                () =>
                {
                    return SelectedBook != null;
                });

                SelectedBook = new Book();
            }
        }
    }
}
