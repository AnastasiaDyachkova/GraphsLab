using System;

namespace GraphsLab
{
    class Point 
    {
        private int number;
        private int linesNumber = 0;


        public Point(int number)
        {
            try
            {    
                this.number = number;
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException();
            }
        }


        public int GetNumber()
        {
            return number;
        }

        public void AddLine()
        {
            linesNumber++;
        }

        public void DeleteLine()
        {
            linesNumber--;
        }

        public int GetLinesNumber()
        {
            return linesNumber;
        }

        public void SetNumber(int number)
        {
            try
            {
                this.number = number;
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException();
            }
        }

        public bool Equals(Point other)
        {
            if (this.GetNumber() == other.GetNumber())
                return true;
            return false;
        }
    }
}
