using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScaleVoting.Domains
{
    public class Vote
    {
        public Guid PollId { get; set; }

        [JsonIgnore]
        public string UserHash { get; set; }

        public IList<Guid> SelectedOptions { get; set; }
        public IDictionary<Guid, string> CustomOptions { get; set; }

        public Vote()
        {
            SelectedOptions = new List<Guid>();
            CustomOptions = new Dictionary<Guid, string>();
        }
    }
}