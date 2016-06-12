using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ThingsDoTo
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {

        Action<Event> saveEventDelegate;
        Event operatedEvent;

        public AddEvent(Action<Event> addEvent)
        {
            saveEventDelegate = addEvent;
            InitializeComponent();
            comboBox.ItemsSource = Event.EventsType;
        }

        private void updateInput(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            clearInput();

            Type selected = (Type)combo.SelectedValue;
            addToInfoInput(selected.GetProperties());
        }

        private void addToInfoInput(PropertyInfo[] info )
        {
            operatedEvent = (Event)Activator.CreateInstance((Type)comboBox.SelectedItem);
            foreach (var property in info)
            {
                if (property.CanWrite)
                {
                    Label text = new Label();
                    text.Content = property.Name+":";
                    infoInput.Children.Add(text);

                    if (property.PropertyType.IsEnum)
                    {
                        addComboBox(property);
                    }
                    else if (property.PropertyType.IsEquivalentTo(typeof(DateTime)))
                    {
                        addCalendar(property);
                    }
                    else
                    {
                        addTextBox(property);
                    }
                }
            }
        }

        private void addTextBox(PropertyInfo property)
        {
            TextBox propertyDataInput = new TextBox();
            infoInput.Children.Add(propertyDataInput);
            propertyDataInput.TextChanged += (s, e) =>
            {
                property.SetValue(operatedEvent, propertyDataInput.Text);
            };
        }

        private void addCalendar(PropertyInfo property)
        {
            Calendar calendar = new Calendar();
            infoInput.Children.Add(calendar);
            calendar.SelectedDatesChanged += (s, e) =>
            {
                property.SetValue(operatedEvent, calendar.SelectedDate);
            };
        }

        private void addComboBox(PropertyInfo property)
        {
            ComboBox chooser = new ComboBox();
            chooser.SelectionChanged += (s, e) =>
            {
                property.SetValue(operatedEvent, chooser.SelectedValue);
            };
            chooser.ItemsSource = Enum.GetValues(property.PropertyType);
            infoInput.Children.Add(chooser);
        }

        private void clearInput()
        {
            infoInput.Children.Clear();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(operatedEvent != null)
                saveEventDelegate.Invoke(operatedEvent);
            this.Close();
        }
    }
}
