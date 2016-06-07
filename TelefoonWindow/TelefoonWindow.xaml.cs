using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ComponentModel;
using System.Media;

namespace Telefoon
{
    /// <summary>
    /// Interaction logic for TelefoonWindow.xaml
    /// </summary>
    public partial class TelefoonWindow : Window
    {
        public TelefoonWindow()
        {
            InitializeComponent();
        }

        public List<Persoon> personen = new List<Persoon>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string bron = @"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\TelefoonWindow\Images";
            
            personen.Add(new Persoon("Anne", "03 213 45 06", "Vrienden", new BitmapImage(new Uri(bron + @"\anne.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("Bob", "03 213 45 07", "Vrienden", new BitmapImage(new Uri(bron + @"\bob.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega1", "03 213 45 08", "Werk", new BitmapImage(new Uri(bron + @"\collega1.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega2", "03 213 45 09", "Werk", new BitmapImage(new Uri(bron + @"\collega2.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("collega3", "03 213 45 10", "Werk", new BitmapImage(new Uri(bron + @"\collega3.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("Ed", "03 213 45 11", "Vrienden", new BitmapImage(new Uri(bron + @"\ed.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("grote zus", "03 213 45 12", "Familie", new BitmapImage(new Uri(bron + @"\grotezus.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("kleine zus", "03 213 45 13", "Familie", new BitmapImage(new Uri(bron + @"\kleinezus.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("tante non", "03 213 45 14", "Familie", new BitmapImage(new Uri(bron + @"\tantenon.jpg", UriKind.Absolute))));
            personen.Add(new Persoon("vader", "03 213 45 15", "Familie", new BitmapImage(new Uri(bron + @"\vader.jpg", UriKind.Absolute))));

            //personen.Add(new Persoon("Anne", "03 213 45 06", "Vrienden", new BitmapImage(new Uri(@"Images\anne.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("Bob", "03 213 45 07", "Vrienden", new BitmapImage(new Uri(@"Images\bob.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("collega1", "03 213 45 08", "Werk", new BitmapImage(new Uri(@"Images\collega1.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("collega2", "03 213 45 09", "Werk", new BitmapImage(new Uri(@"Images\collega2.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("collega3", "03 213 45 10", "Werk", new BitmapImage(new Uri(@"Images\collega3.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("Ed", "03 213 45 11", "Vrienden", new BitmapImage(new Uri(@"Images\ed.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("grote zus", "03 213 45 12", "Familie", new BitmapImage(new Uri(@"Images\grotezus.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("kleine zus", "03 213 45 13", "Familie", new BitmapImage(new Uri(@"Images\kleinezus.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("tante non", "03 213 45 14", "Familie", new BitmapImage(new Uri(@"Images\tantenon.jpg", UriKind.Relative))));
            //personen.Add(new Persoon("vader", "03 213 45 15", "Familie", new BitmapImage(new Uri(@"Images\vader.jpg", UriKind.Relative))));

            foreach (Persoon dePersoon in personen)
            {
                ListBoxPersonen.Items.Add(dePersoon);
            }

            ComboBoxGroepen.Items.Add("Iedereen");
            ComboBoxGroepen.Items.Add("Familie");
            ComboBoxGroepen.Items.Add("Vrienden");
            ComboBoxGroepen.Items.Add("Werk");
            ComboBoxGroepen.SelectedIndex = 0;
        }

        private void ComboBoxGroep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxPersonen.Items.Clear();
            foreach (Persoon pers in personen)
            {
                if (ComboBoxGroepen.SelectedItem.ToString() == "Iedereen")
                {
                    ListBoxPersonen.Items.Add(pers);
                }
                else if (pers.Groep == ComboBoxGroepen.SelectedItem.ToString())
                {
                    ListBoxPersonen.Items.Add(pers);
                }
                ListBoxPersonen.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
            }
            
            //foreach (Persoon pers in personen)
            //{
            //    if (pers.Groep == ComboBoxGroepen.SelectedItem.ToString() || ComboBoxGroepen.SelectedIndex == 0)
            //    {
            //        ListBoxPersonen.Items.Add(pers);
            //    }
            //    ListBoxPersonen.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
            //}
        }

        private void ButtonBellen_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPersonen.SelectedIndex >= 0)
            {
                Persoon beller = (Persoon)ListBoxPersonen.SelectedItem;
                if (MessageBox.Show("Wil je " + beller.Naam + " bellen \nop het nummer: " + beller.Telefoonnr, "Telefoon", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    SoundPlayer speler = new SoundPlayer(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\TelefoonWindow\Phone.Wav");
                    speler.Play();
                }
            }
            else
            {
                MessageBox.Show("Je moet eerst iemand selecteren", "Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
            //Persoon pers = (Persoon)ListBoxPersonen.SelectedItem;
            //if (ListBoxPersonen.SelectedItem == null)
            //{
            //    MessageBox.Show("Je moet eerst iemand selecteren","Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}
            //else if (MessageBox.Show("Wil je " + pers.Naam + " bellen \n op het nummer: " + pers.Telefoonnr, "Telefoon", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            //{
            //    SoundPlayer speler = new SoundPlayer(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\TelefoonWindow\PHONE.wav");
            //    //SoundPlayer speler = new SoundPlayer("PHONE.wav");
            //    speler.Play();
            //}
        }
    }
}
