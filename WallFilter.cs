using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_ISelection_Filter
{
    public class WallFilter : ISelectionFilter //объявляем класс публичным и реализуем его ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Wall; //фильтруем только стены
            //таким образом если элемент в фильтре является стеной, то будет возвращаться значение истинно
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
