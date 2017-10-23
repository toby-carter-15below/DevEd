using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    // A few simple data entity types

    // Good OO would mean we should have references to objects rather than Ids for relationships,
    // but this sort of structure tends to be familiar.

    public class Property
    {
        public int PropertyId { get; set; }
        public int AgentId { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return "PropertyId:" + PropertyId + ", AgentId:" + AgentId + ", Title:" + Title;
        }
    }

    public class Agent
    {
        public int AgentId { get; set; }
        public int? HeadOfficeId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return "AgentId:" + AgentId + ", HeadOfficeId:" + (HeadOfficeId.HasValue ? HeadOfficeId.Value.ToString() : "<null>") + ", Name:" + Name;
        }
    }
}