using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("StartGame()");
        
        GameManager.instance.IntroCutscene();
    }
}
