using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using CheckingElement.Business.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CheckingElement.App {
    [Transaction(TransactionMode.Manual)]
    public class Application : IExternalApplication {
        public Result OnShutdown(UIControlledApplication application) {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application) {
            //1.创建选项卡
            application.CreateRibbonTab("工具");
            //2.创建panel
            var panel = application.CreateRibbonPanel("工具", "审核");
            //3.按钮执行的类
            var pushButtonData = new PushButtonData("12", "审核", typeof(ElementCheckCommand).Assembly.Location, "CheckingElement.Business.Command.ElementCheckCommand");
            //4.添加按钮到panel
            var button = panel.AddItem(pushButtonData) as PushButton;
            //5.添加图片
            button.LargeImage = new BitmapImage(new Uri("pack://application:,,,/CheckingElement.Business;component/Pic/审核.png"));
            return Result.Succeeded; 
        }
    }
}
