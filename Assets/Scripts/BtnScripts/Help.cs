using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpPanel;

    public void HelpBtn()
    {
        helpPanel.SetActive(true);
        Invoke(nameof(HideHelp), 2f);
    }

    void HideHelp()
    {
        helpPanel.SetActive(false);
    }
}
