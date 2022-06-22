using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Controllers.V1.TopSecretSplit.Post
{
    [ExcludeFromCodeCoverage]
    public class PostTopSecretSplitBodyRequest
    {
        public string Distance { get; set; }
        public IList<string> Message { get; set; }
    }
}