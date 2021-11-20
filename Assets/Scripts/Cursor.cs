using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public bool Clicked { get; private set; }
    [SerializeField] private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        var stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        var stick = isRight ? stickR : stickL;

        var dx = stick.x;
        var dy = stick.y;

        // var dx = Input.GetAxis("Horizontal");
        // var dy = Input.GetAxis("Vertical");

        transform.localPosition = new Vector3(Cap(dx * 150, 110) - (isRight ? -1 : 1) * 125, Cap(dy * 100, 75), 0);

        Clicked = isRight ? OVRInput.Get(OVRInput.RawButton.RIndexTrigger) : OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger);
        // Clicked = Input.GetKey(KeyCode.Return);
    }

    private float Cap(float value, float max)
    {
        return Mathf.Abs(value) < max ? value : max * Mathf.Sign(value);
    }
}
