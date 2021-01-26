using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TimeOrganizer
{
    public abstract class TabManager : MonoBehaviour
    {
        [SerializeField] protected Transform m_content;
        
        [Inject] protected GameInstaller.Settings m_settings;
        [Inject] protected ObjectsHandler m_objectsHandler;

        protected List<T> GetListOfObjects<T>(string listKey, List<T> defaultObjects)
        {
            string json = PlayerPrefs.GetString(listKey, ListSerializer.SerializeToJson(defaultObjects));
            List<T> listToCreate = ListSerializer.DeserializeFromJson<T>(json);
            ListSerializer.SaveList(listToCreate, listKey);
            
            return listToCreate;
        }
    }
}