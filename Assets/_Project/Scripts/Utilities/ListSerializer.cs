using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListSerializer
{
    public static string SerializeToJson<T>(List<T> list)
    {
        try 
        {   
            ListContainer<T> container = new ListContainer<T>(list);
            string json = JsonUtility.ToJson(container);
            return json;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public static List<T> DeserializeFromJson<T>(string json)
    {
        try
        {
            ListContainer<T> container = JsonUtility.FromJson<ListContainer<T>>(json);
            return container.List;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
    
    public static void SaveList<T>(List<T> list, string key)
    { 
        PlayerPrefs.SetString(key, SerializeToJson(list));
    }
    
}

public struct ListContainer<T>
{
    public List<T> List;
        
    public ListContainer(List<T> list)
    { List = list; }
}
