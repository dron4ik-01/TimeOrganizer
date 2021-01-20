using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TimeOrganizer.Tags
{
    public class TagsManager : MonoBehaviour
    {
        [SerializeField] private Transform m_tagsContent;

        [Inject] private GameInstaller.Settings m_settings;
        [Inject] private ObjectsHandler m_objectsHandler;
        [Inject] private List<TagInfo> m_defaultTags;
        [Inject] private List<TagSprite> m_tagSprites;
        
        private void Awake()
        {
             Init();
        }
        
        private void Init()
        {
            string tagsJson = PlayerPrefs.GetString("TAGS_DATA_LOCAL", SerializeToJson(m_defaultTags));
            List<TagInfo> tagsToCreate = DeserializeFromJson(tagsJson);
            SaveTags(tagsToCreate);
            
            tagsToCreate.ForEach(t =>  CreateTag(t, m_tagsContent));
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_tagsContent.GetComponent<RectTransform>());
        }

        private string SerializeToJson(List<TagInfo> tags)
        {
            // TODO: Wrap this in try/catch to handle deserialization exceptions
            ListContainer container = new ListContainer(tags);
            string json = JsonUtility.ToJson(container);
            
            return json;
        }
        
        List<TagInfo> DeserializeFromJson(string json)
        {
            // TODO: Wrap this in try/catch to handle deserialization exceptions
            ListContainer container = JsonUtility.FromJson<ListContainer>(json);
            
            return container.Tags;
        }

        private void SaveTags(List<TagInfo> tags)
        {
            ListContainer container = new ListContainer(tags);
            string json = JsonUtility.ToJson(container);
            
            PlayerPrefs.SetString("TAGS_DATA_LOCAL", json);
        }
        
        public void CreateTag(TagInfo tagInfo , Transform parent)
        {
            GameObject tagGO = Instantiate(m_settings.tagPrefab, parent);
            Tag tagComp = tagGO.GetComponent<Tag>();
            
            tagComp.Label = tagInfo.Label;
            tagComp.Color = tagInfo.Color;
            tagComp.Icon = m_tagSprites[tagInfo.SpriteID].sprite;
            
            m_objectsHandler.Tags.Add(tagGO.GetComponent<Tag>());
        }
        
    }

    public struct ListContainer
    {
        public List<TagInfo> Tags;
        
        public ListContainer(List<TagInfo> tags)
        { Tags = tags; }
    }
}
