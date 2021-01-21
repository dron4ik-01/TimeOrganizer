using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TimeOrganizer.Tags
{
    public class ColorButton : MonoBehaviour
    {
        private ColorButtonManager m_colorButtonManager;
        private Color m_color;
        void Start()
        {
            m_colorButtonManager = gameObject.GetComponentInParent<ColorButtonManager>();
            m_color = gameObject.GetComponent<Image>().color;
            
            gameObject.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(() =>
            {
                ManageTagPanel.s_instance.ChooseColor = m_color;
                ManageTagPanel.s_instance.ChooseIconBg.color = m_color;
                m_colorButtonManager.TagColorPopup.Hide();
            });
        }

    }
}