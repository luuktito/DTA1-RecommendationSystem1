using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTA1_RecommendationSystem1.Utils;

namespace DTA1_RecommendationSystem1.Similarity
{
    interface ISimilarity
    {
        double CalculateSimilarity(Vector user1, Vector user2);
    }
}
