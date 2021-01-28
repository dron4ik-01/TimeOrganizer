using System.Collections;
using System.Collections.Generic;
using TimeOrganizer.Blocks;
using TimeOrganizer.Tags;
using UnityEngine;

namespace TimeOrganizer
{
    public class ObjectsHandler
    {
        public List<Tag> Tags { get; set; }
        public List<Block> Blocks { get; set; }
        
        ObjectsHandler()
        {
            Tags = new List<Tag>();
            Blocks = new List<Block>();
        }
    }
    
}

