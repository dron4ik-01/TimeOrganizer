using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeOrganizer.Tags
{
    public class ManageTagPanel : MonoBehaviour
    {
        public static ManageTagPanel s_instance;

        [SerializeField] private TextMeshProUGUI m_title;
        [SerializeField] private TMP_InputField m_inputField;
        [SerializeField] private TextMeshProUGUI m_placeholderText;
        [SerializeField] private UIButton m_deleteItem;
        [SerializeField] private UIButton m_nextButton;
        [SerializeField] private Image m_chooseColorBackground;
        [SerializeField] private Image m_chooseIconBackground;
        [SerializeField] private Image m_chooseIcon;

        private Tag m_item;
        
        public int ChooseIconId { get; set; }
        public bool IsNewTag { get; set; }
        public TextMeshProUGUI Title => m_title;
        public UIButton DeleteItemButton => m_deleteItem;
        public UIButton NextButton => m_nextButton;
        public TMP_InputField InputField => m_inputField;
        
        public Image ChooseIcon
        {
            get => m_chooseIcon;
            set => m_chooseIcon = value;
        }
        public TextMeshProUGUI PlaceholderText
        {
            get => m_placeholderText;
            set => m_placeholderText = value;
        }
        public Color ChooseColor
        {
            get => m_chooseColorBackground.color;
            set => m_chooseColorBackground.color = value;
        }
        public Image ChooseIconBg
        {
            get => m_chooseIconBackground;
            set => m_chooseIconBackground = value;
        }

        public Tag Item
        {
            get => m_item;
            set => m_item = value;
        }

        private void Start() => s_instance = this; 
    }
}