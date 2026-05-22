using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSdropbox : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public void DropdownSelect(int val)
    {
        switch (val)
        {
            case 0:
                Debug.Log("Option 1 Selected");
                Application.targetFrameRate = 30;
                break;

            case 1:
                Debug.Log("Option 2 Selected");
                Application.targetFrameRate = 60;
                break;

            case 2:
                Debug.Log("Option 3 Selected");
                Application.targetFrameRate = 90;
                break;

            case 3:
                Debug.Log("Option 4 Selected");
                Application.targetFrameRate = 120;
                break;

            case 4:
                Debug.Log("Option 5 Selected");
                Application.targetFrameRate = -1;
                break;

            default:
                Debug.Log("Option def Selected");
                Application.targetFrameRate = -1;
                break;
        }
    }
}
