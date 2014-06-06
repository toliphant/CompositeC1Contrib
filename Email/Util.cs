﻿using System.Linq;

using Composite.C1Console.Events;
using Composite.C1Console.Security;

namespace CompositeC1Contrib.Email
{
    public class Util
    {
        public static void UpdateParents(string entityToken, string consoleId)
        {
            var deserializedEntityToken = EntityTokenSerializer.Deserialize(entityToken);
            var graph = new RelationshipGraph(deserializedEntityToken, RelationshipGraphSearchOption.Both);

            if (graph.Levels.Count() <= 1)
            {
                return;
            }

            var level = graph.Levels.ElementAt(1);
            foreach (var token in level.AllEntities)
            {
                var consoleMessageQueueItem = new RefreshTreeMessageQueueItem
                {
                    EntityToken = token
                };

                ConsoleMessageQueueFacade.Enqueue(consoleMessageQueueItem, consoleId);
            }
        }
    }
}