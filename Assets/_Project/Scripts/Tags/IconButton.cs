using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TimeOrganizer.Tags
{
    public class IconButton : MonoBehaviour
    {
        private PrefabPopupManager m_colorButtonManager;
        private Sprite m_sprite;
        void Start()
        {
            m_colorButtonManager = gameObject.GetComponentInParent<PrefabPopupManager>();
            
            gameObject.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(() =>
            {
                ManageTagPanel.s_instance.ChooseIcon.sprite = gameObject.GetComponent<Image>().sprite;
                ManageTagPanel.s_instance.ChooseIconId = transform.GetSiblingIndex();
                m_colorButtonManager.Popup.Hide();
            });
        }
    }
}