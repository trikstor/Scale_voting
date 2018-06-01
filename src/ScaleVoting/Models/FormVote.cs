using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ScaleVoting.Domains;
using ScaleVoting.Extensions;

namespace ScaleVoting.Models
{
    public class FormVote
    {
        [Required]
        public string JsonOptions { get; set; }

        [Required]
        public Guid PollGuid { get; set; }

        public string CustomOptions { get; set; }

        public Vote ToVote(string username)
        {
            var vote = new Vote
            {
                PollId = PollGuid,
                UserHash = Cryptography.Sha256(username),
                SelectedOptions = JsonConvert.DeserializeObject<List<Guid>>(JsonOptions),
                CustomOptions = JsonConvert.DeserializeObject<Dictionary<Guid, string>>(CustomOptions)
            };
            return vote;
        }
    }
}