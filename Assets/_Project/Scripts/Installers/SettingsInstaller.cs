using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TimeOrganizer.ControlPanel;
using TimeOrganizer.Tags;

namespace TimeOrganizer
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "ScriptableObjects/SettingsInstaller", order = 1)]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private ControlPanelManager.ControlPanelSettings m_controlPanel;
        [SerializeField] private List<TagInfo> m_defaultTags;
        [SerializeField] private List<TagSprite> m_tagSprites;
        [SerializeField] private GameInstaller.Prefabs m_prefabs;
        [SerializeField] private GameInstaller.GameEvents m_gameEvents;
        
        public override void InstallBindings()
        {
            Container.BindInstance(m_controlPanel);
            Container.BindInstance(m_defaultTags);
            Container.BindInstance(m_prefabs);
            Container.BindInstance(m_tagSprites);
            Container.BindInstance(m_gameEvents);
        }
        
        
        
    }
}