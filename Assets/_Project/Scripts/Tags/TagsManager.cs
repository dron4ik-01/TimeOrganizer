using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [Inject] private ManageTagPanel m_manageTagPanel;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            string tagsJson = PlayerPrefs.GetString("TAGS_DATA_LOCAL", SerializeToJson(m_defaultTags));
            List<TagInfo> tagsToCreate = DeserializeFromJson(tagsJson);
            tagsToCreate.ForEach(tagInfo => CreateTag(tagInfo, m_tagsContent));
            SaveTags(tagsToCreate);
        }

        private string SerializeToJson(List<TagInfo> tags)
        {
            // TODO: Wrap this in try/catch to handle serialization exceptions
            ListContainer container = new ListContainer(tags);
            string json = JsonUtility.ToJson(container);

            return json;
        }

        private List<TagInfo> DeserializeFromJson(string json)
        {
            // TODO: Wrap this in try/catch to handle deserialization exceptions
            ListContainer container = JsonUtility.FromJson<ListContainer>(json);

            return container.Tags;
        }

        private List<TagInfo> ConvertToTagInfos(List<Tag> tags)
        {
            List<TagInfo> tagInfos = new List<TagInfo>();
            foreach (Tag tagComp in tags)
            {
                TagInfo tagInfo = new TagInfo
                {
                    Color = tagComp.Color,
                    Label = tagComp.Label,
                    // BUG: Incorrect finding of image ID.
                    SpriteID = m_tagSprites.FindIndex(sprite => tagComp.Icon)
                };

                tagInfos.Add(tagInfo);
            }

            return tagInfos;
        }

        private void SaveTags(List<TagInfo> tags)
        {
            ListContainer container = new ListContainer(tags);
            string json = JsonUtility.ToJson(container);

            PlayerPrefs.SetString("TAGS_DATA_LOCAL", json);
        }

        private void CreateTag(TagInfo tagInfo, Transform parent)
        {
            GameObject tagGO = Instantiate(m_settings.tagPrefab, parent);
            Tag tagComp = tagGO.GetComponent<Tag>();

            tagComp.Label = tagInfo.Label;
            tagComp.Color = tagInfo.Color;
            tagComp.Icon = m_tagSprites[tagInfo.SpriteID].sprite;

            m_objectsHandler.Tags.Add(tagGO.GetComponent<Tag>());
        }

        public void CreateCustomTag()
        {
            // BUG: All tags after reload has equal background and icons.
            TagInfo playerInputTag = new TagInfo
            {
                Label = m_manageTagPanel.InputField.text,
                Color = ColorUtility.ToHtmlStringRGBA(m_manageTagPanel.ChooseColor),
                SpriteID = m_tagSprites.FindIndex(sprite => m_manageTagPanel.ChooseIcon)
            };

            bool incorrectInput = playerInputTag.Label == "" || m_objectsHandler.Tags.Exists(t => t.Label == playerInputTag.Label);
            if (incorrectInput)
            {
                m_manageTagPanel.PlaceholderText.text = "Incorrect input";
            }
            else
            {
                CreateTag(playerInputTag, m_tagsContent);
                SaveTags(ConvertToTagInfos(m_objectsHandler.Tags));
            }
        }

        public void DeleteTag()
        {
            
        }
        
        public void ShowTagCreationPanel()
        {
            m_manageTagPanel.Title.text = "New tag";
            m_manageTagPanel.DeleteItemButton.gameObject.SetActive(false);
        }

        public void ShowTagEditionPanel()
        {
            m_manageTagPanel.Title.text = "Edit tag";
            m_manageTagPanel.DeleteItemButton.gameObject.SetActive(true);
        }
        

}

    public struct ListContainer
    {
        public List<TagInfo> Tags;
        
        public ListContainer(List<TagInfo> tags)
        { Tags = tags; }
    }
}
