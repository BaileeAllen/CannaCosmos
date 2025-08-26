using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRLaserPointer : MonoBehaviour
{
    [Tooltip("Maximum length of the laser beam.")]
    public float laserMaxLength = 50f;

    [Tooltip("LayerMask for interactable objects (e.g., Constellations).")]
    public LayerMask interactableLayer;

    private XRController controller;
    private LineRenderer lineRenderer;
    private Trigger currentTrigger;

    void Start()
    {
        // Auto-find the XRController in the parent hierarchy
        controller = GetComponentInParent<XRController>();

        if (controller == null)
        {
            Debug.LogError("VRLaserPointer: No XRController found in parent. Make sure this object is a child of the RightHand Controller.");
            enabled = false;
            return;
        }

        // Create and configure the LineRenderer for the laser
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.002f;
        lineRenderer.endWidth = 0.002f;

        // Create the laser material with glow
        Material laserMat = new Material(Shader.Find("Unlit/Color"));
        laserMat.SetColor("_Color", Color.green);
        laserMat.EnableKeyword("_EMISSION");
        laserMat.SetColor("_EmissionColor", Color.green * 2f); // Glow intensity
        lineRenderer.material = laserMat;

        // Fade from green to transparent
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.green, 0.0f),
                new GradientColorKey(Color.green, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        lineRenderer.colorGradient = gradient;

        // Start disabled
        lineRenderer.enabled = false;
    }

    void Update()
    {
        HandleRay();
        lineRenderer.enabled = true;

        if (currentTrigger != null)
        {
            InfoPanel.Instance.DisplayPanel(currentTrigger.TriggerName);
        }
    }

    private void HandleRay()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        lineRenderer.SetPosition(0, rayOrigin);

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, laserMaxLength, interactableLayer))
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent(out Trigger trigger))
            {
                currentTrigger = trigger;
            }
            else
            {
                currentTrigger = null;
            }
        }
        else
        {
            lineRenderer.SetPosition(1, rayOrigin + rayDirection * laserMaxLength);
            currentTrigger = null;
        }
    }
}
