using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTA1_RecommendationSystem1.Utils
{
    static class RatingsFunctions
    {
        public static Tuple<Vector, Vector> GetCommonRatings(Dictionary<int, double> user1, Dictionary<int, double> user2) {
        
            var user1CommonRattings = new Vector();
            var user2CommonRattings = new Vector();

            foreach (var item in user1.Keys) {
                if (user2.ContainsKey(item)) {
                    user1CommonRattings.AddPoint(user1[item]);
                    user2CommonRattings.AddPoint(user2[item]);
                }
            }
            
            return new Tuple<Vector, Vector>(user1CommonRattings, user2CommonRattings);
        }

        public static Tuple<Vector, Vector> GetCommonRatingsCosine(Dictionary<int, double> user1, Dictionary<int, double> user2)
        {
            var user1CommonRattings = new Vector();
            var user2CommonRattings = new Vector();

            var combinedDictionary = user1.Concat(user2).GroupBy(d => d.Key)
                                        .ToDictionary(d => d.Key, d => d.First().Value);

            foreach (var rating in combinedDictionary)
            {
                if (user1.ContainsKey(rating.Key))
                {
                    user1CommonRattings.AddPoint(user1[rating.Key]);
                    if (user2.ContainsKey(rating.Key)) {
                        user2CommonRattings.AddPoint(user2[rating.Key]);
                    }
                    else {
                        user2CommonRattings.AddPoint(0);
                    }
                }
                else {
                    user1CommonRattings.AddPoint(0);
                    if (user2.ContainsKey(rating.Key))
                    {
                        user2CommonRattings.AddPoint(user2[rating.Key]);
                    }
                    else
                    {
                        user2CommonRattings.AddPoint(0);
                    }
                }
            }

            return new Tuple<Vector, Vector>(user1CommonRattings, user2CommonRattings);
        }
    }
}
