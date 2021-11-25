using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardOutput : MonoBehaviour
{
    [SerializeField] Keyboard keyboard;
    private TMPro.TextMeshProUGUI cardNameText;
    Text text;

    public static string value;

    // Start is called before the first frame update
    void Start()
    {
        cardNameText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        cardNameText.text = value + "_";
    }
}
