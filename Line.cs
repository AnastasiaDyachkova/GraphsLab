using System;

namespace GraphsLab
{
    class Line
    {
        private Point first;
        private Point second;
        private double weight=0;

        public Line(Point first, Point second, double weight)
        {
            try
            {
                this.first = first;
                this.second = second;
                this.weight = weight;
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException();
            }
        }
        public Line(Point first, Point second)
        {
            try
            {
                this.first = first;
                this.second = second;
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                    throw new ArgumentNullException();
            }
        }
        public double GetWeight()
        {
            return weight;
        }
        public Point GetFirst()
        {
            return first;
        }
        public Point GetSecond()
        {
            return second;
        }

        public bool Equals(Line other)
        {
            if (this.GetFirst() == other.GetFirst()
                && this.GetSecond() == other.GetSecond()
                && this.GetWeight() == other.GetWeight())
                return true;
            return false;
        }
    }
}
