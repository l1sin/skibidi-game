using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static float MouseInputX;
    public static float MouseInputY;

    public static float MoveInputX;
    public static float MoveInputY;

    public static bool Jump;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Jump = false;
    }

    private void GetInput()
    {
        if (Input.GetButtonDown(GlobalStrings.JumpInput))
        {
            Jump = true;
        }

        MouseInputX = Input.GetAxis(GlobalStrings.MouseXInput);
        MouseInputY = Input.GetAxis(GlobalStrings.MouseYInput);

        MoveInputX = Input.GetAxisRaw(GlobalStrings.HorizontalInput);
        MoveInputY = Input.GetAxisRaw(GlobalStrings.VerticalInput); 
    }
}
