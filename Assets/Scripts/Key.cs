using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private BaseKeyboard keyboard;
    private string value, shiftValue, markValue, current;
    private bool disableKey = false;
    [SerializeField] private Text text;

    public void Init(string _value, string _shiftValue)
    {
        keyboard = transform.parent.GetComponent<BaseKeyboard>();

        value = _value;
        shiftValue = _shiftValue;
        current = _value;

        if (value.Length == 0)
        {
            text.text = "X";
            text.color = Color.red;
            disableKey = true;
        }
        else
        {
            text.text = value[0].ToString();
        }
    }

    public void OnClick()
    {
        if (!disableKey)
        {
            keyboard.Press(current);
        }
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
