using UnityEngine;
using TMPro;

namespace TimeOrganizer.ControlPanel
{
    public class TopPanelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_titleText;
        
        public void SetTitleText(string text)
        {
            // if (text == "Schedule"){ /* set date of active schedule */ }
            m_titleText.text = text;
        }
    
    }
}

