using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using TimeOrganizer.Tags;
using UnityEngine;

namespace TimeOrganizer.Blocks
{
    public class BlocksManager : TabManager
    {
        private void Awake()
        {   
            // default list is empty because we dont have any default blocks at first start.
            if (PlayerPrefs.GetString("BLOCKS_DATA_LOCAL") != String.Empty)
            {
                List<BlockInfo> tagsToCreate = GetListOfObjects("BLOCKS_DATA_LOCAL", new List<BlockInfo>());
                tagsToCreate.ForEach(obj => CreateBlock(obj, m_content));
            }
        }

        private void CreateBlock(BlockInfo blockInfo, Transform parent)
        {
            GameObject block = Instantiate(m_prefabs.blockPrefab, parent);
            Block blockComp = block.GetComponent<Block>();

            blockComp.Name = blockInfo.Name;
            blockComp.Date = blockInfo.Date;
            
            List<TagInfo> blockTagInfos = GetListOfObjects(blockInfo.TagsKey, new List<TagInfo>());
            for (int i = 0; i < blockTagInfos.Count; i++) { SetTagReferences(blockComp.Tags[i], blockTagInfos[i]); } 
            
            blockComp.Button.OnClick.OnTrigger.Event.AddListener( () => ShowBlockEditionPanel(blockComp) );
            m_objectsHandler.Blocks.Add(blockComp);
        }

        private void ShowBlockEditionPanel(Block blockComp)
        {
            
            GameEventMessage.SendEvent("GoToManageBlocks");
        }

        public void ShowBlockCreationPanel()
        {
            
            GameEventMessage.SendEvent("GoToManageBlocks");
        }

    }
}