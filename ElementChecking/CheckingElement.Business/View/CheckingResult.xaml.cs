using CheckingElement.Business.Model;
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
    /// CheckingResult.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingResult : Window {
        public CheckingResult(IList<RevitElement> elems) {
            InitializeComponent();
            DataContext = new CheckingResultViewModel(elems);
        }
    }
}
