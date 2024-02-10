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
using SAM_Analytical_MachineLearning;

namespace SAM_Analytical
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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ListBox_Results.Items.Clear();

            //Load sample data
            SpaceType.ModelInput modelInput = new SpaceType.ModelInput()
            {
                Name = TextBox_Name.Text,
                Volume = float.NaN,
                Area = float.NaN,
                ThinnessRatio = float.NaN,
                AdjacentSpace1 = @"undefined",
                AdjacentSpace2 = @"undefined",
                AdjacentSpace3 = @"undefined",
                AdjacentSpace4 = @"undefined",
                AdjacentDoor1 = @"undefined",
                AdjacentDoor2 = @"undefined",
                AdjacentDoor3 = @"undefined",
                AdjacentDoor4 = @"undefined",
            };

            //Load model and predict output
            SpaceType.ModelOutput modelOutput = SpaceType.Predict(modelInput);

            IOrderedEnumerable<KeyValuePair<string, float>> orderedEnumerable =  SpaceType.PredictAllLabels(modelInput);
            if(orderedEnumerable == null)
            {
                return;
            }

            foreach(KeyValuePair<string, float> keyValuePair in orderedEnumerable)
            {
                string label = keyValuePair.Key;
                float score = keyValuePair.Value;

                Label textBox = new Label() { Content = string.Format("{0} {1}%", label, Math.Round(score * 100, 2))};

                ListBox_Results.Items.Add(textBox);
            }
        }
    }
}