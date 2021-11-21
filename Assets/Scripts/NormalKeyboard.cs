using UnityEngine;

public class NormalKeyboard : BaseKeyboard
{
    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("normal_layout");
        Debug.Log(textFile.text);

        var keyArray = textFile.text.Split('\n');
        float xOffset = 0;
        float yOffset = 0;
        float xDefault = -1.15f;
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
            }
            yOffset -= keyDistance;
            xDefault += 0.07f;
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Submit();
        }
    }
}
