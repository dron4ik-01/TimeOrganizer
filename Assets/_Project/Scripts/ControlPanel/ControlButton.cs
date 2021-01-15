using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace TimeOrganizer.ControlPanel
{
    public class ControlButton : MonoBehaviour
    {
        [SerializeField] private Image m_icon;

        private UIButton m_button;
        [Inject] private ControlPanelManager m_ctrlPanel;
        [Inject] private TopPanelManager m_topPanelManager;
        private void Start()
        {
            m_button = gameObject.GetComponent<UIButton>();
            if (this == m_ctrlPanel.CurrentActiveButton) Show();
            else Hide();
            
            m_button.OnClick.OnTrigger.Event.AddListener(() =>
            {
                Show();
                m_topPanelManager.SetTitleText(m_button.ButtonName);
            });
            
        }

        private void Show()
        {
            m_button.Button.interactable = false;
            
            m_ctrlPanel.CurrentActiveButton.Hide();
            m_ctrlPanel.CurrentActiveButton = this;

            m_icon.color = m_ctrlPanel.ActiveButtonColor;
            m_button.TextMeshProLabel.color = m_ctrlPanel.ActiveButtonColor;
        }

        private void Hide()
        {
            m_button.Button.interactable = true;

            m_icon.color = m_ctrlPanel.InactiveButtonColor;
            m_button.TextMeshProLabel.color = m_ctrlPanel.InactiveButtonColor;
        }
    }

}
