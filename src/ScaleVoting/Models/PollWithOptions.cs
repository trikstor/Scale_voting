using System.Collections.Generic;

namespace ScaleVoting.Models
{
    public class PollWithOptions
    {
        public Poll Poll { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}