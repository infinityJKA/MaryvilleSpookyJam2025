using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed;
    public RectTransform rectTransform;
    
    public void ResetScroll()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -1100);
    }

    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
    }
}
