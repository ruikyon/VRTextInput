using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalKey : MonoBehaviour
{
    private NormalKeyboard keyboard;
    private string value, shiftValue, markValue, current;
    [SerializeField] private Text text;

    public void Init(string _value, string _shiftValue)
    {
        keyboard = transform.parent.GetComponent<NormalKeyboard>();

        value = _value;
        shiftValue = _shiftValue;
        current = _value;

        text.text = value[0].ToString();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        Debug.Log("press: " + current);
        keyboard.Press(current);
    }

    public void ToShift()
    {
        current = shiftValue;
        text.text = shiftValue[0].ToString();
    }

    public void ToNormal()
    {
        current = value;
        text.text = value[0].ToString();
    }
}
