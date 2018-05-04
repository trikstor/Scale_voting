using System.Collections.Generic;
using System.Linq;
using ScaleVoting.Core.ValidationAndPreprocessing;
using ScaleVoting.Domains;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public class FieldValidator : IFieldValidator
    {
        public bool FieldIsValid(string field, FieldType fieldType)
        {
            if (field.Length < 3)
            {
                return false;
            }
            if (fieldType == FieldType.Option && field.Length > 30)
            {
                return false;
            }
            if (fieldType == FieldType.Title && field.Length > 20)
            {
                return false;
            }
            if (fieldType == FieldType.Content && field.Length > 100)
            {
                return false;
            }
            return true;
        }
        
        public bool OptionsListIsValid(IEnumerable<Option> options)
        {
            var optionsList = options.ToList();

            return optionsList.Count >= 2 &&
                   optionsList.Count <= 50 &&
                   optionsList.All(option => FieldIsValid(option.OptionContent, FieldType.Option));
        }
    }
}