using Autodesk.Revit.DB;
using CheckingElement.Business.Commponent;
using CheckingElement.Business.Model;
using CheckingElement.Business.View;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckingElement.Business.ViewModel {
    public class CheckingElementViewModel : ViewModelBase<CheckingElementModel> {

        public IAsyncRelayCommand Loaded { get; set; }
        public IAsyncRelayCommand ProfessionChanged { get; set; }
        /// <summary>
        /// 打勾操作
        /// </summary>
        public IAsyncRelayCommand<int> CheckElement { get; set; }
        /// <summary>
        /// 审查操作
        /// </summary>
        public IAsyncRelayCommand Check { get; set; }
        public IAsyncRelayCommand Export { get; set; }
        public IAsyncRelayCommand Close { get; set; }


        Document _doc;
        public CheckingElementViewModel(Document doc) {

            Loaded = new AsyncRelayCommand(OnLoaded);
            ProfessionChanged = new AsyncRelayCommand(OnProfessionChanged);
            CheckElement = new AsyncRelayCommand<int>(OnCheckElement);
            Check = new AsyncRelayCommand(OnCheck);
            Export = new AsyncRelayCommand(OnExport);

            Model = new CheckingElementModel();
            _doc = doc;
        }
        /// <summary>
        /// 测试构造，不要doc
        /// </summary>
        public CheckingElementViewModel() {

            Loaded = new AsyncRelayCommand(OnLoaded);
            ProfessionChanged = new AsyncRelayCommand(OnProfessionChanged);
            CheckElement = new AsyncRelayCommand<int>(OnCheckElement);
            Check = new AsyncRelayCommand(OnCheck);
            Export = new AsyncRelayCommand(OnExport);

            Model = new CheckingElementModel();
        }

        private Task OnExport() {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\data.xlsx";
            if (checkResult == null) {
                MessageBox.Show("请先进行审核再导出结果");
                return Task.CompletedTask;
            }
            var exporter = new ExcelUtils(checkResult);

            exporter.ExportData(path);
            return Task.CompletedTask;
        }

        IList<RevitElement> checkResult = null;

        private Task OnCheck() {
            var elems = Model.Elements.SelectMany(x => x.Elements).Where(x => x.IsChecked).ToList();

            checkResult = new List<RevitElement>(CheckLogic(elems));
            var checkResultView = new CheckingResult(checkResult);
            checkResultView.ShowDialog();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 你的审核逻辑写在这儿
        /// </summary>
        /// <param name="selectedElems"></param>
        /// <returns></returns>
        private IList<RevitElement> CheckLogic(IList<RevitElement> selectedElems) {
            var res = new List<RevitElement>();

            foreach(var categoryReveits in Model.Elements)
            {
                if (categoryReveits.IsChecked == true)
                {
                    if (categoryReveits.Name == "建筑墙") {
                        foreach (var element in categoryReveits.Elements)
                        {
                           if(!CheckingFactory.CheckingWall(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "门")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingDoor(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "楼梯")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingStairs(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "结构梁")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingBeam(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "结构柱")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingColumn(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "风管")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingDuct(_doc.GetElement(new ElementId(element.ElementId)))) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "管道")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingMEPColor(_doc)) res.Add(element);
                        }
                    }

                    if (categoryReveits.Name == "桥架")
                    {
                        foreach (var element in categoryReveits.Elements)
                        {
                            if (!CheckingFactory.CheckingMEPColor(_doc)) res.Add(element);
                        }
                    }
                }
            }

            return selectedElems;
        }



        private Task OnCheckElement(int id) {
            if (id > 0) {
                var currrent = Model.Elements.FirstOrDefault(x => x.Id == id);
                if (currrent != null) {
                    currrent.Elements.ToList().ForEach(x => {
                        x.IsChecked = currrent.IsChecked;
                    });
                }
            }
            return Task.CompletedTask;
        }

        private Task OnProfessionChanged() {
            switch (Model.SelectedProfession) {
                case Profession.ARCHITECTURE:
                    CollectArchitectureElements();
                    break;
                case Profession.STRUCTURE:
                    CollectStruuctureElements();
                    break;
                case Profession.PLUMBING:
                    CollectPlumbingElements();
                    break;
                case Profession.ELECTRICAL:
                    CollectElectricalElements();
                    break;
                case Profession.HVAC:
                    CollectHVACElements();
                    break;
                default:
                    Model.Elements.Clear();
                    break;
            }
            return Task.CompletedTask;
        }

        private void CollectArchitectureElements() {
            Model.Elements.Clear();
            var wall = FilterElementUtils.GetWall(_doc);
            wall.Id = 100;

            var door = FilterElementUtils.GetDoor(_doc);
            door.Id = 200;


            Model.Elements.Add(wall);
            Model.Elements.Add(door);
            //Model.Elements.Add(window);
        }
      
        private void CollectStruuctureElements() {
            Model.Elements.Clear();

            var beam = FilterElementUtils.GetBeam(_doc);
            beam.Id = 400;
            Model.Elements.Add(beam);

            var scolumn = FilterElementUtils.GetSColumn(_doc);
            scolumn.Id = 500;
            Model.Elements.Add(scolumn);

            var stairs = FilterElementUtils.GetStairs(_doc);
            stairs.Id = 501;
            Model.Elements.Add(stairs);

        }

        private void CollectPlumbingElements() {
            Model.Elements.Clear();
            var pipe = FilterElementUtils.GetPipe(_doc);
            pipe.Id = 600;
            Model.Elements.Add(pipe);
        }

        private void CollectElectricalElements() {
            Model.Elements.Clear();

            var cableTray = FilterElementUtils.GetCableTray(_doc);
            cableTray.Id = 800;
            Model.Elements.Add(cableTray);
        }

        private void CollectHVACElements() {
            Model.Elements.Clear();

            var duct = FilterElementUtils.GetDuct(_doc);
            duct.Id = 700;
            Model.Elements.Add(duct);

        }

        private Task OnLoaded() {
            var professions = Enum.GetValues(typeof(Profession)).Cast<Profession>().ToList();
            professions.ForEach(x => {
                Model.Professions.Add(x);
            });
            return Task.CompletedTask;
        }
    }
}
