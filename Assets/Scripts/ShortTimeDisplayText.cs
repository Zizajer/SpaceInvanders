using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortTimeDisplayText : MonoBehaviour
{
    public float TimeCanBeDisplayed;
    public string TextToDisplay;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = TextToDisplay;
        Destroy(gameObject, TimeCanBeDisplayed);
    }
}
