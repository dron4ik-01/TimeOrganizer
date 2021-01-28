using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TimeOrganizer.Tags;
using UnityEngine;

namespace TimeOrganizer.Blocks
{
    public abstract class BlockBase : MonoBehaviour
    {
        [SerializeField] private List<Tag> m_tags;
        private UIButton m_button;
        
        private void Awake()
        { m_button = gameObject.GetComponent<UIButton>(); }

        public List<Tag> Tags
        {
            get => m_tags;
            set => m_tags = value;
        }

        public UIButton Button => m_button;
    }
    
    public enum BlockType
    {
        Block,
        Occupation
    }
}