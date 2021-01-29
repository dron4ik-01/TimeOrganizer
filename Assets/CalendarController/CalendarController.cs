using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calendar.Controller
{
    public class CalendarController : MonoBehaviour
    {
        [SerializeField] private GameObject m_item;
        [SerializeField] private GameObject m_circle;

        [SerializeField] private Button m_monthNextBtn;
        [SerializeField] private Button m_monthPrevBtn;

        [SerializeField] private TextMeshProUGUI m_monthText;

        [SerializeField] private TextMeshProUGUI m_targetYear;
        [SerializeField] private TextMeshProUGUI m_targetDay;


        private List<GameObject> m_dateItems = new List<GameObject>();
        private const int m_totalDateNum = 42;
        private int m_emptyItemsCount;

        private DateTime m_dateTime;
        private DateTime m_selectedDate;

        
        
        private void Awake()
        {
            Vector3 startPos = m_item.transform.localPosition;
            m_dateItems.Clear();
            m_dateItems.Add(m_item);

            for (int i = 1; i < m_totalDateNum; i++)
            {
                GameObject item = Instantiate(m_item, m_item.transform.parent, true);
                Rect itemRect = item.GetComponent<RectTransform>().rect;

                item.name = "Item" + (i + 1).ToString();
                item.transform.localScale = Vector3.one;
                item.transform.localRotation = Quaternion.identity;
                // Arranges items between themselves on range equal to their width and height
                item.transform.localPosition = new Vector3((i % 7) * itemRect.width + startPos.x,
                    startPos.y - (i / 7) * itemRect.height, startPos.z);

                m_dateItems.Add(item);
            }

            m_dateTime = DateTime.Now;
            m_selectedDate = DateTime.Now;

            CreateCalendar();

            OnDateItemClick(DateTime.Now.Day.ToString());
            m_monthNextBtn.onClick.AddListener(MonthNext);
            m_monthPrevBtn.onClick.AddListener(MonthPrev);
        }

        private void CreateCalendar()
        {
            DateTime firstDay = m_dateTime.AddDays(-(m_dateTime.Day - 1));
            int index = GetDays(firstDay.DayOfWeek);
            m_emptyItemsCount = index - 1;

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

            m_circle.SetActive(false);
            m_monthText.text = GetMonth(m_dateTime.Month) + " " + m_dateTime.Year;
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

        public static string GetMonth(int monthNum)
        {
            switch (monthNum)
            {
                case 1: return "January"; case 2: return "February";
                case 3: return "March"; case 4: return "April";
                case 5: return "May"; case 6: return "June";
                case 7: return "July"; case 8: return "August";
                case 9: return "September"; case 10: return "October";
                case 11: return "November"; case 12: return "December";
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

        public DateTime GetSelectedDate()
        {
            return m_selectedDate;
        }
        
        public void OnDateItemClick(string day)
        {
            int.TryParse(day, out int iDay);
            DateTime targetDate = new DateTime(m_dateTime.Year, m_dateTime.Month, iDay);
            string dayOfWeek = targetDate.DayOfWeek.ToString();

            m_targetYear.text = m_dateTime.Year.ToString();
            m_targetDay.text = dayOfWeek.Remove(3, dayOfWeek.Length - 3) + ", " + iDay + " " + GetMonth(m_dateTime.Month);

            m_selectedDate = targetDate;

            m_circle.SetActive(true);
            m_circle.transform.position = m_dateItems[m_emptyItemsCount + iDay].transform.position;
        }
    }

}