using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.Nody.Models;
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

        private List<TagInfo> ConvertToTagInfos(List<Tag> tagsToConvert)
        {
            List<TagInfo> tagInfos = new List<TagInfo>();
            foreach (Tag tagComp in tagsToConvert)
            {
                tagInfos.Add(new TagInfo
                {
                    Color = tagComp.Color,
                    Label = tagComp.Label,
                    SpriteID = tagComp.IconID
                });
            }
            return tagInfos;
        }

        private void SaveTags(List<TagInfo> tags)
        {
            PlayerPrefs.SetString("TAGS_DATA_LOCAL", SerializeToJson(tags));
        }

        private void CreateTag(TagInfo tagInfo, Transform parent)
        {
            GameObject tagGO = Instantiate(m_settings.tagPrefab, parent);
            Tag tagComp = tagGO.GetComponent<Tag>();

            tagComp.Label = tagInfo.Label;
            tagComp.Color = tagInfo.Color;
            tagComp.Icon = m_tagSprites[tagInfo.SpriteID].sprite;
            tagComp.IconID = tagInfo.SpriteID;
            tagComp.Button.OnClick.OnTrigger.Event.AddListener(() => ShowTagEditionPanel(tagComp) );
            
            m_objectsHandler.Tags.Add(tagGO.GetComponent<Tag>());
        }

        private void CreateCustomTag()
        {
            TagInfo playerInputTag = new TagInfo
            {
                Label = m_manageTagPanel.InputField.text,
                Color = ColorUtility.ToHtmlStringRGB(m_manageTagPanel.ChooseColor),
                SpriteID = m_manageTagPanel.ChooseIconId
            };

            if (CanSaveTag(playerInputTag) == false) return;
            
            CreateTag(playerInputTag, m_tagsContent);
            SaveTags(ConvertToTagInfos(m_objectsHandler.Tags));
            GameEventMessage.SendEvent("GoToTags");
        }
        
        private bool CanSaveTag(TagInfo playerInputTag)
        {
            bool incorrectInput = string.IsNullOrWhiteSpace(playerInputTag.Label);
            
            bool nameExists = m_objectsHandler.Tags.Exists(t => t.Label == playerInputTag.Label);
            bool alreadyExists = m_manageTagPanel.IsNewTag ? nameExists 
                : playerInputTag.Label != m_manageTagPanel.Item.Label && nameExists;
            
            if (incorrectInput || alreadyExists)
            {
                m_manageTagPanel.InputField.text = "";
                m_manageTagPanel.PlaceholderText.text = incorrectInput ? "Incorrect input" : "Tag already exists";
                return false;
            }
            
            return true;
        }
        
        public void ShowTagCreationPanel()
        {
            m_manageTagPanel.IsNewTag = true;
            
            m_manageTagPanel.Title.text = "New tag";
            m_manageTagPanel.InputField.text = "";
            m_manageTagPanel.ChooseColor = Color.gray;
            m_manageTagPanel.ChooseIconBg.color = Color.gray;
            m_manageTagPanel.DeleteItemButton.gameObject.SetActive(false);
            
            m_manageTagPanel.NextButton.OnClick.OnTrigger.Event.RemoveAllListeners();
            m_manageTagPanel.NextButton.OnClick.OnTrigger.Event.AddListener(CreateCustomTag);
        }
        
        private void ShowTagEditionPanel(Tag tagComp)
        {
            GameEventMessage.SendEvent("GoToManageTag");
            m_manageTagPanel.Item = tagComp;
            m_manageTagPanel.IsNewTag = false;
            
            ColorUtility.TryParseHtmlString( "#" + tagComp.Color, out Color col);
            m_manageTagPanel.ChooseColor = col;
            m_manageTagPanel.ChooseIconBg.color = col;
            m_manageTagPanel.ChooseIcon.sprite = tagComp.Icon;
            m_manageTagPanel.ChooseIconId = tagComp.IconID;
            
            m_manageTagPanel.Title.text = "Edit tag";
            m_manageTagPanel.InputField.text = tagComp.Label;
            m_manageTagPanel.DeleteItemButton.gameObject.SetActive(true);

            m_manageTagPanel.NextButton.OnClick.OnTrigger.Event.RemoveAllListeners();
            m_manageTagPanel.NextButton.OnClick.OnTrigger.Event.AddListener(ApplyChanges);
        }

        private void ApplyChanges()
        {
            TagInfo playerInputTag = new TagInfo
            {
                Label = m_manageTagPanel.InputField.text,
                Color = ColorUtility.ToHtmlStringRGB(m_manageTagPanel.ChooseColor),
                SpriteID = m_manageTagPanel.ChooseIconId
            };
            
            if (CanSaveTag(playerInputTag) == false) return;

            m_manageTagPanel.Item.Label = playerInputTag.Label;
            m_manageTagPanel.Item.Color = playerInputTag.Color;
            m_manageTagPanel.Item.Icon = m_tagSprites[playerInputTag.SpriteID].sprite;
            m_manageTagPanel.Item.IconID = playerInputTag.SpriteID;
            
            SaveTags(ConvertToTagInfos(m_objectsHandler.Tags));
            GameEventMessage.SendEvent("GoToTags");
        }
        
        public void DeleteTag()
        {
            Tag tagItem = m_objectsHandler.Tags.Find(t => t.Label == m_manageTagPanel.Item.Label);
            m_objectsHandler.Tags.Remove(tagItem);
            Debug.Log(tagItem);
            
            Destroy(tagItem.gameObject);
            
            SaveTags(ConvertToTagInfos(m_objectsHandler.Tags));
            GameEventMessage.SendEvent("GoToTags");
        }
}

    public struct ListContainer
    {
        public List<TagInfo> Tags;
        
        public ListContainer(List<TagInfo> tags)
        { Tags = tags; }
    }
}
