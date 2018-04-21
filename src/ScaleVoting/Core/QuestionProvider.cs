using System;
using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Models;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Core
{
    public class QuestionProvider : IQuestionProvider
    {
        private IPreprocessor Preprocessor { get; }
        private IFieldValidator Validator { get; }

        public QuestionProvider(IPreprocessor preprocessor, IFieldValidator validator)
        {
            Preprocessor = preprocessor;
            Validator = validator;
        }

        public Question CreatePoll(string userName, string title, string content, IEnumerable<string> options)
        {
            try
            {
                /*
                if (Validator.FieldIsValid(title, FieldType.Title) &&
                    Validator.FieldIsValid(content, FieldType.Content) &&
                    Validator.OptionsListIsValid(options))
                {
                */
                var id = Guid.NewGuid();
                var poll = new Question
                {
                    UserName = userName,
                    Title = Preprocessor.ProcessField(title),
                    Content = Preprocessor.ProcessField(content),
                    Id = id
                };
                var processedOptions = options.Select(opt => new Option
                {
                    PollId = id,
                    PollOption = opt
                }).ToList();
                return new Question {UserName = userName, Title = title, Content = content, Options = processedOptions};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default(Question);
        }
    }
}