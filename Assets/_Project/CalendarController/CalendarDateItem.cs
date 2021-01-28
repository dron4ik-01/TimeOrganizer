using UnityEngine;
using System.Collections;
using TMPro;

public class CalendarDateItem : MonoBehaviour
{

    [SerializeField] private CalendarController m_calendarController;
    
    public void OnDateItemClick()
    {
        m_calendarController.OnDateItemClick(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
