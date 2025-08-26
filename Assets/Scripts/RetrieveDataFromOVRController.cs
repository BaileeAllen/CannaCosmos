using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveDataFromOVRController : MonoBehaviour
{

    public OVRInput.Controller m_controller;

    //3D Text or GameObject with text component needed
    public TextMesh myTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Write controller values in text
        //Controller position + Controller Rotation
        myTextMesh.text = " Position: " + OVRInput.GetLocalControllerPosition(m_controller).ToString()
            + " \n Rotation: " + OVRInput.GetLocalControllerRotation(m_controller).ToString();

        /* Just use the following button state in the text above if needed
         OVRInput.Get(OVRInput.Button.One, m_controller)
        OVRInput.Get(OVRInput.Button.Two, m_controller)
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x)
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y)
        OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller))
        OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller))

         */
    }
}
