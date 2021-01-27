using System;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;
using Zenject;

namespace TimeOrganizer.ControlPanel
{
    public class ControlButton : MonoBehaviour
    {
        [SerializeField] private Image m_icon;
        
        [Inject] private ControlPanelManager m_ctrlPanel;
        [Inject] private TopPanelManager m_topPanelManager;
        [Inject] private GameInstaller.GameEvents m_gameEvents;
        private UIButton m_button;

        private void Awake()
        {
            m_button = gameObject.GetComponent<UIButton>();
        }

        private void Start()
        {
            if (this == m_ctrlPanel.CurrentActiveButton) SetActiveButton();
            else SetInActiveButton();
            
            m_button.OnClick.OnTrigger.Event.AddListener(() =>
            {
                SetActiveButton();
                m_topPanelManager.SetTitleText(m_button.ButtonName);
                SwitchManageViewButton(m_topPanelManager.OpenManageViewButton);
            });
            
        }

        private void SetActiveButton()
        {
            m_button.Button.interactable = false;
            
            m_ctrlPanel.CurrentActiveButton.SetInActiveButton();
            m_ctrlPanel.CurrentActiveButton = this;

            m_icon.color = m_ctrlPanel.ActiveButtonColor;
            m_button.TextMeshProLabel.color = m_ctrlPanel.ActiveButtonColor;
        }

        private void SetInActiveButton()
        {
            m_button.Button.interactable = true;

            m_icon.color = m_ctrlPanel.InactiveButtonColor;
            m_button.TextMeshProLabel.color = m_ctrlPanel.InactiveButtonColor;
        }

        private void SwitchManageViewButton(UIButton viewButton)
        {
            viewButton.OnClick.OnTrigger.Event.RemoveAllListeners();
            
            switch (m_button.ButtonName)
            {
                case "Tags":
                    viewButton.OnClick.OnTrigger.Event.AddListener(m_gameEvents.openManageTagPanel.Raise);
                    break;
                case "Blocks":
                    viewButton.OnClick.OnTrigger.Event.AddListener(m_gameEvents.openManageBlocksPanel.Raise);
                    break;
                case "Routines":
                    viewButton.OnClick.OnTrigger.Event.AddListener(m_gameEvents.openManageRoutinesPanel.Raise);
                    break;
            }
        }
    }

}
