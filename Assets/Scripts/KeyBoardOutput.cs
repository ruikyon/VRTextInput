using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardOutput : MonoBehaviour
{
    [SerializeField] Keyboard keyboard;
    Text text;

    public static string value;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = value;
    }
}
