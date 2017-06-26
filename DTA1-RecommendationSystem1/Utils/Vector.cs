using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTA1_RecommendationSystem1.Utils
{
    class Vector
    {
        private List<double> points = new List<double>();

        public Vector()
        {
        }

        public Vector(int size)
        {
            for (int i = 0; i < size; i++)
            {
                points.Add(0);
            }
        }

        public Vector(List<double> points)
        {
            this.points = points;
        }

        public void AddPoint(double point)
        {
            points.Add(point);
        }

        public List<double> getPoints()
        {
            return points;
        }

        public int Size()
        {
            return points.Count();
        }

        public override string ToString()
        {
            return "(" + string.Join(", ", points.Select(i => i.ToString())) + ")";
        }

    }
}
