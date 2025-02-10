using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KajakGui
{
    class Kolcsonzes
    {
        public Kolcsonzes(string r)
        {
            var v = r.Split(';');
            Nev = v[0];

            if (Nev.Contains(','))
            {
                var nevekCsoport = Nev.Split(',');
                var temping = nevekCsoport[0].Trim();
                nevekCsoport[0] = nevekCsoport[1].Trim();
                nevekCsoport[1] = temping;

                Nev = $"{nevekCsoport[0]}, {nevekCsoport[1]}";
            }


            Id = int.Parse(v[1]);
            HajoTipus = v[2];
            SzemelyekSzama = int.Parse(v[3]);
            elvitelOraja = int.Parse(v[4]);
            elvitelPerce = int.Parse(v[5]);
            VisszahozatalOraja = int.Parse(v[6]);
            VisszahozatalPerce = int.Parse(v[7]);








        }

        public bool VizennVanE(int ora, int perc)
        {

            int elvitelIdoPercben = elvitelOraja * 60 + elvitelPerce;
            int visszahozatalIdoPercben = VisszahozatalOraja * 60 + VisszahozatalPerce;
            int keresettIdoPercben = ora * 60 + perc;
            return keresettIdoPercben >= elvitelIdoPercben + 1 && keresettIdoPercben <= visszahozatalIdoPercben;
        }
        public int perceAlakitas() {

            int allelvitel = elvitelOraja * 60 + elvitelPerce;
            int allvisszahozatal = VisszahozatalOraja * 60 + VisszahozatalPerce;

            int talalat = allvisszahozatal - allelvitel;
            return talalat;
        }
        public int mennyiFelora() {

            int perc = perceAlakitas();
            int talalat = (int)Math.Ceiling((double)perc / 30);
            return talalat;
        }

      

        public string Nev { get; set; }
        public int Id { get; set; }
        public string HajoTipus { get; set; }
        public int SzemelyekSzama { get; set; }
        public int elvitelOraja { get; set; }
        public int elvitelPerce { get; set; }
        public int VisszahozatalOraja { get; set; }
        public int VisszahozatalPerce { get; set; }

        public override string ToString()
        {
            int percalakitas = perceAlakitas();
            int ennyifelora = mennyiFelora();
            return $"{Id}\t{percalakitas} perc \t{ennyifelora} fél óra kezdődött meg ";
        }

    }
}
