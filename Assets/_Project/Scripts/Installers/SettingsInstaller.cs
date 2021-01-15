using System;
using UnityEngine;
using Zenject;
using TimeOrganizer.ControlPanel;

namespace TimeOrganizer
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "ScriptableObjects/SettingsInstaller", order = 1)]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public ControlPanelManager.ControlPanelSettings controlPanel;

        public override void InstallBindings()
        {
            Container.BindInstance(controlPanel);
        }

    }
}