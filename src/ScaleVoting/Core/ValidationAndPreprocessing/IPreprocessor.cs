namespace ScaleVoting.Core.ValidationAndPreprocessing
{
    public interface IPreprocessor
    {
        string ProcessField(string content);
    }
}