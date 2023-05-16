using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CheckingElement.Business.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Command {
    [Transaction(TransactionMode.Manual)]
    public class ElementCheckCommand : IExternalCommand {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) { //实例WPF窗体
            var doc = commandData.Application.ActiveUIDocument.Document;

            var view = new CheckingElementView(doc);

            var mainUI = new System.Windows.Interop.WindowInteropHelper(view);
            mainUI.Owner = Process.GetCurrentProcess().MainWindowHandle;

            view.ShowDialog();

            return Result.Succeeded;
        }
    }
}
