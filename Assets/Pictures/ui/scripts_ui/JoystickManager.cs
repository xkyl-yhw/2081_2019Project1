using UnityEngine;

public class JoystickManager : MonoBehaviour
{

    public Joystick joystick;
    public Transform moveTarget;
    public float moveSpeed = 10.0f;

    // Use this for initialization
    void Start()
    {
        joystick.OnTouchMove += OnJoystickMove;
    }

    private void OnJoystickMove(JoystickData joystickData)
    {
        //Debug.Log(1);
       moveTarget.GetComponent<move>().moveh = Mathf.Cos(joystickData.radians) *joystickData.power;
        moveTarget.GetComponent<move>().movev = Mathf.Sin(joystickData.radians) * joystickData.power;
       
    }
}