﻿using System;
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

namespace HobbyLijst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HobbyLijstWindow : Window
    {
        public HobbyLijstWindow()
        {
            InitializeComponent();
        }

        public List<Hobby> hobbies = new List<Hobby>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //hobbies.Add(new Hobby("sport", "voetbal", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\voetbal.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("sport", "atletiek", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\atletiek.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("sport", "basketbal", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\basketbal.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("sport", "tennis", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\tennis.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("sport", "turnen", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\turnen.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("muziek", "trompet", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\trompet.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("muziek", "drum", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\drum.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("muziek", "gitaar", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\gitaar.jpg", UriKind.Absolute))));
            //hobbies.Add(new Hobby("muziek", "piano", new BitmapImage(new Uri(@"C:\Users\net04\Documents\Visual Studio 2013\Projects\WpfCursus\HobbyLijst\images\piano.jpg", UriKind.Absolute))));

            hobbies.Add(new Hobby("sport", "voetbal", new BitmapImage(new Uri(@"images\voetbal.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "atletiek", new BitmapImage(new Uri(@"images\atletiek.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "basketbal", new BitmapImage(new Uri(@"images\basketbal.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "tennis", new BitmapImage(new Uri(@"images\tennis.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "turnen", new BitmapImage(new Uri(@"images\turnen.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "trompet", new BitmapImage(new Uri(@"images\trompet.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "drum", new BitmapImage(new Uri(@"images\drum.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "gitaar", new BitmapImage(new Uri(@"images\gitaar.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "piano", new BitmapImage(new Uri(@"images\piano.jpg", UriKind.Relative))));

            ComboBoxCategorie.Items.Add("- allecategorieën -");
            ComboBoxCategorie.Items.Add("muziek");
            ComboBoxCategorie.Items.Add("sport");
            ComboBoxCategorie.SelectedIndex = 0;
        }

        private void ComboBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxHobbies.Items.Clear();
            foreach (Hobby hob in hobbies)
            {
                if (hob.Categorie == ComboBoxCategorie.SelectedItem.ToString() || ComboBoxCategorie.SelectedIndex == 0)
                {
                    ListBoxHobbies.Items.Add(hob);
                }
                ListBoxHobbies.Items.SortDescriptions.Add(new SortDescription("Activiteit", ListSortDirection.Ascending));
            }
        }

        private void ButtonKies_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListBoxHobbies.SelectedItem != null)
            {
                Hobby gekozenHobby = (Hobby)ListBoxHobbies.SelectedItem;
                //ListBoxGekozen.Items.Add(gekozenHobby.Categorie + " : " + gekozenHobby.Activiteit);
                ListBoxGekozen.Items.Add(gekozenHobby);
                ListBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Categorie", ListSortDirection.Ascending));
                ListBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Activiteit", ListSortDirection.Ascending));
            }
        }

        private void ButtonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxGekozen.SelectedIndex >= 0)
            {
                ListBoxGekozen.Items.RemoveAt(ListBoxGekozen.SelectedIndex);
            }
        }

        private void ButtonSamenvatting_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wil je de gekozen hobby's op een rijtje?", "Samenvatting", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                string mijnTekst = "Mijn hobby's zijn: ";
                string cat = string.Empty;

                foreach (Object item in ListBoxGekozen.Items)
                {
                    Hobby mijnHobby = (Hobby)item;
                    if (cat != mijnHobby.Categorie)
                    {
                        cat = mijnHobby.Categorie;
                        mijnTekst += "\n" + mijnHobby.Categorie + " : " + mijnHobby.Activiteit;
                    }
                    else
                    {
                        mijnTekst += ", " + mijnHobby.Activiteit;
                    }
                }
                if (ListBoxGekozen.Items.Count == 0)
                {
                    MessageBox.Show("Ik heb geen hobby's", "Samenvatting", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(mijnTekst, "Samenvatting", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
