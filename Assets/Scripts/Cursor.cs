using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public bool Clicked { get; private set; }
    [SerializeField] private bool isRight;

    private void Update()
    {
        var stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        var stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        var stick = isRight ? stickR : stickL;

        var x = Cap(stick.x * 125, 105) - (isRight ? -1 : 1) * 112.5f;
        var y = Cap(stick.y * 75, 60);
        transform.localPosition = new Vector3(x, y, 0);

        Clicked = isRight ? OVRInput.Get(OVRInput.RawButton.RIndexTrigger) : OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger);
    }

    private float Cap(float value, float max)
    {
        return Mathf.Abs(value) < max ? value : max * Mathf.Sign(value);
    }
}
