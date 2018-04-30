using System.Collections.Generic;
using ScaleVoting.Domains;

namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public interface IFieldValidator
    {
        bool FieldIsValid(string field, FieldType fieldType);
        bool OptionsListIsValid(List<string> options);
    }
}