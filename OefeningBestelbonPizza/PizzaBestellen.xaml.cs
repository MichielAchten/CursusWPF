using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OefeningBestelbonPizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static string grootte;
        static string ingrediënten = "tomaat en kaas";
        static string extra = "";
        static int aantal = 1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBlockBestelling.TextWrapping = TextWrapping.Wrap;
            textBlockBestelling.Text = "U heeft " + aantal + " " + grootte + " pizza('s') besteld met "
                + ingrediënten + extra;
        }

        private void Grootte_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton knop = (RadioButton)sender;
            foreach (FrameworkElement kind in groottePanel.Children)
            {
                if (knop.IsChecked == true)
                {
                    if (knop.Name == "RadioButtonSmall")
                    {
                        grootte = "small";
                    }
                    else if (knop.Name == "RadioButtonMedium")
                    {
                        grootte = "medium";
                    }
                    else
                    {
                        grootte = "large";
                    }
                }
            }
        }

        private void CheckBoxIngrediënt_Click(object sender, RoutedEventArgs e)
        {
            //CheckBox vak = (CheckBox)sender;
            ingrediënten = "";
            List<string> ingrediëntenLijst = new List<string>{"tomaat","kaas"};
            if (CheckBoxHam.IsChecked == true)
            {
                ingrediëntenLijst.Add("ham");
            }
            if (CheckBoxAnanas.IsChecked == true)
            {
                ingrediëntenLijst.Add("ananas");
            }
            if (CheckBoxSalami.IsChecked == true)
            {
                ingrediëntenLijst.Add("salami");
            }
            if (ingrediëntenLijst.Count() > 2)
            {
                for (int i = 0; i < ingrediëntenLijst.Count() - 1; i++)
                {
                    ingrediënten += ingrediëntenLijst[i] + ", ";
                }
                ingrediënten = ingrediënten.Substring(0, ingrediënten.Length - 2);
            }
            else
            {
                ingrediënten += ingrediëntenLijst[0];
            }
            ingrediënten += " en " + ingrediëntenLijst[ingrediëntenLijst.Count() - 1];
            ingrediëntenLijst.Clear();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            //ToggleButton knop = (ToggleButton)sender;
            extra = "";
            List<string> extraLijst = new List<string>();
            if (ExtraKorstToggleButton.IsChecked == true)
            {
                extraLijst.Add(" met extra dikke korst ");
            }
            if (ExtraKaasToggleButton.IsChecked == true)
            {
                extraLijst.Add(" overstrooid met extra kaas ");
            }
            if (extraLijst.Count() > 1)
            {
                for (int i = 0; i < extraLijst.Count() - 1; i++)
                {
                    extra += extraLijst[i] + ", ";
                }
                extra = extra.Substring(0, extra.Length - 2);
                extra += "en" + extraLijst[extraLijst.Count() - 1];
            }
            else if (extraLijst.Count() == 1)
            {
                extra += extraLijst[0];
            }
            else
            {
                extra = "";
            }
            extraLijst.Clear();
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (aantal < 10)
            {
                aantal += 1;
            }
            Aantal.Content = aantal;
        }

        private void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            if (aantal > 1)
            {
                aantal -= 1;
            }
            Aantal.Content = aantal;
        }
    }
}
