using UnityEngine;

public class NormalKeyboard : BaseKeyboard
{
    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("normal_layout");
        Debug.Log(textFile.text);

        var keyArray = textFile.text.Split('\n');
        var yOffset = keyDistance;
        var xDefault = -5.3f * keyDistance;
        foreach (var row in keyArray)
        {
            var xOffset = xDefault;
            var keys = row.Split(',');
            foreach (var key in keys)
            {
                var tmp = Instantiate<Key>(keyPrefab, transform);
                tmp.Init(key == "comma" ? "," : key, key);
                tmp.transform.localPosition += new Vector3(xOffset, yOffset, 0);
                xOffset += keyDistance;
            }
            yOffset -= keyDistance;
            xDefault += 0.3f * keyDistance;
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Submit();
        }
    }
}
