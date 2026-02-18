using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class CameraZoom : MonoBehaviour
{
    public float zoomStep = 0.5f;
    public float minZoom = 1.2f;
    public float maxZoom = 6.0f;

    private CinemachineOrbitalFollow orbitalFollow;

    void Start()
    {
        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
    }

    void Update()
    {
        if (Mouse.current != null && orbitalFollow != null)
        {
            float scrollValue = Mouse.current.scroll.ReadValue().y;

            if (scrollValue != 0)
            {
                float newRadius = orbitalFollow.Radius - (Mathf.Sign(scrollValue) * zoomStep);
                orbitalFollow.Radius = Mathf.Clamp(newRadius, minZoom, maxZoom);
            }
        }
    }
}