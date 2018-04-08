using System;

namespace ScaleVoting.Domains
{
    public class PollStat
    {
        public Guid Id { get; }
        public int[] OptionsStat { get; }
        
        public PollStat(Guid id, int optionsQuantity)
        {
            Id = id;
            OptionsStat = new int[optionsQuantity];
        }
    }
}