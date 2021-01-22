using UnityEngine;
using Doozy.Engine.UI;

public class PopupManager : MonoBehaviour
{
    // Used by assets
    public void ShowPopup(string popupName)
    {
        UIPopup popup = UIPopup.GetPopup(popupName);
        popup.Show();
    }
}
