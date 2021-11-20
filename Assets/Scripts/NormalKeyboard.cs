using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NormalKeyboard : MonoBehaviour
{
    [SerializeField] Key keyPrefab;
    [SerializeField] float keyDistance;
    [SerializeField] InputExam exam;
    List<Key> keyList;
    public string Value { get; private set; }
    private bool expansion, shift;

    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("normal_layout");
        Debug.Log(textFile.text);
        keyList = new List<Key>();

        var keyArray = textFile.text.Split('\n');
        float xOffset = 0;
        float yOffset = 0;
        float xDefault = -50;
        foreach (var row in keyArray)
        {
            xOffset = xDefault;
            var keys = row.Split(',');
            foreach (var key in keys)
            {
                var tmp = Instantiate<Key>(keyPrefab, transform);
                tmp.Init(key == "comma" ? "," : key, key);
                tmp.transform.position += new Vector3(xOffset, yOffset, 0);
                xOffset += keyDistance;
                keyList.Add(tmp);
            }
            yOffset -= keyDistance;
            xDefault += 2.25f;
        }

        var cursor = transform.GetChild(0);
        cursor.SetSiblingIndex(transform.childCount - 1);
    }

    public void Press(string key)
    {
        Value += key;
        Logger.AddAction("press " + key);
    }

    // TODO: 以下特殊入力

    public void Submit()
    {
        exam.Submit(Value);
    }

    public void BackSpace()
    {
        Value = Value.Remove(Value.Length - 1, 1);
        Logger.AddAction("back space");
    }

    public void Space()
    {
        Value += " ";
        Logger.AddAction("space");
    }
}
