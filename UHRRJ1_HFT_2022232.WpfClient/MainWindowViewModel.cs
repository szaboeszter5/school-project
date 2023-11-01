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

        public RestCollection<Reader> Readers { get; set; }

        public ICommand CreateReaderCommand { get; set; }
        public ICommand DeleteReaderCommand { get; set; }
        public ICommand UpdateReaderCommand { get; set; }

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
                        ReaderName = value.ReaderName,
                        ReaderId = value.ReaderId,
                    };
                }
                OnPropertyChanged();
                (DeleteReaderCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public MainWindowViewModel()
        {
            if(!IsInDesignMode)
            {
                Readers = new RestCollection<Reader>("http://localhost:23125/", "reader", "hub");

                CreateReaderCommand = new RelayCommand(
                    () =>
                    {
                        Readers.Add(new Reader() { ReaderName = SelectedReader.ReaderName});
                    });

                DeleteReaderCommand = new RelayCommand(
                    () => { Readers.Delete(SelectedReader.ReaderId); },
                    () => { return SelectedReader != null; });

                UpdateReaderCommand = new RelayCommand(
                    () =>
                    {
                        Readers.Add(new Reader() { ReaderName = "Video T. Károly" });
                    });

                SelectedReader = new Reader();
            }
        }
    }
}
