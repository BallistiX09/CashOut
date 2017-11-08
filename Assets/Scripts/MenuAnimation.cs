using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour {

    [SerializeField] private MenuController menuController;

    public void PanelClosed()
    {
        //Triggers the function to disable the active panel when the animation finishes
        menuController.DisableCurrentPanel();
    }
}
