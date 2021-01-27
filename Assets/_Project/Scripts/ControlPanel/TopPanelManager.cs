using Doozy.Engine.UI;
using UnityEngine;
using TMPro;

namespace TimeOrganizer.ControlPanel
{
    public class TopPanelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private UIButton m_openManageViewButton;

        public UIButton OpenManageViewButton => m_openManageViewButton;

        public void SetTitleText(string text)
        {
            //TODO: if (text == "Schedule"){ /* set date of active schedule */ }
            m_titleText.text = text;
        }
    
    }
}

