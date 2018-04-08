using System.Collections.Generic;
using ScaleVoting.Domains;

namespace ScaleVoting.Infrastucture
{
    public class DB
    {
        public static List<Poll> Polls = new List<Poll>();
        public static List<PollStat> PollsStat = new List<PollStat>();
    }
}