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
        
        public bool OptionsListIsValid(string[] options)
        {
            return options.Length > 2 &&
                   options.Length < 50 &&
                   options.All(option => FieldIsValid(option, FieldType.Option));
        }
    }
}