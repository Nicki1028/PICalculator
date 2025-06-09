using DIContainer;
using PI_Calculator.Extention;
using PI_Calculator.Interface;
using PI_Calculator.Models;
using PI_Calculator.ViewModels;
using System.Diagnostics;
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

namespace PI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICalculatorView
    {
        CalcuatorViewModel calcuatorViewModel = new CalcuatorViewModel();
        private System.Threading.Timer? Timer;
        private readonly ICalculatorPresenter presenter;
        

        Dictionary<long, PIModel> items = new Dictionary<long, PIModel>();
        public MainWindow(DIContainer.PresenterFactory factory)
        {
            InitializeComponent();          
            this.DataContext = calcuatorViewModel;
            presenter = factory.Create<ICalculatorPresenter, ICalculatorView>(this);
            Timer = new System.Threading.Timer(x => presenter.FetchCompletedPiResults(), null, 1000, 1000);
           
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.Timerextention(() =>
            {               
                long size = long.Parse(sampleSizeText.Text);
                if (!items.ContainsKey(size))
                {
                    PIModel pIModel = new PIModel(size, DateTime.Now, 0);
                    calcuatorViewModel.Add(pIModel);
                    items.Add(size, pIModel);
                }                             
                presenter.AddMission(size);
            });

        }

        public void UpdatePIResult(List<PIModel> pIModels)
        {
            calcuatorViewModel.datas = pIModels;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string content = btn.Content.ToString() == "Start" ? "Stop" : "Start";

            btn.Content = content;
            bool activate = content == "Stop" ? true : false;  
            presenter.ActivateServiceRunning(activate);

        }
    }
}