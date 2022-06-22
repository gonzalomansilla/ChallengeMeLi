namespace ChallengeMeLi.Shared.MessageErrors
{
	public static class MessageErrors
	{
		//Validations

		public static readonly string VALIDATION_FIELD_IS_REQUIRED = "{0} is required";
		public static readonly string VALIDATION_MAX_LENGTH = "{0} must be less than {1} characters";
		public static readonly string VALIDATION_MUST_BE_DECIMAL_AND_GREATER_THAN_ZERO = "{0} must be a valid decimal greater than zero";

		#region TopSecret

		public static readonly string TOPSECRET_ONLY_THREE_SATELLITES = "Define only three satellites to triangulate the position correctly";
		public static readonly string TOPSECRET_MESSAGES_ARE_NOT_SAME_LENGTH = "Messages are not the same length";
		public static readonly string TOPSECRET_MESSAGE_CONTAIN_NULL_ELEMENT = "Messages cannot contain null elements";
		public static readonly string TOPSECRET_SATELLITE_NOTFOUND = "The satellite with name: '{0}' not exist";

		#endregion TopSecret

		#region TopSecretSplit

		public static readonly string TOPSECRETSPLIT_DIFERENT_NUMBER_OF_SATELLITES_AND_MESSAGES =
			"The number of satellites '{0}' is not the same as the number of messages '{1}' defined for those satellites";

		public static readonly string TOPSECRETSPLIT_MESSAGES_ARE_NOT_SAME_LENGTH = "The messages are not the same length in all satellites";
		public static readonly string TOPSECRETSPLIT_SATELLITE_DATA_NOTFOUND = "No data associated with the '{0}' satellite was found.";

		#endregion TopSecretSplit
	}
}
