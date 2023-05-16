using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Commponent
{
    public static class FasterFiltered
    {
        public static IEnumerable<Ttype> OfClass<Ttype>(this Document doc){
            var eles = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfClass(typeof(Ttype))
                  .Cast<Ttype>();
            return eles;
        }
    }
}
