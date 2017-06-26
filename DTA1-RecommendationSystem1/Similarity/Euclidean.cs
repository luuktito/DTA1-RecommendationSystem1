using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTA1_RecommendationSystem1.Utils;

namespace DTA1_RecommendationSystem1.Similarity
{
    class Euclidean : ISimilarity
    {
        public double CalculateSimilarity(Vector user1, Vector user2)
        {
            var vectorLength = user1.Size();
            var squareSumTotal = 0.0;

            for (var i = 0; i < vectorLength; i++)
            {
                squareSumTotal += Math.Pow(user1.getPoints()[i] - user2.getPoints()[i], 2);
            }

            var euclideanDistance = Math.Sqrt(squareSumTotal);
            return 1 / (1 + euclideanDistance);
        }
    }
}
