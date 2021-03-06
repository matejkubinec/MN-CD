using MNCD.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MNCD.Readers
{
    /// <summary>
    /// Class implementing methods for reading community list.
    /// </summary>
    public class ActorCommunityListReader
    {
        /// <summary>
        /// Reads a community list from a string,
        /// input must be in following format:
        /// actor community.
        /// </summary>
        /// <param name="input">Input community list.</param>
        /// <returns>List of communities.</returns>
        public List<Community> FromString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string must not empty or null.");
            }

            var idCounter = 1;
            var communityMap = new Dictionary<string, Community>();
            var actorMap = new Dictionary<string, Actor>();

            foreach (var row in input.Split('\n'))
            {
                var values = row.Split(' ');

                if (values.Length != 2)
                {
                    throw new ArgumentException("Invalid community list.");
                }

                var actor = GetActor(idCounter, values[0], actorMap);
                var community = GetCommunity(values[1], communityMap);

                community.Actors.Add(actor);
            }

            return communityMap.Values.ToList();
        }

        private Actor GetActor(int idCounter, string actorName, Dictionary<string, Actor> actorMap)
        {
            if (!actorMap.ContainsKey(actorName))
            {
                actorMap.Add(actorName, new Actor(idCounter, actorName));
            }

            return actorMap[actorName];
        }

        private Community GetCommunity(string communityString, Dictionary<string, Community> communityMap)
        {
            if (!communityMap.ContainsKey(communityString))
            {
                communityMap.Add(communityString, new Community());
            }

            return communityMap[communityString];
        }
    }
}