namespace ScaleVoting.Models.ValidationAndPreprocessing
{
    public interface IFieldValidator
    {
        bool FieldIsValid(string field, FieldType fieldType);
        bool OptionsListIsValid(string[] options);
    }
}