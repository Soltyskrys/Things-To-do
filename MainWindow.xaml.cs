using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Xml.Serialization;

namespace ThingsDoTo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Event> Events;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Events = new ObservableCollection<Event>(loadEvents());
            }
            catch(SerializationException)
            {
                showWarning("Błąd danych", "Dziennik niemożliwy do odtworzenia, przyczyna : błąd serializacji");
                Events = new ObservableCollection<Event>();
            }
            catch(FileNotFoundException)
            {
                showWarning("Nie odnaleziono kalendarza", "Otwieram nowy kalendarz");
                Events = new ObservableCollection<Event>();
            }


            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = Events;
        }

        private static void showWarning(String header,String text)
        {
            string messageBoxText = text;
            string caption = header;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void openAddEventWindow(object sender, RoutedEventArgs e)
        {
            Action<Event> addEvent = AddEvent;

            AddEvent addEventWindow = new AddEvent(addEvent);
            addEventWindow.Show();
        }

        public void AddEvent(Event added)
        {
            Events.Add(added);
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.DeferRefresh();
            
        }

        private void saveEvents()
        {
            FileStream stream = File.Create("events");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, Events.ToList());
            stream.Close();
        }

        private List<Event> loadEvents()
        {
            FileStream stream = File.OpenRead("events");
            var formatter = new BinaryFormatter();
            List<Event> events = (List<Event>)formatter.Deserialize(stream);
            stream.Close();
            return events;
        }

        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveEvents();
        }
    }
}
