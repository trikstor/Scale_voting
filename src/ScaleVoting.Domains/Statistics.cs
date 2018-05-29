﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ScaleVoting.Domains
{
    public class Statistics
    {
        public Poll Poll { get; }
        public Dictionary<Guid, int> OptionCountStat { get; }
        public Dictionary<Guid, int> QuestionCountStat { get; }

        public Statistics(Poll poll)
        {
            Poll = poll;
            OptionCountStat = GetOptionCountStat();
            QuestionCountStat = GetQuestionCountStat();
        }

        private Dictionary<Guid, int> GetQuestionCountStat()
        {
            var result = new Dictionary<Guid, int>();
            foreach (var question in Poll.Questions)
            {
                result[question.Guid] = 0;
                foreach (var option in question.Options)
                {
                    result[question.Guid] += OptionCountStat[option.Guid];
                }
            }

            return result;
        }

        private Dictionary<Guid, int> GetOptionCountStat()
        {
            var result = new Dictionary<Guid, int>();
            foreach (var question in Poll.Questions)
            {
                foreach (var option in question.Options)
                {
                    result[option.Guid] = GetOptionCount(option.Guid);
                }
            }

            return result;
        }

        private int GetOptionCount(Guid guid)
        {
            return Poll.Votes.SelectMany(vote => vote.SelectedOptions).
                Count(option => option == guid);
        }
    }
}