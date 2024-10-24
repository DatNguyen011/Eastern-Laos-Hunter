using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : Singleton<JoystickControl>
{
    public static Vector3 direct;

    private Vector3 screen;

    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private Vector3 startPoint;
    private Vector3 updatePoint;

    public RectTransform joystickBG;
    public RectTransform joystickControl;
    public float magnitude;

    public GameObject joystickPanel;

    private bool active;

    // Start is called before the first frame update
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;

        joystickPanel.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            updatePoint = MousePosition;
            Vector3 direction = updatePoint - startPoint;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude(direction, magnitude);

            direct = (updatePoint - startPoint).normalized;
            direct.z = 0;
            //Debug.Log(direct);
        }
    }

    public void ButtonDown()
    {
        startPoint = joystickBG.anchoredPosition;
        joystickPanel.SetActive(true);
        active = true;
    }

    public void ButtonUp()
    {
        joystickPanel.SetActive(false);
        direct = Vector3.zero;
        active = false;
    }


    private void OnDisable()
    {
        joystickPanel.SetActive(false);
        active = false;
    }
}
