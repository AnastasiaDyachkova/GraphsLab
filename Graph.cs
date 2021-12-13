using System;
using System.Collections.Generic;
using System.IO;

namespace GraphsLab  
{
    class Graph
    {
        private string fileName="";
        private int pointsCount=0;
        private int linesCount=0;
        private List<Point> points = new List<Point>();
        private List<Line> lines = new List<Line>();
        private bool matrix;

        public Graph(string fileName)
        {
            matrix = IsMatrix(fileName);
            if (!File.Exists(fileName))
            {
                //throw new FileNotFoundException();
                this.fileName = fileName;
            }
            try
            { 
                switch (matrix)
                {
                    case false:
                        this.ReadGraph(fileName);                     
                        break;
                    case true:
                        this.ReadGraphAsMatrix(fileName);
                        break;
                }
                this.fileName = fileName;
                this.linesCount = lines.Count;
                this.pointsCount = points.Count;
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException();
            }
        }
        

        private bool IsMatrix(string fileName)
        {
            string extension = "";
            char[] fNameReverse = fileName.ToCharArray();
            for (int i = fNameReverse.Length - 4; i < fNameReverse.Length; i++)
                extension += fNameReverse[i];
            if (extension == ".lst") 
            {
                return false;
            }
            else if (extension == ".mrx")
                return true;
            else throw new FileLoadException();
        }

        private void ReadGraph(string fileName)
        {
            using (StreamReader file = new StreamReader(fileName)) //Реализует объект TextReader, который считывает символы из потока байтов в определенной кодировке.
            {
                pointsCount = Convert.ToInt32(file.ReadLine());
                for (int i = 0; i < pointsCount; i++)
                    points.Add(new Point(i + 1));     
                while (!file.EndOfStream)
                {
                    string[] currentLine = file.ReadLine().Split(' ');
                    int first, second;
                    double weight;
                    first = Convert.ToInt32(currentLine[0]);
                    second = Convert.ToInt32(currentLine[1]);
                    weight = Convert.ToDouble(currentLine[2]);
                    this.NewLine(new Line(points[first-1], points[second-1], weight));
                    this.points[first - 1].AddLine();
                    this.points[second - 1].AddLine();

                }
            }
        }

        private void ReadGraphAsMatrix(string fileName)
        {
            using (StreamReader file = new StreamReader(fileName))
            {
                pointsCount = Convert.ToInt32(file.ReadLine());
                double[][] matrix = new double[pointsCount][];
                int counter = 0;
                while (!file.EndOfStream)
                {
                    string[] currentLine = System.Text.RegularExpressions.Regex.Split(file.ReadLine(), @"\s{1,}"); //@ - регулярное выражение, где \s - любой пробельный символ
                    double[] matLine = new double[pointsCount];         //Разделяет входную строку в массив подстрок в позициях, определенных соответствием регулярного выражения.
                    for (int i=0;i<currentLine.Length;i++)
                    {
                        matLine[i] = Convert.ToDouble(currentLine[i]);
                    }
                        
                    matrix[counter] = matLine;
                    counter++;
                }
                for (int i = 0; i < counter; i++)
                {
                    this.NewPoint(new Point(i + 1));
                }

                for (int i=0;i<counter;i++)
                {
                    for (int j=0;j<counter;j++)
                    {
                        if (matrix[i][j]!=0)
                        {
                            if(!CheckLineExist(this.points[i], this.points[j]))
                            {
                                this.NewLine(new Line(this.points[i],
                                this.points[j],matrix[i][j]));
                                this.points[i].AddLine();
                                this.points[j].AddLine();
                            }
                           
                        }
                    }
                }
            }
        }

        public bool CheckLineExist(Point f, Point s)
        {
            foreach (Line l in lines)
            {
                if (((l.GetFirst() == f) && (l.GetSecond() == s)) || ((l.GetFirst() == s) && (l.GetSecond() == f)))
                {
                    return true;
                }

            }
            return false;
        }

        public void SaveGraph()
        {
            if (this.matrix)
            {
                SaveGraphAsMatrix();
                return;
            }              
            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(this.pointsCount);
                foreach (Line l in lines)
                {
                    string lineString = l.GetFirst().GetNumber()+" "+l.GetSecond().GetNumber()+" "+l.GetWeight().ToString();
                    file.WriteLine(lineString);
                }
                file.Close();
            }
        }

        private void SaveGraphAsMatrix()
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(this.pointsCount);
                double[,] matrix = new double[pointsCount, pointsCount];
                for (int i = 0; i < GetPointsCount(); i++)
                {
                    for (int j = 0; j < GetPointsCount(); j++)
                    {
                        int vi = i + 1;
                        int vj = j + 1;
                        if (i == j)
                            matrix[i, j] = 0;
                        else
                            matrix[i, j] = GetWeight(vi, vj);
                    }
                }

                for (int i=0;i<pointsCount;i++)
                {
                    for (int j=0;j<pointsCount;j++)
                    {
                        file.Write(matrix[i, j]);
                        if (j != pointsCount - 1)
                            file.Write(' ');
                    }
                    if (i != pointsCount - 1)
                        file.Write('\n');
                }
            }
        }

        public void NewEmptyGraph()
        {
            points.Clear();
            lines.Clear();
            pointsCount = 0;
            linesCount = 0;
        }

        public void NewPoint(Point p)
        {
            points.Add(p);
            pointsCount++;       
        }
        public bool NewLine(Point f, Point s, double w)
        {
            foreach (Line l in lines)
            {
                if(((l.GetFirst() == f) && (l.GetSecond() == s)) ||( (l.GetFirst() == s) && (l.GetSecond() == f)))
                {
                    return false;
                }
                
            }
            if (points.Contains(f) && points.Contains(s) && w!=0)
            {
                lines.Add(new Line(f, s, w));
                linesCount++;
                return true;
            } 
            else return false;

        }

        public void NewLine(Line l) {
           
            if (!lines.Contains(l))
            {
                lines.Add(l);
                linesCount++;
            }            
           
        }

        public bool DeleteLine(Point f, Point s)
        {

            foreach (Line l in lines)
            {
                if (((l.GetFirst() == f) && (l.GetSecond() == s)) || ((l.GetFirst() == s) && (l.GetSecond() == f)))
                {
                    lines.Remove(l);
                    linesCount--;
                    return true;
                }

             }
            return false;

        }


        public int GetPointsCount() {
            return pointsCount;
        }

        public int GetLinesCount()
        {
            return linesCount;
        }

        public string GetPointsListing()
        {
            string toReturn = "";
            foreach (Point p in points)
                toReturn += "Вершина: " + p.GetNumber() + '\n';
            return toReturn;
        }

        public string GetLinesListing()
        {
            string toReturn = "";
            foreach(Line l in lines)
            {
                toReturn += "Ребро: первая вершина = " + l.GetFirst().GetNumber() 
                    + ", вторая вершина = " + l.GetSecond().GetNumber() 
                    + ", вес ребра = " + l.GetWeight()+'\n';
            }
            return toReturn;
        }

        public string GetFileName()
        {
            return fileName;
        }

        public Point GetPoint(int i)
        {
            foreach (Point p in points)
            {
                if (p.GetNumber() == i)
                    return p;
            }
            return null;
        }

        public bool CheckExistance(int first, int second)
        {
            foreach (Line l in lines)
            {
                if (l.GetFirst().GetNumber() == first && l.GetSecond().GetNumber() == second)
                {
                    return true;
                }
            }
            return false;
        }

        public double GetWeight(int first, int second)
        {
            foreach (Line l in lines)
            {
                if ((l.GetFirst().GetNumber() == first && l.GetSecond().GetNumber() == second)|| (l.GetFirst().GetNumber() == second && l.GetSecond().GetNumber() == first))
                {
                    return l.GetWeight();
                }
                
            }
            return 0;
        }

        public bool IsConnectedTo(Point f, Point s)
        {

            foreach (Line l in lines)
            {
                if ((l.GetFirst() == f && l.GetSecond() == s) || (l.GetFirst()== s && l.GetSecond() == f))
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetMathFunction() 
        {
            if (pointsCount < 4)
            {
                return false;
            }
            List<Point> pointsWithDegreeMore3 = new List<Point>(); 

            foreach (Point p in points)
            {
                if (p.GetLinesNumber() >= 3)
                {
                    pointsWithDegreeMore3.Add(p);
                }                
            }
            if (pointsWithDegreeMore3.Count < 4)
            {
                return false;
            }

          
                
            foreach (Point p1 in pointsWithDegreeMore3)
            {
                foreach(Point p2 in pointsWithDegreeMore3)
                {
                    if (!IsConnectedTo(p1, p2))
                        continue;
                    foreach (Point p3 in pointsWithDegreeMore3)
                    {
                        if (!IsConnectedTo(p2, p3) && !IsConnectedTo(p1,p3))
                            continue;
                        foreach (Point p4 in pointsWithDegreeMore3)
                        {
                            if (!IsConnectedTo(p3, p4)&& !IsConnectedTo(p2, p4)&& !IsConnectedTo(p1, p4))
                            continue;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
