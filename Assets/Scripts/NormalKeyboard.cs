using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NormalKeyboard : MonoBehaviour
{
    [SerializeField] NormalKey keyPrefab;
    [SerializeField] float keyDistance;
    [SerializeField] InputExam exam;
    public string Value { get; private set; }
    private bool expansion, shift;

    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("normal_layout");
        Debug.Log(textFile.text);

        var keyArray = textFile.text.Split('\n');
        float xOffset = 0;
        float yOffset = 0;
        float xDefault = -1.25f;
        foreach (var row in keyArray)
        {
            xOffset = xDefault;
            var keys = row.Split(',');
            foreach (var key in keys)
            {
                var tmp = Instantiate<NormalKey>(keyPrefab, transform);
                tmp.Init(key == "comma" ? "," : key, key);
                tmp.transform.position += new Vector3(xOffset, yOffset, 0);
                xOffset += keyDistance;
            }
            yOffset -= keyDistance;
            xDefault += 0.1f;
        }

        var cursor = transform.GetChild(0);
        cursor.SetSiblingIndex(transform.childCount - 1);
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Submit();
        }
    }

    public void Press(string key)
    {
        Value += key;
        Logger.AddAction("press " + key);
        Debug.Log("pressed: " + key);
        KeyBoardOutput.value = Value;
    }

    // TODO: 以下特殊入力

    public void Submit()
    {
        exam.Submit(Value);
        Value = "";
        KeyBoardOutput.value = Value;
    }

    public void BackSpace()
    {
        Value = Value.Remove(Value.Length - 1, 1);
        Logger.AddAction("back space");
        KeyBoardOutput.value = Value;
    }

    public void Space()
    {
        Value += " ";
        Logger.AddAction("space");
        KeyBoardOutput.value = Value;
    }
}
