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
            #endregion



        }


        public bool f15(int id, string hajotipusa, int szemelyekszama)
        {
            foreach (var item in kolcsonzes)
            {
                if (item.Id == id || hajotipusa == hajotipusa || szemelyekszama == szemelyekszama)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
          
            return true;
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

        private void f16_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}