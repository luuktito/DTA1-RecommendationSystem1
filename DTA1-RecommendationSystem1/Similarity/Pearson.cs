using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTA1_RecommendationSystem1.Utils;

namespace DTA1_RecommendationSystem1.Similarity
{
    class Pearson : ISimilarity
    {
        public double CalculateSimilarity(Vector user1, Vector user2)
        {
            var vectorLength = user1.Size();
            var sumX = 0.0;
            var sumY = 0.0;
            var squareSumX = 0.0;
            var squareSumY = 0.0;
            var sumTotal = 0.0;

            for (var i = 0; i < vectorLength; i++)
            {
                sumX += user1.getPoints()[i];
                sumY += user2.getPoints()[i];
                squareSumX += Math.Pow(user1.getPoints()[i], 2);
                squareSumY += Math.Pow(user2.getPoints()[i], 2);
                sumTotal += user1.getPoints()[i] * user2.getPoints()[i];
            }

            var cov = sumTotal - (sumX * sumY / vectorLength);
            var stdX = Math.Sqrt((squareSumX - (Math.Pow(sumX, 2) / vectorLength)));
            var stdY = Math.Sqrt((squareSumY - (Math.Pow(sumY, 2) / vectorLength)));

            return cov / (stdX * stdY);
        }
    }
}
