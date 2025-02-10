using KajakGui;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KajakGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kolcsonzes> kolcsonzes = new List<Kolcsonzes>();
        List<Kolcsonzes> kolcsonzes2 = new List<Kolcsonzes>();
        public MainWindow()
        {





            InitializeComponent();
            beolvasas();
            foreach (var item in kolcsonzes)
            {
                alapGrid.Items.Add(item);
            }

            for (int i = 0; i < 24; i++)
            {
                Ora.Items.Add(i);
            }
            for (int i = 0; i < 59; i++)
            {
                Perc.Items.Add(i);
            }

            foreach (var item in kolcsonzes)
            {
                Listbox.Items.Add(item.ToString());
            }







            #region comboboxFeltoltes
            for (int i = 1; i < 26; i++)
            {
                HajodIdCb.Items.Add(i);
            }
            var hajotipusok = kolcsonzes.Select(x=>x.HajoTipus).Distinct().ToList();
            foreach (var item in hajotipusok)
            {
                HajoTipusCb.Items.Add(item);
            }
            var szemelyekSzama = kolcsonzes.Select(x => x.SzemelyekSzama).Distinct().ToList();
            foreach (var item in szemelyekSzama)
            {
                HajoSzemelyekSzamaCB.Items.Add(item);
            }
            for (int i = 0; i < 60; i++)
            {
                elvitelPerceCb.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                VisszahozatalPerce.Items.Add(i);
            }
            for (int i = 0; i < 24; i++)
            {
                elvitelOrajaCb.Items.Add(i);
            }
            for (int i = 0; i < 24; i++)
            {
                VisszahozatalOrajaCb.Items.Add(i);
            }
            #endregion




           

        }



        private bool f15(int id, string hajotipusa, int szemelyekszama)
        {
            return !kolcsonzes.Any(item => item.Id == id && item.HajoTipus == hajotipusa && item.SzemelyekSzama == szemelyekszama);
        }




        public void beolvasas()
        {
            
            
            using StreamReader sr = new($"../../../src/kolcsonzes.txt", Encoding.UTF8);
            _= sr.ReadLine();
            while (!sr.EndOfStream)
            {
                kolcsonzes.Add(new Kolcsonzes(sr.ReadLine()));
            }

        
        }

        private void Perc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (Ora.SelectedItem == null || Perc.SelectedItem == null)
                {
                    MessageBox.Show("Válassz ki egy időpontot!");
                    return;
                }
                int ora = (int)Ora.SelectedItem;
                int perc = (int)Perc.SelectedItem;
                var vizenvan = kolcsonzes.Where(k => k.VizennVanE(ora, perc)).ToList();

                if (vizenvan.Count == 0)
                {
                    MessageBox.Show("Nincs a megadott időpontban vízen lévő hajó.");
                }
                else
                {
                    foreach (var item in vizenvan)
                    {
                        szamoloGrid.Items.Add(item);
                    }
                }
            }



        }

        private void napibevetelBtn_Click(object sender, RoutedEventArgs e)
        {
            var napi = kolcsonzes.Sum(x => x.mennyiFelora() * 1500);
            eredmenyLbl.Content = $"a napi bevétel {napi} ft";
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NevTextBox.Text.Contains(" "))
            {
                MessageBox.Show("A névben szükséges megadni 'szóköz' karaktert");
            }
            else
            {
                if (f15((int)HajodIdCb.SelectedItem, HajoTipusCb.Text, (int)HajoSzemelyekSzamaCB.SelectedItem))
                {
                    kolcsonzes.Add(new Kolcsonzes(
                        NevTextBox.Text,
                        (int)HajodIdCb.SelectedItem,
                        HajoTipusCb.SelectedItem.ToString(),
                        (int)HajoSzemelyekSzamaCB.SelectedItem,
                        (int)elvitelOrajaCb.SelectedItem,
                        (int)elvitelPerceCb.SelectedItem,
                        (int)VisszahozatalOrajaCb.SelectedItem,
                        (int)VisszahozatalPerce.SelectedItem
                    ));

                    MessageBox.Show("Új kölcsönzés sikeresen hozzáadva!");
                }
                else
                {
                    MessageBox.Show("Ez a hajó már szerepel az adatbázisban!");
                }

            }
        }
    }
}


































//if (!NevTextBox.Text.Contains(" "))
//{
//    MessageBox.Show("A névben szükséges megadni szóközt!");
//    return;
//}

//if (!f15((int)HajodIdCb.SelectedItem, HajoTipusCb.Text, (int)HajoSzemelyekSzamaCB.SelectedItem))
//{
//    MessageBox.Show("Ez a hajó már szerepel az adatbázisban!");
//    return;
//}
//var ujKolcsonzes = new Kolcsonzes
//{
//    Nev = NevTextBox.Text,
//    Id = (int)HajodIdCb.SelectedItem,
//    HajoTipus = HajoTipusCb.Text,
//    SzemelyekSzama = (int)HajoSzemelyekSzamaCB.SelectedItem,
//    elvitelOraja = (int)elvitelOrajaCb.SelectedItem,
//    elvitelPerce = (int)elvitelPerceCb.SelectedItem,
//    VisszahozatalOraja = (int)VisszahozatalOrajaCb.SelectedItem,
//    VisszahozatalPerce = (int)VisszahozatalPerce.SelectedItem
//};

//kolcsonzes.Add(ujKolcsonzes);
//MessageBox.Show("Új kölcsönzés sikeresen hozzáadva!");
//this.DialogResult = true;
//this.Close();