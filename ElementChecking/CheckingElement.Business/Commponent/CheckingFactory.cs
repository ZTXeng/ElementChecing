using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Commponent
{
    public static class CheckingFactory
    {
        //检查项目参数的进度计划
        public static bool CheckingProjectInfo(Document doc )
        {
            var info  = doc.ProjectInformation;
            if (info.LookupParameter("进度计划") == null) return false;
            return info.LookupParameter("进度计划").HasValue;
        }

        //检查墙
        public static bool CheckingWall(Element wall)
        {
            var doc = wall.Document;
            //1.检查核心层材质
            var mas = wall.GetMaterialIds(false);
            int count = 0;
            foreach (var item in mas)
            {
                var material = doc.GetElement(item) as Material;

                if (material.Name.Contains("默认")|| material.Name.Contains("按类别")) return false;
                if (material.Color != new Color(232, 233, 231)) return false;
                if (material.Name.Contains("保温")) count++;
            }
            if (count == 0) return false;

            var wallType =doc.GetElement(wall.GetTypeId()) as WallType;
            if (wallType.LookupParameter("防火等级") == null) return false;
            if (string.IsNullOrEmpty(wallType.LookupParameter("防火等级")?.AsString())) return false;

            return true;
        }

        //检查门
        public static bool CheckingDoor(Element door)
        {
            var doc = door.Document;
            //检查框架材质和框架类型
            if (door.LookupParameter("框架材质") == null) return false;
           if(string.IsNullOrEmpty(door.LookupParameter("框架材质").AsString())) return false;
            if (door.LookupParameter("框架类型") == null) return false;
           if(string.IsNullOrEmpty(door.LookupParameter("框架类型").AsString())) return false;
           //检查防火等级
            var type = doc.GetElement(door.GetTypeId());
            if (door.LookupParameter("防火等级").AsString() == null) return false;
            if(string.IsNullOrEmpty(door.LookupParameter("防火等级").AsString())) return false;
            if(!CheckingColor(door, new Color(232, 233, 231))) return false;
            //检查颜色
            return true;
        }

        //检查楼梯
        public static bool CheckingStairs(Element stair)
        {
            var doc = stair.Document;
            //检查楼梯颜色
            if (!CheckingColor(stair, new Color(232, 233, 231))) return false;
            return true;
        }

        //检查梁
        public static bool CheckingBeam(Element beam)
        {
            var doc = beam.Document;
            //检查梁颜色
            if (!CheckingColor(beam, new Color(232, 233, 231))) return false;

            if (!CheckingMaterialDefault(beam)) return false;

            //检查防火等级
            var beamType = doc.GetElement(beam.GetTypeId());
            if (beamType.LookupParameter("防火等级") == null) return false;
            if (string.IsNullOrEmpty(beamType.LookupParameter("防火等级").AsString())) return false;
            return true;
        }

        //检查柱
        public static bool CheckingColumn(Element cloumn)
        {
            var doc = cloumn.Document;

            //检查颜色
            if (!CheckingColor(cloumn, new Color(232, 233, 231))) return false;
            if (!CheckingMaterialDefault(cloumn)) return false;
            return true;
        }

        //检查售票机

        //检查视图过滤器管道颜色
        public static bool CheckingMEPColor(Document doc)
        {
            var view3Ds = doc.OfClass<View3D>();
            foreach (var view3D in view3Ds)
            {
                var filters = view3D.GetFilters();
                foreach (var filterid in filters)
                {
                    var filter = doc.GetElement(filterid) as ParameterFilterElement;
                    var overFilter = view3D.GetFilterOverrides(filterid);
                    var color = overFilter.SurfaceForegroundPatternColor;
                    if (filter.Name.Equals("防排烟") && color != new Color(255, 255, 0)) return false;
                    if (filter.Name.Equals("新风XF") && color != new Color(0, 255, 0)) return false;
                    if (filter.Name.Equals("送风SF") && color != new Color(0, 255, 255)) return false;
                    if (filter.Name.Equals("给水系统") && color != new Color(0, 191, 255)) return false;
                    if (filter.Name.Equals("排水系统") && color != new Color(0,0,255)) return false;
                    if (filter.Name.Equals("通用桥架") && color != new Color(128,128,128)) return false;
                }
            }
            return true;
        }

        //检查风管流量
        public static bool CheckingDuct(Element ele)
        {
            var doc = ele.Document;
            if (ele.LookupParameter("其他流量") == null) return false;
            if(ele.LookupParameter("其他流量").AsDouble()==0) return false ;

            return true;
        }

        //检查元素颜色
        public static bool CheckingColor( Element ele,Color  color)
        {
            var doc = ele.Document;
            var mas = ele.GetMaterialIds(true);
            foreach (var item in mas)
            {
                var material = doc.GetElement(item) as Material;

                if (material.Color != color) return false;
            }
            return true;
        }

        //检查模型材质是否是默认或按类别
        public static bool CheckingMaterialDefault(Element ele)
        {
            var doc = ele.Document;
            //1.检查核心层材质
            var mas = ele.GetMaterialIds(false);
            foreach (var item in mas)
            {
                var material = doc.GetElement(item) as Material;

                if (material.Name.Contains("默认") || material.Name.Contains("按类别")) return false;
            }
            return true;
        }
    }
}
