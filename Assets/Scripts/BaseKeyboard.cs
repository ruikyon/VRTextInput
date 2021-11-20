using UnityEngine;

public class BaseKeyboard : MonoBehaviour
{
    [SerializeField] protected Key keyPrefab;
    [SerializeField] protected InputExam exam;
    [SerializeField] protected float keyDistance;
    public string Value { get; protected set; }

    public void Press(string key)
    {
        Value += key;
        Logger.AddAction("press " + key);
        KeyBoardOutput.value = Value;
    }

    public void Submit()
    {
        if (Value.Length == 0) { return; }

        exam.Submit(Value);
        Value = "";
        KeyBoardOutput.value = Value;
    }

    public void BackSpace()
    {
        Logger.AddAction("back space");

        if (Value.Length == 0) { return; }

        Value = Value.Remove(Value.Length - 1, 1);
        KeyBoardOutput.value = Value;
    }

    public void Space()
    {
        Value += " ";
        Logger.AddAction("space");
        KeyBoardOutput.value = Value;
    }
}
