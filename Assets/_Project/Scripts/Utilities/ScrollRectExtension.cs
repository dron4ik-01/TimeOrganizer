using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectExtension : MonoBehaviour
{
    [SerializeField] private ScrollRect m_scrollRect;

    private void Awake()
    {
        RebuildScrollRect();
    }

    public void RebuildScrollRect()
    { 
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_scrollRect.content);
    }

    public void ScrollToTop()
    {
        m_scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    
    public void ScrollToBottom()
    {
        m_scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}