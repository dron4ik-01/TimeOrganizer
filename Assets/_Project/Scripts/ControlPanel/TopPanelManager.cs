using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using TimeOrganizer.ControlPanel;
using TMPro;
using Zenject;

namespace TimeOrganizer.ControlPanel
{
    public class TopPanelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private UIButton m_settingsButton;

        // [SerializeField] private UIPopup m_settingsPopup;
        private void Start()
        {
            m_settingsButton.OnClick.OnTrigger.Event.AddListener(() =>
            {
                // m_popup.Show();
            });
        }

        public void SetTitleText(string text)
        { m_titleText.text = text; }
    
    }
}

