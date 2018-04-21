using BCClient;

namespace BlockChainMachine.Core
{
    public struct Answer : IAnswer
    {
        public string QuestionId { get; }
        public string OptionId { get; }
        public string UserHash { get; }

        public Answer(string questionId, string optionId, string userHash)
        {
            QuestionId = questionId;
            OptionId = questionId;
            UserHash = userHash;
        }
    }
}