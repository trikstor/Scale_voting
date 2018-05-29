namespace ScaleVoting.Core.ValidationAndPreprocessing
{
    public interface IPreprocessing
    {
        string Process(string content);
    }
}