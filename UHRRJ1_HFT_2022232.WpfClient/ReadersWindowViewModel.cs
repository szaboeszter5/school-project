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

        public RestCollection<Reader> Readers { get; set; }

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
                }
            }
        }

        public ICommand CreateReaderCommand { get; set; }
        public ICommand DeleteReaderCommand { get; set; }
        public ICommand UpdateReaderCommand { get; set; }
        public ICommand ListByNumberOfBooksCommand { get; set; }

        public ReadersWindowViewModel()
        {
            if (!IsInDesignMode)
            {
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
            }
        }
    }
}
