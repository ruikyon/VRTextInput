using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerInput : MonoBehaviour
{
    [SerializeField] Pointer left;
    [SerializeField] Pointer right;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            right.OnButtonDown();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            right.OnButtonUp();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            left.OnButtonDown();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            left.OnButtonUp();
        }
    }
}
