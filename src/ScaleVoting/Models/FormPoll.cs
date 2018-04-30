using System.ComponentModel.DataAnnotations;
using ScaleVoting.Domains;

namespace ScaleVoting.Models
{
    public class FormPoll
    {
        public string Title { get; set; }
        public string  JsonQuestions { get; set; }
    }
}