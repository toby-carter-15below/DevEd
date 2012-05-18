using System;
using System.Collections.Generic;
using System.Linq;

namespace LYNC
{
    public static class SampleData
    {
        public static readonly List<Property> PropertyList = new List<Property>() {
                new Property(){PropertyId = 1, AgentId = 1, Title = "first house is agent 1"},
                new Property(){PropertyId = 2, AgentId = 1, Title = "second house is agent 1"},
                new Property(){PropertyId = 3, AgentId = 2, Title = "third house is agent 2"}};

        public static readonly List<Agent> AgentList = new List<Agent>() {
                new Agent(){AgentId = 1, HeadOfficeId = null, Name = "first agent"},
                new Agent(){AgentId = 2, HeadOfficeId = 1, Name = "second agent is a branch"},
                new Agent(){AgentId = 3, HeadOfficeId = null, Name = "third agent stands alone"}};

        public static readonly IEnumerable<int> IntList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
