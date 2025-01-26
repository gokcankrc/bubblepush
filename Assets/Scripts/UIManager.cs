using Ky;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject PressAnyKeyUI;
    public GameObject LoseUI;
    public GameObject WinUI;

    public void CloseAllUI()
    {
        PressAnyKeyUI.gameObject.SetActive(false);
        LoseUI.gameObject.SetActive(false);
        WinUI.gameObject.SetActive(false);
    }

    public void ShowLoseUI()
    {
        Cursor.visible = true;
        LoseUI.gameObject.SetActive(true);
    }

    public void ShowWinUI()
    {
        Cursor.visible = true;
        WinUI.gameObject.SetActive(true);
    }

    public void ShowPressAnyKeyUI()
    {
        Cursor.visible = true;
        PressAnyKeyUI.gameObject.SetActive(true);
    }
    
    public void HideLoseUI()
    {
        LoseUI.gameObject.SetActive(false);
    }

    public void HideWinUI()
    {
        WinUI.gameObject.SetActive(false);
    }

    public void HidePressAnyKeyUI()
    {
        PressAnyKeyUI.gameObject.SetActive(false);
    }

}