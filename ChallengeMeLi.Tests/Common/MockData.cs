using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Domain.Entities;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Tests.Common
{
	[ExcludeFromCodeCoverage]
	public static class MockData
	{
		public const string NAME_SATELLITE1 = "satellite1";
		public const string NAME_SATELLITE2 = "satellite2";
		public const string NAME_SATELLITE3 = "satellite3";
		public const decimal DISTANCE_SATELLITE1_REQ = 100m;
		public const decimal DISTANCE_SATELLITE2_REQ = 120m;
		public const decimal DISTANCE_SATELLITE3_REQ = 125m;
		public const decimal POS_X_SATELLITE1_DB = 100m;
		public const decimal POS_Y_SATELLITE1_DB = 100m;
		public const decimal POS_X_SATELLITE2_DB = 120m;
		public const decimal POS_Y_SATELLITE2_DB = -100m;
		public const decimal POS_X_SATELLITE3_DB = -50m;
		public const decimal POS_Y_SATELLITE3_DB = 50m;
		public const decimal POS_X_RESULT = 66.53m;
		public const decimal POS_Y_RESULT = 6.65m;

		// Messages
		public static readonly IList<string> Message1 = new List<string>()
		{
			"Mensaje", "super", EMPTY_STRING, "de", EMPTY_STRING, "nave", EMPTY_STRING
		};

		public static readonly string FINAL_MESSAGE = "Mensaje super secreto de la nave imperial.";

		public static readonly IList<string> Message2 = new List<string>()
		{
			EMPTY_STRING, "super", EMPTY_STRING, "de", "la", EMPTY_STRING, EMPTY_STRING
		};

		public static readonly IList<string> Message3 = new List<string>()
		{
			"Mensaje", EMPTY_STRING, "secreto", EMPTY_STRING, EMPTY_STRING, EMPTY_STRING, "imperial."
		};

		public static readonly IList<string> MessageCwithNull = new List<string>()
		{
			"Mensaje", EMPTY_STRING, "secreto", EMPTY_STRING, EMPTY_STRING, null, "imperial."
		};

		public static readonly IList<SatelliteRequest> SatellitesRequestOk = new List<SatelliteRequest>()
		{
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE1,
				Distance = DISTANCE_SATELLITE1_REQ.ToString(),
				Message = Message1,
			},
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE2,
				Distance = DISTANCE_SATELLITE2_REQ.ToString(),
				Message = Message2
			},
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE3,
				Distance = DISTANCE_SATELLITE3_REQ.ToString(),
				Message = Message3
			}
		};

		public static readonly IList<SatelliteRequest> SatellitesInDifferentOrderRequestOk = new List<SatelliteRequest>()
		{
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE3,
				Distance = DISTANCE_SATELLITE3_REQ.ToString(),
				Message = Message3
			},
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE1,
				Distance = DISTANCE_SATELLITE1_REQ.ToString(),
				Message = Message1,
			},
			new SatelliteRequest()
			{
				Name = NAME_SATELLITE2,
				Distance = DISTANCE_SATELLITE2_REQ.ToString(),
				Message = Message2
			},
		};

		public static readonly IList<Satellite> SatellitesDB = new List<Satellite>()
		{
			new Satellite {Name = NAME_SATELLITE1, PosX = POS_X_SATELLITE1_DB, PosY = POS_Y_SATELLITE1_DB},
			new Satellite {Name = NAME_SATELLITE2, PosX = POS_X_SATELLITE2_DB, PosY = POS_Y_SATELLITE2_DB},
			new Satellite {Name = NAME_SATELLITE3, PosX = POS_X_SATELLITE3_DB, PosY = POS_Y_SATELLITE3_DB}
		};

		public static readonly SatelliteRequest Satellite1RequestOk = new SatelliteRequest()
		{
			Name = NAME_SATELLITE1,
			Distance = DISTANCE_SATELLITE1_REQ.ToString(),
			Message = Message1,
		};

		private const string EMPTY_STRING = "";
		public static IList<Word> WordsMessage1 { get; } = GenerateWordsForMessage(Message1, 1, Message1.Count);
		public static IList<Word> WordsMessage2 { get; } = GenerateWordsForMessage(Message2, Message1.Count + 1, Message2.Count * 2);
		public static IList<Word> WordsMessage3 { get; } = GenerateWordsForMessage(Message3, Message2.Count * 2 + 1, Message3.Count * 3);

		private static IList<Word> GenerateWordsForMessage(IList<string> message, int wordIdMin, int wordIdMax)
		{
			// Logica para crear los id de la entidad Word tomando como referencia el largo de un mensaje

			// TODO agregar Message.
			var words = new List<Word>();
			var position = 1;
			var indexMessage = 0;

			for (int i = wordIdMin; i <= wordIdMax; i++)
			{
				words.Add(new Word { WordId = i, Text = message[indexMessage], Position = position });
				position += 1;
				indexMessage += 1;
			}

			return words;
		}
	}
}
