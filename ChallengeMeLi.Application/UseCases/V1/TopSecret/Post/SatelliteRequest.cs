using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecret.Post
{
    [ExcludeFromCodeCoverage]
    public class SatelliteRequest
    {
        private string _name { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                var nameLowercase = value?.ToLower();
                _name = nameLowercase;
            }
        }

        public string Distance { get; set; }
        public IList<string> Message { get; set; }
    }
}