using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand OpenReadersCommand { get; set; }
        public ICommand OpenAuthorsCommand { get; set; }
        public ICommand OpenBooksCommand { get; set; }
        public ICommand OpenBookstoresCommand { get; set; }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                OpenReadersCommand = new RelayCommand(
                    () =>
                    {
                        ReadersWindow w = new ReadersWindow();
                        w.ShowDialog();
                    });

                OpenAuthorsCommand = new RelayCommand(
                    () =>
                    {
                        AuthorsWindow w = new AuthorsWindow();
                        w.ShowDialog();
                    });

                OpenBooksCommand = new RelayCommand(
                    () =>
                    {
                        BooksWindow w = new BooksWindow();
                        w.ShowDialog();
                    });

                OpenBookstoresCommand = new RelayCommand(
                    () =>
                    {
                        BookStoresWindow w = new BookStoresWindow();
                        w.ShowDialog();
                    });
            }
        }
    }
}
