using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using TimeOrganizer.Tags;
using UnityEngine;
using Zenject;

namespace TimeOrganizer.Tags
{
    public class ColorButtonManager : MonoBehaviour
    {
        [SerializeField] private UIPopup m_tagColorPopup;
        public UIPopup TagColorPopup => m_tagColorPopup;
    }
}