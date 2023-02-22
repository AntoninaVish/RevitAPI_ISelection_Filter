using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_ISelection_Filter
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите стены");
            //с помощью new WallFilter  мы указали, что выбирать нужно только стены (собираем элементы, фльтр указали и сообщение "Выберите стены")
            
            var wallList = new List<Wall>();//список стен

            string info = string.Empty; //создаем переменную, пустую строку, которую будем заполнять в процессе выполнения цикла


            foreach (var selectedElement in selectedElementRefList)
            {
                Wall oWall = doc.GetElement(selectedElement) as Wall;//объявляем сразу переменную типом стена и преобразуем в тип стена
                            
               wallList.Add(oWall);//добавляем данную переменную в список стен
                //проверки типов не будет, потому что по умолчанию будут только стены

                //создаем переменную, которая будет показывать толщину стены, вызываем клас UnitUtils который позволяет конвертировать
                //единицы из футов в мм
                var width = UnitUtils.ConvertFromInternalUnits(oWall.Width, UnitTypeId.Millimeters);

                info += $"Name: {oWall.Name}, width: {width} {Environment.NewLine}"; //добавляем эту информацию в переменную info
                                                                                     //и выводим в каждой строке
            }

            info += $"Количество: {wallList.Count}";//добавляем количество


            TaskDialog.Show("Selection", info); //выводим информацию

            return Result.Succeeded;
        }
    }
}
