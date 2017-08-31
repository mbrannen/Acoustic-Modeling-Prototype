using UnityEngine;

public class SampleTools : MonoBehaviour {

    void OnEnable()
    {
        foreach(Canvas canvas in gameObject.transform.GetComponentsInChildren<Canvas>())
        {
            canvas.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        if (gameObject.transform.childCount >= 1)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
