using System;
using System.Collections;
using System.Collections.Generic;
using Calendar.Controller;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;

namespace TimeOrganizer.Blocks
{
    public class ManageBlocksPanel : ManagePanel
    {
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private UIButton m_dateButton;
        private DateTime m_selectedDate = DateTime.Now;
        
        private void Start()
        {
            SetDateText(m_selectedDate); // set present day
            
            m_dateButton.OnClick.OnTrigger.Event.AddListener(() =>
            {
                UIPopup calendarPopup = UIPopup.GetPopup("CalendarPopup");
                calendarPopup.Show();
                calendarPopup.Data.Buttons[0].OnClick.OnTrigger.Event.AddListener(() =>
                {
                    m_selectedDate = calendarPopup.GetComponent<CalendarController>().GetSelectedDate();
                    SetDateText(m_selectedDate);
                });
            });
        }

        private void SetDateText(DateTime selectedDate)
        {
            TextMeshProUGUI dateText = m_dateButton.GetComponentInChildren<TextMeshProUGUI>();

            string dayOfWeek = selectedDate.DayOfWeek.ToString();
            string month = CalendarController.GetMonth(selectedDate.Month);

            // tue,jan.28,2021 - example
            dateText.text = dayOfWeek.Remove(3, dayOfWeek.Length - 3).ToLower() + ", " +
                            month.Remove(3, month.Length - 3).ToLower() + ". " +
                            selectedDate.Day + ", " + selectedDate.Year;
        }
    }
    
}