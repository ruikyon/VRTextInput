using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Keyboard : MonoBehaviour
{
    [SerializeField] Key keyPrefab;
    [SerializeField] float keyDistance;
    List<Key> keyList;
    int preXL = -1, preYL = -1, preXR = -1, preYR = -1;
    public string Value { get; private set; }
    private bool expansion, shift;

    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("keyboard");
        Debug.Log(textFile.text);
        keyList = new List<Key>();

        var keyArray = textFile.text.Split('\n');
        float xOffset = 0;
        float yOffset = 0;
        foreach (var row in keyArray)
        {
            xOffset = -4;
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
        }

        var cursor = transform.GetChild(0);
        cursor.SetSiblingIndex(transform.childCount - 1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // var dx = (int)Input.GetAxisRaw("Horizontal");
        // var dy = (int)Input.GetAxisRaw("Vertical");
        // Debug.Log(dx + ", " + dy);
        // if (dx != preX || dy != preY)
        // {
        //     var data = new PointerEventData(EventSystem.current);
        //     keyList[(1 - preY) * 10 + preX + 1].GetComponent<Button>().OnPointerExit(data);
        //     keyList[(1 - dy) * 10 + dx + 1].GetComponent<Button>().OnPointerEnter(data);
        //     preX = dx;
        //     preY = dy;
        // }

        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     keyList[(1 - preY) * 10 + preX + 1].onClick();
        // }


        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            expansion = true;
            for (var i = 0; i < 3; i++)
            {
                keyList[(1 - i) * 10 + 1].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 3].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 0].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 4].GetComponent<Button>().interactable = true;

                keyList[(1 - i) * 10 + 6].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 8].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 5].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 9].GetComponent<Button>().interactable = true;
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            expansion = false;
            for (var i = 0; i < 3; i++)
            {
                keyList[(1 - i) * 10 + 1].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 3].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 0].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 4].GetComponent<Button>().interactable = false;

                keyList[(1 - i) * 10 + 6].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 8].GetComponent<Button>().interactable = true;
                keyList[(1 - i) * 10 + 5].GetComponent<Button>().interactable = false;
                keyList[(1 - i) * 10 + 9].GetComponent<Button>().interactable = false;
            }
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            shift = true;
            foreach (var key in keyList)
            {
                key.ToShift();
            }
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            shift = false;
            foreach (var key in keyList)
            {
                key.ToNormal();
            }

        }

        var stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        var stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

        var x = StickXValue(stickL.x) * (expansion ? 2 : 1);
        var y = StickYValue(stickL.y);

        if (x != preXL || y != preYL)
        {
            var data = new PointerEventData(EventSystem.current);
            keyList[(1 - preYL) * 10 + preXL + 2].GetComponent<Button>().OnPointerExit(data);
            keyList[(1 - y) * 10 + x + 2].GetComponent<Button>().OnPointerEnter(data);
            preXL = x;
            preYL = y;
        }

        // var lTrigger1 = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        // if (lTrigger1 > 0.8)
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            keyList[(1 - preYL) * 10 + preXL + 2].onClick();
        }

        x = StickXValue(stickR.x) * (expansion ? 2 : 1);
        y = StickYValue(stickR.y);

        if (x != preXR || y != preYR)
        {
            var data = new PointerEventData(EventSystem.current);
            keyList[(1 - preYR) * 10 + preXR + 7].GetComponent<Button>().OnPointerExit(data);
            keyList[(1 - y) * 10 + x + 7].GetComponent<Button>().OnPointerEnter(data);
            preXR = x;
            preYR = y;
        }

        // var rTrigger1 = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        // if (rTrigger1 > 0.8)
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            keyList[(1 - preYR) * 10 + preXR + 7].onClick();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A) && Value.Length > 0)
        {
            Value = Value.Remove(Value.Length - 1, 1);
        }
    }

    private int StickYValue(float value)
    {
        if (value < -0.5)
        {
            return -1;
        }
        else if (value < 0.5)
        {
            return 0;
        }
        return 1;
    }

    private int StickXValue(float value)
    {
        // if (value < -0.8)
        // {
        //     return -2;
        // }
        // else if (value < -0.2)
        // {
        //     return -1;
        // }
        // else if (value < 0.2)
        // {
        //     return 0;
        // }
        // else if (value < 0.8)
        // {
        //     return 1;
        // }
        // return 2;

        if (value < -0.5)
        {
            return -1;
        }
        else if (value < 0.2)
        {
            return 0;
        }
        return 1;
    }

    public void Press(string key)
    {
        Value += key;
    }
}
