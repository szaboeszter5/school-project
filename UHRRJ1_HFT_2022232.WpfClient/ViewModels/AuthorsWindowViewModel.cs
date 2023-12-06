using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;
using UHRRJ1_HFT_2022232.Models;
using static UHRRJ1_HFT_2022232.Logic.BookLogic;

namespace UHRRJ1_HFT_2022232.WpfClient.ViewModels
{
    public class AuthorsWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        static RestService rest;

        public RestCollection<Author> Authors { get; set; }

        public ObservableCollection<Book> AuthorsBooks { get; set; }
        public ObservableCollection<BookStore> AuthorsBookStores { get; set; }
        public ObservableCollection<AuthorsBookCount> ListByNumberOfBooks { get; set; }

        private Author selectedAuthor;
        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                if (value != null)
                {
                    selectedAuthor = new Author()
                    {
                        AuthorId = value.AuthorId,
                        AuthorName = value.AuthorName,
                        Books = value.Books
                    };

                    OnPropertyChanged();
                    (DeleteAuthorCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateAuthorCommand as RelayCommand).NotifyCanExecuteChanged();
                    PropertyChanged += Refresh;
                }
            }
        }

        public ICommand CreateAuthorCommand { get; set; }
        public ICommand DeleteAuthorCommand { get; set; }
        public ICommand UpdateAuthorCommand { get; set; }

        public ICommand ListByNumberOfBooksCommand { get; set; }

        void GetBooks(string name)
        {
            AuthorsBooks.Clear();
            var result = rest.Get<Book>($"/WrittenBooks/WrittenBooks?authorName={name}");
            foreach (var item in result)
            {
                AuthorsBooks.Add(item);
            }
        }

        void GetBookStores(string name)
        {
            AuthorsBookStores.Clear();
            var result = rest.Get<BookStore>($"AuthorsStores/Stores?authorName={name}");
            foreach (var item in result)
            {
                AuthorsBookStores.Add(item);
            }
        }

        void GetListByNumberOfBooks()
        {
            ListByNumberOfBooks.Clear();
            var result = rest.Get<AuthorsBookCount>($"AuthorBookNumber/AuthorsByNumberOfBooks");
            foreach (var item in result)
            {
                ListByNumberOfBooks.Add(item);
            }
        }

        void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            GetBooks(SelectedAuthor.AuthorName);
            GetBookStores(SelectedAuthor.AuthorName);
        }

        public AuthorsWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                rest = new RestService("http://localhost:23125/");

                //CRUD
                Authors = new RestCollection<Author>("http://localhost:23125/", "Author", "hub");

                CreateAuthorCommand = new RelayCommand(() =>
                {
                    Authors.Add(new Author()
                    {
                        AuthorName = SelectedAuthor.AuthorName
                    });
                    System.Threading.Thread.Sleep(150);
                    Authors.Update(Authors.Last());
                });
                UpdateAuthorCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Authors.Update(SelectedAuthor);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                DeleteAuthorCommand = new RelayCommand(() =>
                {
                    Authors.Delete(SelectedAuthor.AuthorId);
                },
                () =>
                {
                    return SelectedAuthor != null;
                });

                SelectedAuthor = new Author();

                //non-crud
                AuthorsBooks = new ObservableCollection<Book>();
                AuthorsBookStores = new ObservableCollection<BookStore>();
                ListByNumberOfBooks = new ObservableCollection<AuthorsBookCount>();

                ListByNumberOfBooksCommand = new RelayCommand(() =>
                {
                    GetListByNumberOfBooks();
                    ListByNumberOfBooksWindow w = new ListByNumberOfBooksWindow();
                    w.DataContext = this;
                    w.lb.ItemsSource = ListByNumberOfBooks;
                    w.ShowDialog();
                });
            }
        }
    }
}