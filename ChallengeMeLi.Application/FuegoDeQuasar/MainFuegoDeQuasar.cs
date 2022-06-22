using ChallengeMeLi.Domain.Common;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Domain.Helpers;

using System.Collections.Generic;

namespace ChallengeMeLi.Application.FuegoDeQuasar
{
    public class MainFuegoDeQuasar
    {
        private const string EMPTY_STRING = " ";

        public MainFuegoDeQuasar()
        {
        }

        public MainFuegoDeQuasar(IList<Satellite> satellites)
        {
            Satellites = satellites;
        }

        public IList<Satellite> Satellites { get; set; } = new List<Satellite>();

        public Position GetLocations(decimal distanceA, decimal distanceB, decimal distanceC)
        {
            var coordinateAx = Satellites[0].PosX;
            var coordinateAy = Satellites[0].PosY;
            var coordinateBx = Satellites[1].PosX;
            var coordinateBy = Satellites[1].PosY;
            var coordinateCx = Satellites[2].PosX;
            var coordinateCy = Satellites[2].PosY;

            // Sistema de ecuaciones para determinar la posicicon utilizando el metodo de trilateración
            var A = 2 * coordinateBx - 2 * coordinateAx;
            var B = 2 * coordinateBy - 2 * coordinateAy;
            var C = Helpers.CalculateSquared(distanceA) -
                Helpers.CalculateSquared(distanceB) -
                Helpers.CalculateSquared(coordinateAx) +
                Helpers.CalculateSquared(coordinateBx) -
                Helpers.CalculateSquared(coordinateAy) +
                Helpers.CalculateSquared(coordinateBy);
            var D = 2 * coordinateCx - 2 * coordinateBx;
            var E = 2 * coordinateCy - 2 * coordinateBy;
            var F = Helpers.CalculateSquared(distanceB) -
                Helpers.CalculateSquared(distanceC) -
                Helpers.CalculateSquared(coordinateBx) +
                Helpers.CalculateSquared(coordinateCx) -
                Helpers.CalculateSquared(coordinateBy) +
                Helpers.CalculateSquared(coordinateCy);
            var x = (C * E - F * B) / (E * A - B * D);
            var y = (C * D - A * F) / (B * D - A * E);

            return new Position(x, y);
        }

        public string GetMessage(string[] messageA, string[] messageB, string[] messageC)
        {
            // TODO Make recursive
            var parcialMessage = Helpers.UnionListsWithSameValue(messageA, messageB);
            var finalMessage = Helpers.UnionListsWithSameValue(parcialMessage, messageC);

            return string.Join(EMPTY_STRING, finalMessage);
        }
    }
}