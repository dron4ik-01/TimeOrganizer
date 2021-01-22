using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TimeOrganizer.Tags;
using UnityEngine;
using Zenject;

namespace TimeOrganizer.Tags
{
    public class PrefabPopupManager : MonoBehaviour
    {
        [SerializeField] private UIPopup m_popup;
        public UIPopup Popup => m_popup;
    }
}