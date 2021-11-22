using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Keyboard : BaseKeyboard
{
    List<Key> keyList;
    int preXL = -1, preYL = -1, preXR = -1, preYR = -1;
    private bool expansionL, expansionR;

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
            xOffset = -2;
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
    }

    private void Start()
    {
        ChangeArea(true, false);
        ChangeArea(false, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            BackSpace();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Submit();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            expansionR = true;
            ChangeArea(true, true);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            expansionR = false;
            ChangeArea(true, false);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            expansionL = true;
            ChangeArea(false, true);
        }

        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            expansionL = false;
            ChangeArea(false, false);
        }

        var stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        var x = StickValue(stickL.x) * (expansionL ? 2 : 1);
        var y = StickValue(stickL.y);

        if (x != preXL || y != preYL)
        {
            var data = new PointerEventData(EventSystem.current);
            keyList[(1 - preYL) * 10 + preXL + 2].GetComponent<Button>().OnPointerExit(data);
            keyList[(1 - y) * 10 + x + 2].GetComponent<Button>().OnPointerEnter(data);
            preXL = x;
            preYL = y;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            keyList[(1 - preYL) * 10 + preXL + 2].OnClick();
        }

        var stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

        x = StickValue(stickR.x) * (expansionR ? 2 : 1);
        y = StickValue(stickR.y);

        if (x != preXR || y != preYR)
        {
            var data = new PointerEventData(EventSystem.current);
            keyList[(1 - preYR) * 10 + preXR + 7].GetComponent<Button>().OnPointerExit(data);
            keyList[(1 - y) * 10 + x + 7].GetComponent<Button>().OnPointerEnter(data);
            preXR = x;
            preYR = y;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            keyList[(1 - preYR) * 10 + preXR + 7].OnClick();
        }

        KeyBoardOutput.value = Value;
    }

    private int StickValue(float value)
    {
        var threshold = 0.33f;
        if (value < -threshold)
        {
            return -1;
        }
        else if (value < threshold)
        {
            return 0;
        }
        return 1;
    }

    private void ChangeArea(bool isRight, bool isExpansion)
    {
        for (var i = 0; i < 3; i++)
        {
            var offset = (2 - i) * 10 + (isRight ? 5 : 0);
            keyList[offset + 1].GetComponent<Button>().interactable = !isExpansion;
            keyList[offset + 3].GetComponent<Button>().interactable = !isExpansion;
            keyList[offset + 0].GetComponent<Button>().interactable = isExpansion;
            keyList[offset + 4].GetComponent<Button>().interactable = isExpansion;
        }
    }
}
