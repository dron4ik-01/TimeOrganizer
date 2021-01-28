using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    [SerializeField] private GameObject m_calendarPanel;
    [SerializeField] private TextMeshProUGUI m_yearNumText;
    [SerializeField] private TextMeshProUGUI m_monthText;

    [SerializeField] private GameObject m_item;

    private List<GameObject> m_dateItems = new List<GameObject>();
    private const int m_totalDateNum = 42;

    private DateTime m_dateTime;
    private TextMeshProUGUI m_target;
    
    private void Awake()
    {
        // TODO : Make new positioning system (?based on width)
        Vector3 startPos = m_item.transform.localPosition;
        m_dateItems.Clear();
        m_dateItems.Add(m_item);

        for (int i = 1; i < m_totalDateNum; i++)
        {
            GameObject item = Instantiate(m_item, m_item.transform.parent, true);
            item.name = "Item" + (i + 1).ToString();
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            m_dateItems.Add(item);
        }

        m_dateTime = DateTime.Now;

        CreateCalendar();

        m_calendarPanel.SetActive(false);
    }

    private void CreateCalendar()
    {
        DateTime firstDay = m_dateTime.AddDays(-(m_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < m_totalDateNum; i++)
        {
            TextMeshProUGUI label = m_dateItems[i].GetComponentInChildren<TextMeshProUGUI>();
            m_dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    m_dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();
                    date++;
                }
            }
        }
        m_yearNumText.text = m_dateTime.Year.ToString();
        m_monthText.text = GetMonth(m_dateTime.Month);
    }

    private int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }

    private string GetMonth(int month)
    {
        switch (month)
        {
            case 1 : return "January"; case 2 : return "February";
            case 3 : return "March"; case 4 : return "April";
            case 5 : return "May"; case 6 : return "June";
            case 7 : return "July"; case 8 : return "August";
            case 9 : return "September"; case 10 : return "October";
            case 11 : return "November"; case 12 : return "December";
        }
        return "";
    }
    
    public void YearPrev()
    {
        m_dateTime = m_dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        m_dateTime = m_dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        m_dateTime = m_dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        m_dateTime = m_dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(TextMeshProUGUI target)
    {
        m_calendarPanel.SetActive(true);
        m_target = target;
        // m_calendarPanel.transform.position = Input.mousePosition-new Vector3(0,120,0);
    }
    public void OnDateItemClick(string day)
    {
        m_target.text = m_yearNumText.text + " year, " + m_monthText.text + " " + day+"day";
        // m_calendarPanel.SetActive(false);
    }
}
