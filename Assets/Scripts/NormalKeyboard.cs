using UnityEngine;

public class NormalKeyboard : BaseKeyboard
{
    [SerializeField] private float xDefault, xShift;

    void Awake()
    {
        var textFile = Resources.Load<TextAsset>("normal_layout");
        Debug.Log(textFile.text);

        var keyArray = textFile.text.Split('\n');
        float xOffset = 0;
        float yOffset = 0;
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
            xDefault += xShift;
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
