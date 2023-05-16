using Autodesk.Revit.DB;
using CheckingElement.Business.ViewModel;
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

namespace CheckingElement.Business.View {
    /// <summary>
    /// Interaction logic for CheckingElementView.xaml
    /// </summary>
    public partial class CheckingElementView : Window {
        public CheckingElementView(Document doc) {
            InitializeComponent();

            DataContext = new CheckingElementViewModel(doc);
        }
        public CheckingElementView() {

            InitializeComponent();

            DataContext = new CheckingElementViewModel();
        }
    }
}
