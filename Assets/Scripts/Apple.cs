using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Apple : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Get a reference to the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the grab events
        grabInteractable.selectExited.AddListener(OnGrab);
    }

    private void OnGrab(SelectExitEventArgs arg0)
    {
        rigidbody.isKinematic = false;
    }
}
