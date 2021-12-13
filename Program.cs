using System;

namespace GraphsLab
{
    class Program
    {
        private static Graph current=null;
        static void Main(string[] args)
        {
            IO consoleIO = new IO();
            while(true)
            {
                MainMenu(consoleIO);
            }
        }

        private static void MainMenu(IO io)
        {
            try
            {
                if (current != null)
                {

                    io.PrintLine("", true);
                    io.PrintLine("Имя графа: " + current.GetFileName()
                    + "\nВыберите необходимое действие:"
                    + "\n1. Создать пустой граф;"
                    + "\n2. Вывести все вершины;"
                    + "\n3. Вывести все ребра;"
                    + "\n4. Количество вершин и ребер;"
                    + "\n5. Добавить новую вершину;"
                    + "\n6. Добавить новое ребро;"
                    + "\n7. Удалить ребро;"
                    + "\n8. Вывести вес ребра;"
                    + "\n9. Смежность вершин;"
                    + "\n10. Имеется ли в графе четырехвершинный полный подграф;"
                    + "\n11. Сохранить и выйти;", false);
                    int command = io.PrintLineWithIntegerReading("Ввод: ", false);
                    int first, second;
                    double weight;
                    switch (command)
                    {
                        case 1:
                            current.NewEmptyGraph();
                            io.PrintLineWithKeyHolder("Пустой граф успешно создан" , false);
                            break;
                        case 2:
                            io.PrintLineWithKeyHolder("Вершины: \n"+ current.GetPointsListing(), false);
                            break;
                        case 3:
                            io.PrintLineWithKeyHolder("Ребра: \n" + current.GetLinesListing(), false);
                            break;
                        case 4:
                            io.PrintLineWithKeyHolder("Вершины: " + current.GetPointsCount()
                                + ";\nРебра: " + current.GetLinesCount() + ";", false);
                            break;
                        case 5:
                            io.PrintLineWithKeyHolder("Вершина добавлена", false);
                            current.NewPoint(new Point(current.GetPointsCount() + 1));

                            break;
                        case 6:
                            try
                            {
                                first = io.PrintLineWithIntegerReading("Укажите первую точку: ", false);
                                second = io.PrintLineWithIntegerReading("Укажите вторую точку: ", false);
                                weight = io.PrintLineWithDoubleReading("Введите вес ребра ", false);
                                if (first < 0 || second < 0 || weight < 0)
                                {
                                    io.PrintLineWithKeyHolder("Вершина и вес не могут быть отрицательными", false);
                                    break;
                                }
                                if (current.NewLine(current.GetPoint(first), current.GetPoint(second), weight)){
                                    current.GetPoint(first).AddLine();
                                    current.GetPoint(second).AddLine();
                                }
                                else io.PrintLineWithKeyHolder("Введены неподходящие данные, ребро не создано", false);
                            }
                            catch (Exception e)
                            {
                                io.PrintLineWithKeyHolder(e.Message, false);
                            }
                            break;
                        case 7:
                            first = io.PrintLineWithIntegerReading("Укажите первую точку: ", false);
                            second = io.PrintLineWithIntegerReading("Укажите вторую точку: ", false);
                            if (current.DeleteLine(current.GetPoint(first), current.GetPoint(second)))
                            {
                                current.GetPoint(first).DeleteLine();
                                current.GetPoint(second).DeleteLine();
                                io.PrintLineWithKeyHolder("Успешно удалено",false);
                               
                            }
                            else
                            {
                                io.PrintLineWithKeyHolder("Такого ребра не существует", false);
                            }
                            break;
                        case 8:
                            first = io.PrintLineWithIntegerReading("Укажите первую точку: ", false);
                            second = io.PrintLineWithIntegerReading("Укажите вторую точку: ", false);
                            weight = current.GetWeight(first, second);
                            if (weight > 0)
                                io.PrintLineWithKeyHolder("Вес: " + weight, false);
                            else io.PrintLineWithKeyHolder("Ребро не найдено!", false);
                            break;
                        case 9:
                            first = io.PrintLineWithIntegerReading("Укажите первую точку: ", false);
                            second = io.PrintLineWithIntegerReading("Укажите вторую точку:: ", false);
                            if (current.CheckExistance(first, second)|| current.CheckExistance(second, first))
                                io.PrintLineWithKeyHolder("Вершины смежны", false);
                            else io.PrintLineWithKeyHolder("Вершины НЕ смежны", false);
                            break;

                        case 10:
                            io.PrintLine("Результаты работы алгоритма "
                                + "'Определить, имеется ли в графе с количеством вершин не менее 4-х четырехвершинный полный подграф'"
                                + "\nСтудентка: Дьячкова Анастасия Алексеевна"
                                + "\nГруппа: б2-ИФСТ-32"
                                + "\nДата и время: " + DateTime.Now, false) ;
                            
                            if (current.GetMathFunction())
                            {
                                io.PrintLineWithKeyHolder("Ответ: имеется", false);
                            }
                            else
                            {
                                io.PrintLineWithKeyHolder("Ответ: не имеется", false);
                            }
                            break;
                        case 11:
                            current.SaveGraph();
                            current = null;
                            io.PrintLineWithKeyHolder("Файл сохранен", false);
                            break;
                    }
                }
                else
                {
                    io.PrintLine("", true);
                    io.PrintLine("Добро пожаловать в GraphsLab!"
                        + "\nВыберите необходимое действие:"
                        + "\n1. Создать новый граф;"
                        + "\n2. Открыть существующий граф;"
                        + "\n3. О программе;"
                        + "\n4. Выход;", false);
                    int command = io.PrintLineWithIntegerReading("Ввод: ", false);
                    string fileName;
                    switch (command)
                    {
                        case 1:
                            fileName = io.PrintLineWithStringReading("Введите полное название файла с необходимым форматом(.lst - в виде списков ребер, .mrx - в виде матрицы)", false);
                            current = new Graph(fileName);
                            break;
                        case 2:
                            try
                            {
                                fileName = io.PrintLineWithStringReading("Введите путь к файлу, который хотите открыть: ", false);
                                current = new Graph(fileName);
                            }
                            catch (Exception e)
                            {
                                io.PrintLineWithKeyHolder(e.Message, false);
                            }
                            break;
                        case 3:
                            io.PrintLineWithKeyHolder("GraphsLab 1.0.0"
                                + "\nАвтор: Дьячкова Анастасия, б2-ИФСТ-32, СГТУ им. Гагарина"
                                + "\n(c) 2021"
                                + "\nНажмите любую клавишу для продолжения...", true);
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            io.PrintLineWithKeyHolder("Ошибка! Неизвестная команда, нажмите любую клавишу для продолжения и выберите функцию из возможных ", true);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                io.PrintLineWithKeyHolder("Ошибка: "+e.Message, false);
            }
          
  
        }
    }
}
