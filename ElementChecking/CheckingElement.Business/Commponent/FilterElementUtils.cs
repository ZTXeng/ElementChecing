using Autodesk.Revit.DB;
using CheckingElement.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Electrical;

namespace CheckingElement.Business.Commponent {
    public static class FilterElementUtils {
        //1.获取建筑墙
        public static RevitCategoryElement GetWall(Document doc) {
            var revitElement = new RevitCategoryElement();

            var walls = doc.OfClass<Wall>().Where(x => {
                var par = x.get_Parameter(BuiltInParameter.WALL_STRUCTURAL_SIGNIFICANT).AsInteger();
                if (par == 0) return true;
                else return false;
            });

            revitElement.Name = "建筑墙";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var wall in walls) {
                elements.Add(new RevitElement() {
                    ElementId = wall.Id.IntegerValue,
                    Name =wall.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);

            return revitElement;
        }
        //2.获取普通门
        public static RevitCategoryElement GetDoor(Document doc) {
            var revitElement = new RevitCategoryElement();

            var cc = Category.GetCategory(doc, BuiltInCategory.OST_Doors);

            var doors = doc.OfClass<FamilyInstance>().Where(x=>x.Category.Id==cc.Id).ToList();

            revitElement.Name = "门";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var door in doors) {
                elements.Add(new RevitElement() {
                    ElementId = door.Id.IntegerValue,
                    Name = door.Symbol.FamilyName+door.Name,
                });
            }

            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;

        }
        //3.获取楼梯
        public static RevitCategoryElement GetStairs(Document doc) {
            var revitElement = new RevitCategoryElement();

            var stairs = doc.OfClass<Stairs>();

            revitElement.Name = "楼梯";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var stair in stairs) {
                elements.Add(new RevitElement() {
                    ElementId = stair.Id.IntegerValue,
                    Name = stair.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //4.获取梁
        public static RevitCategoryElement GetBeam(Document doc) {
            var revitElement = new RevitCategoryElement();

            var beams = doc.OfClass<FamilyInstance>().Where(x => 
                x.Category.Id == Category.GetCategory(doc, BuiltInCategory.OST_StructuralFraming).Id).ToList();

            revitElement.Name = "结构梁";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var beam in beams) {
                elements.Add(new RevitElement() {
                    ElementId = beam.Id.IntegerValue,
                    Name = beam.Symbol.FamilyName +beam.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //5.获取结构柱
        public static RevitCategoryElement GetSColumn(Document doc) {
            var revitElement = new RevitCategoryElement();

            var columns = doc.OfClass<FamilyInstance>().Where(x => 
                x.Category.Id == Category.GetCategory(doc, BuiltInCategory.OST_StructuralColumns).Id 
            );

            revitElement.Name = "结构柱";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var column in columns) {
                elements.Add(new RevitElement() {
                    ElementId = column.Id.IntegerValue,
                    Name = column.Symbol.FamilyName + column.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //6.获取建筑柱
        public static RevitCategoryElement GetColumn(Document doc) {
            var revitElement = new RevitCategoryElement();

            var columns = doc.OfClass<FamilyInstance>().Where(x => 
                x.Category.Id == Category.GetCategory(doc, BuiltInCategory.OST_Columns).Id
            );

            revitElement.Name = "建筑柱";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var column in columns) {
                elements.Add(new RevitElement() {
                    ElementId = column.Id.IntegerValue,
                    Name = column.Symbol.FamilyName+ column.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //7.获取售票机
        public static RevitCategoryElement GetTicketMachine(Document doc) {
            var revitElement = new RevitCategoryElement();

            var tms = doc.OfClass<FamilyInstance>().Where(x => {
                if (x.Category.Id == Category.GetCategory(
                    doc, BuiltInCategory.OST_SpecialityEquipment).Id
                    && x.Symbol.FamilyName.Contains("售票机")
                )
                    return true;
                else return false;
            });

            revitElement.Name = "售票机";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var tm in tms) {
                elements.Add(new RevitElement() {
                    ElementId = tm.Id.IntegerValue,
                    Name = tm.Symbol.FamilyName + tm.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //8.获取管道
        public static RevitCategoryElement GetPipe(Document doc) {
            var revitElement = new RevitCategoryElement();

            var pipes = doc.OfClass<Pipe>();

            revitElement.Name = "管道";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var pipe in pipes) {
                elements.Add(new RevitElement() {
                    ElementId = pipe.Id.IntegerValue,
                    Name = pipe.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //9.获取风管
        public static RevitCategoryElement GetDuct(Document doc) {
            var revitElement = new RevitCategoryElement();

            var ducts = doc.OfClass<Duct>();

            revitElement.Name = "风管";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var duct in ducts) {
                elements.Add(new RevitElement() {
                    ElementId = duct.Id.IntegerValue,
                    Name = duct.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
        //10.获取桥架  
        public static RevitCategoryElement GetCableTray(Document doc)
        {
            var revitElement = new RevitCategoryElement();

            var cableTrays = doc.OfClass<CableTray>();

            revitElement.Name = "桥架";

            var elements = new ObservableCollection<RevitElement>();
            foreach (var cableTray in cableTrays) {
                elements.Add(new RevitElement() {
                    ElementId = cableTray.Id.IntegerValue,
                    Name = cableTray.Name,
                });
            }
            revitElement.Elements = new ObservableCollection<RevitElement>(elements);
            return revitElement;
        }
    }
}
