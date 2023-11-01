using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public CharacterInput CharacterInput;
    public FPSCamera FPSCamera;
    public WeaponController WeaponController;

    public void Pause()
    {
        CharacterInput.InputOn = false;
        Time.timeScale = 0;
        FPSCamera.ShowCursor();
        foreach (Gun g in WeaponController.AllGuns)
        {
            g.enabled = false;
        }
    }

    public void UnPause()
    {
        CharacterInput.InputOn = true;
        Time.timeScale = 1;
        FPSCamera.LockAndHideCursor();
        foreach (Gun g in WeaponController.AllGuns)
        {
            g.enabled = true;
        }
    }
}
