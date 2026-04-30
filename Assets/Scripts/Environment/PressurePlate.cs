using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator buttonAnimator;
    public string boolParameter = "IsPressed";
    
    [Header("Events")]
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    [SerializeField] private int objectsOnTop = 0;

    private void OnTriggerEnter(Collider other)
    {
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
    }

    private void DeactivatePlate()
    {
        if (buttonAnimator != null) buttonAnimator.SetBool(boolParameter, false);
        OnDeactivated?.Invoke();
    }
}
