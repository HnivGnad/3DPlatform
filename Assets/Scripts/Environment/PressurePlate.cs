using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator buttonAnimator;
    public string boolParameter = "IsPressed";
    
    [Header("Events")]
    public UnityEvent OnActivated;   // Mở cửa
    public UnityEvent OnDeactivated; // Đóng cửa

    [SerializeField] private int objectsOnTop = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Cho phép bất kỳ vật thể nào có Collider chạm vào (Player, Đá, v.v.)
        objectsOnTop++;
        
        if (objectsOnTop == 1) 
        {
            ActivatePlate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsOnTop--;

        if (objectsOnTop <= 0) 
        {
            objectsOnTop = 0;
            DeactivatePlate();
        }
    }

    private void ActivatePlate()
    {
        if (buttonAnimator != null) buttonAnimator.SetBool(boolParameter, true);
        OnActivated?.Invoke();
        Debug.Log("<color=green>Nút đã bị đè!</color>");
    }

    private void DeactivatePlate()
    {
        if (buttonAnimator != null) buttonAnimator.SetBool(boolParameter, false);
        OnDeactivated?.Invoke();
        Debug.Log("<color=yellow>Nút đã nổi lên!</color>");
    }
}
