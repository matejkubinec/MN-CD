using System.Collections.Generic;
using MNCD.Evaluation.MultiLayer;
using MNCD.Core;
using Xunit;

namespace MNCD.Tests.Evaluation.MultiLayer
{
    public class ExclusivityTests
    {
        [Fact]
        public void ExclusivityEqualsOne()
        {
            var actors = new List<Actor>
            {
                new Actor(1, "a1"),
                new Actor(2, "a2"),
                new Actor(3, "a3"),
                new Actor(4, "a4")
            };
            var network = new Network
            {
                Actors = actors,
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Edges = new List<Edge>
                        {
                            new Edge(actors[0], actors[1])
                        }
                    },
                    new Layer
                    {
                        Edges = new List<Edge>
                        {
                            new Edge(actors[2], actors[3])
                        }
                    }
                }
            };
            var community = new Community(actors);

            Assert.Equal(1, Exclusivity.Compute(community, network));
        }

        [Fact]
        public void ExclusivityEqualsZero()
        {
            var actors = new List<Actor>
            {
                new Actor(1, "a1"),
                new Actor(2, "a2")
            };
            var network = new Network
            {
                Actors = actors,
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Edges = new List<Edge>
                        {
                            new Edge(actors[0], actors[1])
                        }
                    },
                    new Layer
                    {
                        Edges = new List<Edge>
                        {
                            new Edge(actors[0], actors[1])
                        }
                    }
                }
            };
            var community = new Community(actors);

            Assert.Equal(0, Exclusivity.Compute(community, network));
        }
    }
}