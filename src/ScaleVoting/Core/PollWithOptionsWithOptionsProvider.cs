using System;
using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Models;
using ScaleVoting.Models.ValidationAndPreprocessing;

namespace ScaleVoting.Core
{
    public class PollWithOptionsWithOptionsProvider : IPollWithOptionsProvider
    {
        private IPreprocessor Preprocessor { get; }
        private IFieldValidator Validator { get; }

        public PollWithOptionsWithOptionsProvider(IPreprocessor preprocessor, IFieldValidator validator)
        {
            Preprocessor = preprocessor;
            Validator = validator;
        }

        public PollWithOptions CreatePoll(string userName, string title, string content, string[] options)
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
                var poll = new Poll
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
                return new PollWithOptions { Poll = poll, Options = processedOptions };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default(PollWithOptions);
        }
    }
}