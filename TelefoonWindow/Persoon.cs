using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Telefoon
{
    public class Persoon
    {
        public string Naam { get; set; }
        public string Telefoonnr { get; set; }
        public string Groep { get; set; }
        public BitmapImage Foto { get; set; }

        public Persoon(string nNaam, string nTelefoonnr, string nGroep, BitmapImage nFoto)
        {
            Naam = nNaam;
            Telefoonnr = nTelefoonnr;
            Groep = nGroep;
            Foto = nFoto;
        }
    }
}
