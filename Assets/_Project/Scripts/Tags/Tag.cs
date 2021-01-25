using System;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TimeOrganizer.Tags
{
    public class Tag : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_label;
        [SerializeField] private Image m_backgroundImage;
        [SerializeField] private Image m_iconImage;
        [SerializeField] private UIButton m_button;

        private int m_iconID;

        public string Label
        {
            get => m_label.text;
            set => m_label.text = value;
        }

        public string Color
        {
            get => ColorUtility.ToHtmlStringRGBA(m_backgroundImage.color);
            set
            {
                ColorUtility.TryParseHtmlString("#" + value, out var col);
                m_backgroundImage.color = col;
            }
        }

        public Sprite Icon
        {
            get => m_iconImage.sprite;
            set => m_iconImage.sprite = value;
        }

        public int IconID
        {
            get => m_iconID;
            set => m_iconID = value;
        }

        public UIButton Button
        {
            get => m_button;
            set => m_button = value;
        }

        public GameObject TagObject
        {
            get => gameObject;
            set => value = gameObject;
        }
    }
    
    [Serializable] public class TagInfo
    {
        public string Label;
        public string Color;
        public int SpriteID;
    }
    
    [Serializable] public class TagSprite
    {
        public Sprite sprite;
    }
}
