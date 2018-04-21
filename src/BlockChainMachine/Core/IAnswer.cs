namespace BCClient
{
    public interface IAnswer
    {
        string QuestionId { get; }
        string OptionId { get; }
        string UserHash { get; }
    }
}