using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStarPanelVisibility : MonoBehaviour
{
    public void ShowHidePanel(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
