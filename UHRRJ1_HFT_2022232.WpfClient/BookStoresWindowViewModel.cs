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

namespace UHRRJ1_HFT_2022232.WpfClient
{
    public class BookStoresWindowViewModel : ObservableRecipient
    {
        public RestCollection<BookStore> BookStores { get; set; }

        private BookStore selectedBookStore;

        public BookStore SelectedBookStore
        {
            get { return selectedBookStore; }
            set
            {
                if (value != null)
                {
                    selectedBookStore = new BookStore()
                    {
                        BookStoreName = value.BookStoreName,
                        BookStoreId = value.BookStoreId
                    };
                    OnPropertyChanged();
                    (DeleteBookStoreCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateBookStoreCommand { get; set; }

        public ICommand DeleteBookStoreCommand { get; set; }

        public ICommand UpdateBookStoreCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public BookStoresWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                BookStores = new RestCollection<BookStore>("http://localhost:23125/", "BookStore", "hub");
                CreateBookStoreCommand = new RelayCommand(() =>
                {
                    BookStores.Add(new BookStore()
                    {
                        BookStoreName = SelectedBookStore.BookStoreName
                    });
                });

                UpdateBookStoreCommand = new RelayCommand(() =>
                {
                    try
                    {
                        BookStores.Update(SelectedBookStore);
                    }
                    catch (ArgumentException ex)
                    {

                    }

                });

                DeleteBookStoreCommand = new RelayCommand(() =>
                {
                    BookStores.Delete(SelectedBookStore.BookStoreId);
                },
                () =>
                {
                    return SelectedBookStore != null;
                });
                SelectedBookStore = new BookStore();
            }
        }
    }
}
