using System.Collections.Generic;

namespace ScaleVoting.Core.ValidationAndPreprocessing
{
    public class FieldPreprocessor : IPreprocessor
    {
        private IEnumerable<IPreprocessing> Preprocessors { get; }

        public FieldPreprocessor(IEnumerable<IPreprocessing> preprocessors)
        {
            Preprocessors = preprocessors;
        }

        public string ProcessField(string content)
        {
            var resultString = content;

            foreach (var preprocessor in Preprocessors)
            {
                resultString = preprocessor.Process(content);
            }

            return resultString;
        }
    }
}