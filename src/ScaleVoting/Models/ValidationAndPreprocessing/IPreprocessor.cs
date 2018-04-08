namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public interface IPreprocessor
    {
        string ProcessField(string content);
    }
}