using System.Collections;
using System.Collections.Generic;
using TimeOrganizer.Tags;
using UnityEngine;

namespace TimeOrganizer
{
    public class ObjectsHandler
    {
        public List<Tag> Tags { get; set; }
        
        ObjectsHandler()
        {
            Tags = new List<Tag>();
        }
    }
    
}

