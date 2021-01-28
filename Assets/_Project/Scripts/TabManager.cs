using System;
using System.Collections;
using System.Collections.Generic;
using TimeOrganizer.Tags;
using UnityEngine;
using Zenject;

namespace TimeOrganizer
{
    public abstract class TabManager : MonoBehaviour
    {
        [SerializeField] protected Transform m_content;
        
        [Inject] protected GameInstaller.Prefabs m_prefabs;
        [Inject] protected ObjectsHandler m_objectsHandler;
        [Inject] protected List<TagSprite> m_tagSprites;

        protected List<T> GetListOfObjects<T>(string listKey, List<T> defaultObjects)
        {
            string json = PlayerPrefs.GetString(listKey, ListSerializer.SerializeToJson(defaultObjects));
            List<T> listToCreate = ListSerializer.DeserializeFromJson<T>(json);
            ListSerializer.SaveList(listToCreate, listKey);
            
            return listToCreate;
        }

        protected void SetTagReferences(Tag tagComp, TagInfo tagInfo)
        {
            tagComp.Label = tagInfo.Label;
            tagComp.Color = tagInfo.Color;
            tagComp.Icon = m_tagSprites[tagInfo.SpriteID].sprite;
            tagComp.IconID = tagInfo.SpriteID;
        }
    }
}