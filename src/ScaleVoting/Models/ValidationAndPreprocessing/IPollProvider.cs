using ScaleVoting.Domains;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public interface IPollProvider
    {
        Poll CreatePoll(string title, string content, string[] options);
    }
}