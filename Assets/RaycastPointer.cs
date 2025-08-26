using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPointer : MonoBehaviour
{
    private Camera cam;

    public Trigger trigger;
    public TrailRenderer trail;
    public Transform pointer;
    public float lazorLength;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRay();

        //change GetMouseButtonUp(0) to button on VR controller
        if (Input.GetMouseButtonUp(0) && trigger != null)
        {
            InfoPanel.Instance.DisplayPanel(trigger.TriggerName);
        }
    }

    private void HandleRay()
    {
        //change the mousePosition to pointer position
        Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        trail.Clear();
        trail.AddPosition(pointer.position);
        trail.AddPosition(ray.GetPoint(lazorLength));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent<Trigger>(out Trigger trigger))
            {
                this.trigger = trigger;
            }
            else
            {
                this.trigger = null;
            }
        }
        else
        {
            this.trigger = null;
        }
    }
}
