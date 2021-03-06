﻿using System;
using DTA1_RecommendationSystem1.Similarity;
using DTA1_RecommendationSystem1.Utils;
using DTA1_RecommendationSystem1.Algorithms;
using System.Collections.Generic;
using System.Linq;

namespace DTA1_RecommendationSystem1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#Assignment 1: User-Item ");

            var ratings = Parser.Parser.Parse(',', "userItem.data");

            var commonRatingsUser3And4 = RatingsFunctions.GetCommonRatings(ratings[3], ratings[4]);
            var pearsonCalculation = new Pearson();
            var pearsonSimilarity3And4 = pearsonCalculation.CalculateSimilarity(commonRatingsUser3And4.Item1, commonRatingsUser3And4.Item2);
            Console.WriteLine("Pearson Similarity between users 3 and 4: " + pearsonSimilarity3And4 + "\n");


            Console.WriteLine("#Nearest neighbours and similarities for user 7 (using pearson): ");
            var nearestNeighbourUser7Pearson = new NearestNeighbour(ratings, 7, "pearson", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Pearson.NearestNeightbourResult) {
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);
            }
            Console.WriteLine();

            Console.WriteLine("#Nearest neighbours and similarities for user 7 (using euclidean): ");
            var nearestNeighbourUser7Euclidean = new NearestNeighbour(ratings, 7, "euclidean", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Euclidean.NearestNeightbourResult)
            {
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);
            }

            Console.WriteLine();
            Console.WriteLine("#Nearest neighbours and similarities for user 7 (using cosine): ");
            var nearestNeighbourUser7Cosine = new NearestNeighbour(ratings, 7, "cosine", 3, 0.35);
            foreach (var nearestNeighbor in nearestNeighbourUser7Cosine.NearestNeightbourResult)
            {
                Console.WriteLine("User " + nearestNeighbor.Key + " = Similarity: " + nearestNeighbor.Value);
            }
            Console.WriteLine();


            Console.WriteLine("#Predicted ratings for user 7 for the following items:");
            var predictedRatingUser7item101 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 101);
            Console.WriteLine("Item 101 has a predicted rating of: " + predictedRatingUser7item101);

            var predictedRatingUser7item103 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 103);
            Console.WriteLine("Item 103 has a predicted rating of: " + predictedRatingUser7item103);

            var predictedRatingUser7item106 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 106);
            Console.WriteLine("Item 106 has a predicted rating of: " + predictedRatingUser7item106);
            Console.WriteLine();

            Console.WriteLine("#Predicted ratings for user 5 for the following item:");
            var nearestNeighbourUser4Pearson = new NearestNeighbour(ratings, 4, "pearson", 3, 0.35);
            var predictedRatingUser4item101 = Prediction.PredictRating(ratings, nearestNeighbourUser4Pearson.NearestNeightbourResult, 4, 101);
            Console.WriteLine("User 4, Item 101 has a predicted rating of: " + predictedRatingUser4item101);
            Console.WriteLine();


            ratings[7][106] = 2.8;
            Console.WriteLine("#Predicted ratings for user 7 for the following item (with the updated rating for item 106):");
            nearestNeighbourUser7Pearson = new NearestNeighbour(ratings, 7, "pearson", 3, 0.35);
            predictedRatingUser7item101 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 101);
            Console.WriteLine("User 7, Item 101 has a predicted rating of: " + predictedRatingUser7item101);

            predictedRatingUser7item103 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 103);
            Console.WriteLine("User 7, Item 103 has a predicted rating of: " + predictedRatingUser7item103);
            Console.WriteLine();

            ratings[7][106] = 5;
            Console.WriteLine("#Predicted ratings for user 7 for the following item (with the updated rating for item 106):");
            nearestNeighbourUser7Pearson = new NearestNeighbour(ratings, 7, "pearson", 3, 0.35);
            predictedRatingUser7item101 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 101);
            Console.WriteLine("User 7, Item 101 has a predicted rating of: " + predictedRatingUser7item101);

            predictedRatingUser7item103 = Prediction.PredictRating(ratings, nearestNeighbourUser7Pearson.NearestNeightbourResult, 7, 103);
            Console.WriteLine("User 7, Item 103 has a predicted rating of: " + predictedRatingUser7item103);
            Console.WriteLine();


            var ratingsMovie = Parser.Parser.Parse('\t', "u.data");
            var nearestNeighbourUser186Pearson = new NearestNeighbour(ratingsMovie, 186, "pearson", 25, 0.35);

            Console.WriteLine("#The top 8 predicted ratings for user 186 are as follows:");
            var orderedPredictedRatingsUser186 = Prediction.GetTopPredictions(ratingsMovie, nearestNeighbourUser186Pearson.NearestNeightbourResult, 186, 0, 8);
            foreach (var movie in orderedPredictedRatingsUser186)
            {
                Console.WriteLine("Movie ID " + movie.Key + " has a predicted rating of: " + movie.Value);
            }
            Console.WriteLine();

            Console.WriteLine("#The top 8 predicted ratings with a minimum of 3 neighbour ratings for user 186 are as follows:");
            orderedPredictedRatingsUser186 = Prediction.GetTopPredictions(ratingsMovie, nearestNeighbourUser186Pearson.NearestNeightbourResult, 186, 3, 8);
            foreach (var movie in orderedPredictedRatingsUser186)
            {
                Console.WriteLine("Movie ID " + movie.Key + " has a predicted rating of: " + movie.Value);
            }


            Console.ReadLine();
        }
    }
}
