using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPIntro.Models
{
    public class Osoba
    {
        private int id;
        private string ime;
        private string brTelefona;

        public Osoba()
        {
                
        }

        public string BrTelefona
        {
            get { return brTelefona; }
            set { brTelefona = value; }
        }


        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

    }
}
