using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S05_Events_i_Grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button primer;
        Button segon;
        bool primerclic = true;
        int totalCartes;
        int totalParelles;
        int totalIntens = 0;    

        public MainWindow()
        {
            InitializeComponent();
            Randomizar();
            totalParelles = totalCartes / 2;
            intents.Text = "Número d'intents: 0";
            parelles.Text = "Parelles restants: " + totalParelles.ToString();
            
        }

        public List<string> grid = new List<string>
        {
            "elon.png", "elon.png", "España.png", "España.png", "hasbulla.jpg", "hasbulla.jpg",
            "increible.jpg", "increible.jpg", "mimir.jpg", "mimir.jpg", "mono.jpg", "mono.jpg",
            "sasuke.jpg", "sasuke.jpg", "vaporeon.jpg", "vaporeon.jpg"
        };

        

        public void Randomizar()
        {
            Random random = new Random();
            int posicioAleatoria;
            

            for (int i = 0; i < memoryGrid.Children.Count; i++)
            {
                if(grid.Count > 0)
                {
                    Border border = memoryGrid.Children[i] as Border;
                    Button button = border.Child as Button;

                    posicioAleatoria = random.Next(0, grid.Count);

                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(grid[posicioAleatoria], UriKind.Relative)),
                        Stretch = System.Windows.Media.Stretch.Fill
                    };

                    button.Name = grid[posicioAleatoria][0].ToString() + grid[posicioAleatoria][1].ToString(); 

                    grid.RemoveAt(posicioAleatoria);
                    totalCartes++;

                }
            }
        }


        public async void Parella(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            

            if (primerclic)
            {
                primer = button;
                primer.Opacity = 1;
                primerclic = false;               
            }
            else 
            {
                segon = button;
                segon.Opacity = 1;              
                
                if (primer.Name != segon.Name)
                {
                    await DelayedFunction();
                    primer.Opacity = 0;
                    segon.Opacity = 0;

                    totalIntens++;
                    intents.Text = "Número d'intents: " + totalIntens.ToString();

                }
                else if (totalParelles > 1)
                {
                    primer.IsEnabled = false;
                    segon.IsEnabled = false;

                    totalIntens++;
                    intents.Text = "Número d'intents: " + totalIntens.ToString();
                    totalParelles--;
                    parelles.Text = "Parelles restants: " + totalParelles.ToString();

                }
                else
                {
                    totalIntens++;
                    intents.Text = "Número d'intents: " + totalIntens.ToString();
                    totalParelles--;
                    parelles.Text = "Parelles restants: " + totalParelles.ToString();
                    Guanyar();
                }

                primerclic = true;
            }                                          
        }
        static async Task DelayedFunction()
        {          
            await Task.Delay(500); // Delay de 0.5 segonds (500 milisegonds)        
        }

        public void Guanyar()
        {

            MessageBoxResult guanyar = MessageBox.Show("Enhorabona has Guanyat!!");

        }
    }
}
