using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TimeOrganizer.Tags;
using TMPro;
using UnityEngine;

namespace TimeOrganizer.Blocks
{
    public class Block : BlockBase
    {
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_date;

        private BlockType m_blockType = BlockType.Block;
        
        public string Name
        {
            get => m_name.text;
            set => m_name.text = value;
        } 
        public string Date 
        {
            get => m_date.text;
            set => m_date.text = value;
        }
        
        public BlockType Type => m_blockType;
    }

    public class BlockInfo
    {
        public string Name;
        public string Date;
        public string TagsKey;
    }
}