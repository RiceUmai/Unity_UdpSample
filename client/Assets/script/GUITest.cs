using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 100, 30), information.own_ip);
        GUI.Label(new Rect(25, 50, 100, 30), information.own_port.ToString());

        GUI.Label(new Rect(Screen.width - 100, 25, 100, 30), information.ribal_ip);
        GUI.Label(new Rect(Screen.width - 100, 50, 100, 30), information.ribal_port.ToString());
    }
}