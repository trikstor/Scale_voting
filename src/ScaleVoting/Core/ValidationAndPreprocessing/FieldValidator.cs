using System.Collections.Generic;
using System.Linq;

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
        
        public bool OptionsListIsValid(List<string> options)
        {
            return options.Count > 2 &&
                   options.Count < 50 &&
                   options.All(option => FieldIsValid(option, FieldType.Option));
        }
    }
}