using UnityEngine;

public class MainMenuBtn : MonoBehaviour
{
    public SceneFader sceneFader;

    public void StartBtn()
    {    
        sceneFader.FadeTo("Level01");
    }

    public void ExitBtn()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
