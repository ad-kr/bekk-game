using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public class FactionValidator
    {
        private struct FactionPair
        {
            public Faction FirstFaction { get; set; }
            public Faction SecondFaction { get; set; }
            public FactionRelation Relation { get; set; }

            public FactionPair(Faction firstFaction, Faction secondFaction, FactionRelation relation)
            {
                FirstFaction = firstFaction;
                SecondFaction = secondFaction;
                Relation = relation;
            }
        }

        private static readonly List<FactionPair> _relations = new()
        {
            new FactionPair(Faction.Human, Faction.Robot, FactionRelation.Hostile)
        };

        public static FactionRelation GetRelation(Faction first, Faction second)
        {
            foreach (FactionPair pair in _relations)
            {
                bool doFactionsMatch = (pair.FirstFaction == first && pair.SecondFaction == second)
                                    || (pair.FirstFaction == second && pair.SecondFaction == first);
                if (doFactionsMatch) return pair.Relation;
            }
            return FactionRelation.None;
        }
    }

    public enum Faction
    {
        Human, Robot
    }

    public enum FactionRelation
    {
        Neutral, Friendly, Hostile, None
    }
}