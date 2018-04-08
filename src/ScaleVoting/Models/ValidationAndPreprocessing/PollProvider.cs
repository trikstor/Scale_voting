using System;
using ScaleVoting.Domains;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public class PollProvider : IPollProvider
    {
        private IPreprocessor Preprocessor { get; }
        private IFieldValidator Validator { get; }

        public PollProvider(IPreprocessor preprocessor, IFieldValidator validator)
        {
            Preprocessor = preprocessor;
            Validator = validator;
        }

        public Poll CreatePoll(string title, string content, string[] options)
        {
            try
            {
                if (Validator.FieldIsValid(title, FieldType.Title) &&
                    Validator.FieldIsValid(content, FieldType.Content) &&
                    Validator.OptionsListIsValid(options))
                {
                    return new Poll(
                        Preprocessor.ProcessField(title),
                        Preprocessor.ProcessField(content),
                        options);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default(Poll);
        }
    }
}