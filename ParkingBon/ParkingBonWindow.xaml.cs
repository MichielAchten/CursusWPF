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
using System.IO;
using Microsoft.Win32;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {


        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
        }

        private void Nieuw()
        {
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;
            StatusItem.Content = "nieuwe bon";
            SaveEnAfdruk(false);
        }

        private void SaveEnAfdruk(bool actief)
        {
            ButtonAfdrukvoorbeeld.IsEnabled = actief;
            ButtonOpslaan.IsEnabled = actief;
            MenuAfdrukVoorbeeld.IsEnabled = actief;
            MenuBonOplsaan.IsEnabled = actief;
        }

        private void Window_Closing(object sender,
        System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten",
            MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace("€", ""));
            if (bedrag > 0)
            {
                bedrag -= 1;
            }
            if (bedrag == 0)
            {
                ButtonAfdrukvoorbeeld.IsEnabled = false;
                ButtonOpslaan.IsEnabled = false;
                MenuAfdrukVoorbeeld.IsEnabled = false;
                MenuBonOplsaan.IsEnabled = false;
            }
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content =
            Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 *
            bedrag).ToLongTimeString();
            SaveEnAfdruk(!(bedrag == 0));
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace("€", ""));
            DateTime vertrekuur =
            Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
            {
                bedrag += 1;
            }
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content =
            Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 *
            bedrag).ToLongTimeString();
            ButtonAfdrukvoorbeeld.IsEnabled = true;
            ButtonOpslaan.IsEnabled = true;
            MenuAfdrukVoorbeeld.IsEnabled = true;
            MenuBonOplsaan.IsEnabled = true;
            SaveEnAfdruk(!(bedrag == 0));
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Parkeerbonnen |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader invoer = new StreamReader(dlg.FileName))
                    {
                        DatumBon.SelectedDate = Convert.ToDateTime(invoer.ReadLine());
                        AankomstLabelTijd.Content = invoer.ReadLine();
                        TeBetalenLabel.Content = invoer.ReadLine();
                        VertrekLabelTijd.Content = invoer.ReadLine();
                    }
                    StatusItem.Content = dlg.FileName;
                    SaveEnAfdruk(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("openen mislukt : " + ex.Message);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                DateTime tijd = (DateTime)DatumBon.SelectedDate;
                dlg.FileName = tijd.Day.ToString() + "-" + tijd.Month.ToString() + "-" + tijd.Year.ToString() +
                    "om" + AankomstLabelTijd.Content.ToString().Replace(":", "-");
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Parkeerbonnen |*.bon";

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter uitvoer = new StreamWriter(dlg.FileName))
                    {
                        uitvoer.WriteLine(tijd.ToShortTimeString());
                        uitvoer.WriteLine(AankomstLabelTijd.Content);
                        uitvoer.WriteLine(TeBetalenLabel.Content);
                        uitvoer.WriteLine(VertrekLabelTijd.Content);
                    }
                    StatusItem.Content = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("opslaan mislukt : ", ex.Message);
            }
        }
        
        private double vertPositie;

        private void PrintPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(640, 320);
            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);
            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = 640;
            page.Height = 320;
            Image logo = new Image();
            logo.Source = logoImage.Source;
            logo.Margin = new Thickness(96);
            page.Children.Add(logo);
            vertPositie = 96;
            page.Children.Add(Regel("datum: " + DatumBon.Text));
            page.Children.Add(Regel("starttijd: " + AankomstLabelTijd.Content));
            page.Children.Add(Regel("eindtijd: " + VertrekLabelTijd.Content));
            page.Children.Add(Regel("bedrag betaald: " + TeBetalenLabel.Content));

            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = document;
            preview.ShowDialog();
        }

        private TextBlock Regel(string tekst)
        {
            TextBlock deRegel = new TextBlock();
            deRegel.Margin = new Thickness(300, vertPositie, 96, 96);
            deRegel.FontSize = 18;
            vertPositie += 36;
            deRegel.Text = tekst;
            return deRegel;
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        { 
            this.Close(); 
        }
    }
}

