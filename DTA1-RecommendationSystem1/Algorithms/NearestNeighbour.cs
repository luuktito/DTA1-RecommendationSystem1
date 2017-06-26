using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTA1_RecommendationSystem1.Similarity;
using DTA1_RecommendationSystem1.Utils;

namespace DTA1_RecommendationSystem1.Algorithms
{
    class NearestNeighbour
    {

        public Dictionary<int, double> NearestNeightbourResult = new Dictionary<int, double>();

        public NearestNeighbour(Dictionary<int, Dictionary<int, double>> ratings, int userId, string similarityOption, int maxNeighbours, double similarityThreshold) 
        {
            ISimilarity similarityCalculation;

            switch (similarityOption)
            {
                case "pearson":
                    similarityCalculation = new Pearson();
                    break;
                case "euclidean":
                    similarityCalculation = new Euclidean();
                    break;
                case "cosine":
                    similarityCalculation = new Cosine();
                    break;
                default:
                    throw new Exception("Not a supported similarity option");
            }


            foreach (var user in ratings) {
                if (user.Key != userId ) 
                {
                    var commonRatingsUsers = similarityOption == "cosine" 
                        ? RatingsFunctions.GetCommonRatingsCosine(ratings[userId], user.Value) 
                        : RatingsFunctions.GetCommonRatings(ratings[userId], user.Value);

                    var similarityResult = similarityCalculation.CalculateSimilarity(commonRatingsUsers.Item1, commonRatingsUsers.Item2);
                    var differentItemsResult = DifferentItemsForUsers(ratings[userId], user.Value);

                    if (similarityResult > similarityThreshold && differentItemsResult) {
                        if (NearestNeightbourResult.Count() < maxNeighbours) {
                            NearestNeightbourResult.Add(user.Key, similarityResult);
                        }
                        else {
                            var oldMinimumSimilarity = NearestNeightbourResult.Aggregate((l, r) => l.Value < r.Value ? l : r);
                            if (similarityResult > oldMinimumSimilarity.Value) {
                                NearestNeightbourResult.Remove(oldMinimumSimilarity.Key);
                                NearestNeightbourResult.Add(user.Key, similarityResult);
                            }
                        }
                    }
                    if (NearestNeightbourResult.Count() == maxNeighbours)
                    {
                        similarityThreshold = NearestNeightbourResult.Values.Min();
                    }
                }
            }
        }


        private bool DifferentItemsForUsers(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            return user2.Any(x => !user1.Keys.Contains(x.Key));
        }
    }
}
