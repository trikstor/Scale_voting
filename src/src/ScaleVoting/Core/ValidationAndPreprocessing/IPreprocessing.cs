namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public interface IPreprocessing
    {
        string Process(string content);
    }
}