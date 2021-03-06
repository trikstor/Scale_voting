﻿using System.Collections.Generic;
using ScaleVoting.Domains;

namespace ScaleVoting.Core.ValidationAndPreprocessing
{
    public interface IFieldValidator
    {
        bool FieldIsValid(string field, FieldType fieldType, out string message);
        bool OptionsListIsValid(IEnumerable<Option> options, out string message);
    }
}