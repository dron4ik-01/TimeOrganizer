using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  Zenject;

namespace TimeOrganizer.ControlPanel
{
    public class ControlPanelManager
    {
        private ControlButton m_currentActiveButton;
        private Color m_activeColor;
        private Color m_inactiveColor;
        
        public ControlPanelManager
            (ControlButton currentActiveButton, ControlPanelSettings ctrlPanelSettings)
        {
            m_currentActiveButton = currentActiveButton;
            m_activeColor = ctrlPanelSettings.activeButtonColor;
            m_inactiveColor = ctrlPanelSettings.inactiveButtonColor;
        }
        
        public ControlButton CurrentActiveButton { get => m_currentActiveButton; set => m_currentActiveButton = value; }
        public Color ActiveButtonColor => m_activeColor;
        public Color InactiveButtonColor => m_inactiveColor;

        [Serializable]
        public class ControlPanelSettings
        {
            public Color activeButtonColor = Color.white;
            public Color inactiveButtonColor = Color.black;
        }
        
    }

}