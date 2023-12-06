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
using static UHRRJ1_HFT_2022232.Logic.BookLogic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace UHRRJ1_HFT_2022232.WpfClient.ViewModels
{
    public class ReadersWindowViewModel : ObservableRecipient
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

        public RestCollection<Reader> Readers { get; set; }
        public ObservableCollection<Book> ListOwnedBooks { get; set; }
        public ObservableCollection<AuthorsBookCount> AuthorsAndNumberOfBooks { get; set; }

        private Reader selectedReader;
        public Reader SelectedReader
        {
            get { return selectedReader; }
            set
            {
                if (value != null)
                {
                    selectedReader = new Reader()
                    {
                        ReaderId = value.ReaderId,
                        ReaderName = value.ReaderName,
                        Books = value.Books
                    };

                    OnPropertyChanged();
                    (DeleteReaderCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateReaderCommand as RelayCommand).NotifyCanExecuteChanged();
                    PropertyChanged += Refresh;
                }
            }
        }

        public ICommand CreateReaderCommand { get; set; }
        public ICommand DeleteReaderCommand { get; set; }
        public ICommand UpdateReaderCommand { get; set; }

        void GetBooks(string name)
        {
            ListOwnedBooks.Clear();
            var result = rest.Get<Book>($"/OwnedBooks/OwnedBooks?readerName={name}");
            foreach (var item in result)
            {
                ListOwnedBooks.Add(item);
            }
        }

        void GetAuthorsAndNumberOfBooks(string name)
        {
            AuthorsAndNumberOfBooks.Clear();
            var result = rest.Get<AuthorsBookCount>($"ReadersAuthorsAndBooks/FavouriteAuthor?readerName={name}");
            foreach (var item in result)
            {
                AuthorsAndNumberOfBooks.Add(item);
            }
        }

        void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            GetBooks(SelectedReader.ReaderName);
            GetAuthorsAndNumberOfBooks(SelectedReader.ReaderName);
        }

        public ReadersWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                rest = new RestService("http://localhost:23125/");

                //CRUD
                Readers = new RestCollection<Reader>("http://localhost:23125/", "Reader", "hub");

                CreateReaderCommand = new RelayCommand(() =>
                {
                    Readers.Add(new Reader()
                    {
                        ReaderName = SelectedReader.ReaderName
                    });
                    System.Threading.Thread.Sleep(150);
                    Readers.Update(Readers.Last());
                });
                UpdateReaderCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Readers.Update(SelectedReader);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                DeleteReaderCommand = new RelayCommand(() =>
                {
                    Readers.Delete(SelectedReader.ReaderId);
                },
                () =>
                {
                    return SelectedReader != null;
                });

                SelectedReader = new Reader();

                //non-crud
                ListOwnedBooks = new ObservableCollection<Book>();
                AuthorsAndNumberOfBooks = new ObservableCollection<AuthorsBookCount>();
            }
        }
    }
}
