using System.Collections;
using System.Collections.Generic;
using TimeOrganizer.ControlPanel;
using UnityEngine;
using UnityEngine.UI;
using  Zenject;

namespace TimeOrganizer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Control Panel dependencies")]
        [SerializeField] private TopPanelManager m_topPanelManager;
        [SerializeField] private ControlButton m_firstActiveButton;
        public override void InstallBindings()
        {
            InstallControlPanel();
        }

        private void InstallControlPanel()
        {
            Container.Bind<ControlPanelManager>().AsSingle()
                .WithArguments(m_firstActiveButton);

            Container.Bind<TopPanelManager>().FromInstance(m_topPanelManager);
        }
        
    }
}
