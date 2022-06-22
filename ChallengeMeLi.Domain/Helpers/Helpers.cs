using System;
using System.Linq;

namespace ChallengeMeLi.Domain.Helpers
{
    public static class Helpers
    {
        private const string EMPTY_STRING = "";

        // Numeric
        public static decimal RoundWithTwoDecimals(decimal num)
        {
            return Math.Round(num, 2);
        }

        public static decimal CalculateSquared(decimal num)
        {
            return (decimal)Math.Pow((double)num, 2);
        }

        // String
        public static string[] UnionListsWithSameValue(string[] messageA, string[] messageB)
        {
            return messageA.Zip(messageB,
                (itemA, itemB) => itemA.Equals(itemB) ?
                            itemA :
                            GetNotEmpty(itemA, itemB))
                .ToArray();
        }

        public static string GetNotEmpty(string e1, string e2)
        {
            if (!string.IsNullOrEmpty(e1))
            {
                return e1;
            }
            else if (!string.IsNullOrEmpty(e2))
            {
                return e2;
            }

            return EMPTY_STRING;
        }
    }
}