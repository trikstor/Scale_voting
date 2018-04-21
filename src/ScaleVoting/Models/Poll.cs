using BCClient;

namespace ScaleVoting.Models
{
    public class Poll
    {
        public Question[] Questions { get; }
        public BlockChainStatistics Statistics { get; }
        
        public Poll(Question[] questions)
        {
            Questions = questions;
        }
    }
}