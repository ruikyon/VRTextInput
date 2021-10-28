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
        var dx = Input.GetAxisRaw("Horizontal");
        var dy = Input.GetAxisRaw("Vertical");
        transform.localPosition = new Vector3(Cap(dx * 150, 110) - (isRight ? -1 : 1) * 130, Cap(dy * 100, 75), 0);
    }

    private float Cap(float value, float max)
    {
        return Mathf.Abs(value) < max ? value : max * Mathf.Sign(value);
    }
}
