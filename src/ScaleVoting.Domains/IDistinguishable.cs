using System;

namespace ScaleVoting.Domains
{
    public interface IDistinguishable
    {
        Guid Guid { get; set; }

        string Title { get; set; }
    }
}