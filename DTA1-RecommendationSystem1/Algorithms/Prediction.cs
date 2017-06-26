using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTA1_RecommendationSystem1.Algorithms
{
    class Prediction
    {
        public static double PredictRating(Dictionary<int, Dictionary<int, double>> ratings, Dictionary<int, double> nearestNeighbours, int userId, int itemId) 
        {
            double sumSimilarities = 0;
            double sumRatingsSimilarities = 0;

            foreach (var neighbour in nearestNeighbours) {
                if (ratings[neighbour.Key].ContainsKey(itemId)) {
                    sumRatingsSimilarities += (ratings[neighbour.Key][itemId] * neighbour.Value);
                    sumSimilarities += neighbour.Value;
                }
            }

            double predictedRating = sumRatingsSimilarities / sumSimilarities;

            return predictedRating;
        }

        public static Dictionary<int, double> GetTopPredictions(Dictionary<int, Dictionary<int, double>> ratings, Dictionary<int, double> nearestNeighbours, int userId, int minRatings, int topAmount) 
        {
            Dictionary<int, double> predictedRatingsUser = new Dictionary<int, double>();
            List<int> allItems = new List<int>();

            foreach (var user in ratings)
            {
                foreach (var itemId in user.Value.Keys)
                {
                    if (!allItems.Contains(itemId))
                        allItems.Add(itemId);
                }
            }

            foreach (var itemId in allItems)
            {
                var amountOfRatingsByNeighbours = 0;
                if (minRatings > 0)
                {
                    foreach (var user in nearestNeighbours.Keys)
                    {
                        if (ratings[user].ContainsKey(itemId))
                            amountOfRatingsByNeighbours += 1;
                    }
                }

                if ((amountOfRatingsByNeighbours >= minRatings) || (minRatings < 1))
                    predictedRatingsUser.Add(itemId, PredictRating(ratings, nearestNeighbours, 186, itemId));
            }

            var orderedPredictedRatingsUser = predictedRatingsUser.OrderByDescending(x => x.Value).Take(topAmount).ToDictionary(x => x.Key, x => x.Value);

            return orderedPredictedRatingsUser;
        }
    }
}
