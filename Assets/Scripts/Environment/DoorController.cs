using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Animation")]
    public Animator doorAnimator;
    public string openBool = "IsOpen";

    private void Awake()
    {
        if (doorAnimator == null)
            doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (doorAnimator != null)
            doorAnimator.SetBool(openBool, true);
    }

    public void CloseDoor()
    {
        if (doorAnimator != null)
            doorAnimator.SetBool(openBool, false);
    }
}
