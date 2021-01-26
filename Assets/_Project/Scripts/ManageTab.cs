using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;

namespace TimeOrganizer
{
    public abstract class ManageTab : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI m_title;
        [SerializeField] protected UIButton m_deleteItem;
        [SerializeField] protected UIButton m_nextButton;

        public TextMeshProUGUI Title => m_title;
        public UIButton DeleteItemButton => m_deleteItem;
        public UIButton NextButton => m_nextButton;
        public bool IsNewTab { get; set; }

    }
}