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
        [SerializeField] private Image m_chooseColorBackground;
        [SerializeField] private Image m_chooseIconBackground;
        [SerializeField] private Image m_chooseIcon;

        public TextMeshProUGUI Title => m_title;
        public UIButton DeleteItemButton => m_deleteItem;
        public TMP_InputField InputField => m_inputField;

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
        public Image ChooseIcon
        {
            get => m_chooseIcon;
            set => m_chooseIcon = value;
        }

        public Image ChooseIconBg
        {
            get => m_chooseIconBackground;
            set => m_chooseIconBackground = value;
        }

        private void Start() => s_instance = this; 
    }
}