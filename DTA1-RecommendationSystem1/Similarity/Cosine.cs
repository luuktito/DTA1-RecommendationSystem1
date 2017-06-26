using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTA1_RecommendationSystem1.Utils;

namespace DTA1_RecommendationSystem1.Similarity
{
    class Cosine : ISimilarity
    {
        public double CalculateSimilarity(Vector user1, Vector user2)
        {
            var vectorLength = user1.Size();
            var sumProductTotal = 0.0;
            var sumSquareX = 0.0;
            var sumSquareY = 0.0;

            for (var i = 0; i < vectorLength; i++)
            {
                sumProductTotal += (user1.getPoints()[i] * user2.getPoints()[i]);
                sumSquareX += Math.Pow(user1.getPoints()[i], 2);
                sumSquareY += Math.Pow(user2.getPoints()[i], 2);
            }

            var Cosine = sumProductTotal / (Math.Sqrt(sumSquareX) * Math.Sqrt(sumSquareY));
            return Cosine;
        }
    }
}
