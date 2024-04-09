using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    public bool isPlay;
    public bool isQuit;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPlay)
        {
            SceneManager.LoadScene("2D");
        }
        else if (isQuit)
        {
            Application.Quit();
        }
    }
}