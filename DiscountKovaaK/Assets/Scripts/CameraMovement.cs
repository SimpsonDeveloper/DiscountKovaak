using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float verticalRotation;
    private float ySensitivity;
    public bool invertY = false;
    public bool showMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        ySensitivity = GameManager.Instance.ySensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!showMenu)
        {
            verticalRotation = Mathf.Clamp((invertY?1:-1)* Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime + verticalRotation, -90.0f, 90.0f);
            transform.localEulerAngles = new Vector3(verticalRotation, 0.0f, 0.0f);
        }
    }

    public void LockIn()
    {
        showMenu = false;
    }

    public void LockOut()
    {
        showMenu = true;
    }

    public void setYSensitivity(float ySens)
    {
        ySensitivity = ySens * Conversions.SliderModifier;
    }
}
