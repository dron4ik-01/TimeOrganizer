using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;
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
            
            if (this == m_ctrlPanel.CurrentActiveButton) SetActiveButton();
            else SetInActiveButton();
            
            m_button.OnClick.OnTrigger.Event.AddListener(() =>
            {
                SetActiveButton();
                m_topPanelManager.SetTitleText(m_button.ButtonName);
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
    }

}
