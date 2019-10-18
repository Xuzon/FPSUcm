using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    [SerializeField]
    protected float maxLocalY = 0.1f;
    [SerializeField]
    protected float lerpStrength = 0.1f;
    [SerializeField]
    protected Vector2 angles = new Vector2(1, 15);

    protected Transform cam;
    protected Transform Cam
    {
        get
        {
            if (cam == null)
            {
                cam = Camera.main.transform;
            }
            return cam;
        }
    }

    protected float initialLocalY;
    void OnEnable()
    {
        initialLocalY = transform.localPosition.y;
    }

    internal void SetRecoil()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, initialLocalY + maxLocalY, transform.localPosition.z);
        var strength = Random.Range(angles.x, angles.y);
        Cam.Rotate(cam.right, strength);
    }

    // Update is called once per frame
    void Update()
    {
        var newY = Mathf.Lerp(transform.localPosition.y, initialLocalY, lerpStrength);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}
