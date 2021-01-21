using System;
using TimeOrganizer.ControlPanel;
using TimeOrganizer.Tags;
using UnityEngine;
using  Zenject;

namespace TimeOrganizer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Control Panel dependencies")] 
        [SerializeField] private TopPanelManager m_topPanelManager;
        [SerializeField] private ControlButton m_firstActiveButton;

        [Header("Managers instances")] 
        [SerializeField] private TagsManager m_tagsManager;
        [SerializeField] private ManageTagPanel m_manageTagPanel;
        public override void InstallBindings()
        {
            ControlPanelInstalls();
            
            Container.Bind<ObjectsHandler>().AsSingle();
            Container.Bind<TagsManager>().FromInstance(m_tagsManager).AsSingle();
            Container.Bind<ManageTagPanel>().FromInstance(m_manageTagPanel).AsSingle();
        }

        private void ControlPanelInstalls()
        {
            Container.Bind<ControlPanelManager>().AsSingle().WithArguments(m_firstActiveButton);
            Container.Bind<TopPanelManager>().FromInstance(m_topPanelManager).AsSingle();
        }
        
        [Serializable] public class Settings
        {
            public GameObject tagPrefab;

        }
    }
}
