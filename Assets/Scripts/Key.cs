using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private BaseKeyboard keyboard;
    private string value, shiftValue, markValue, current;
    [SerializeField] private Text text;

    public void Init(string _value, string _shiftValue)
    {
        keyboard = transform.parent.GetComponent<BaseKeyboard>();

        value = _value;
        shiftValue = _shiftValue;
        current = _value;

        text.text = value[0].ToString();
    }

    public void OnClick()
    {
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
