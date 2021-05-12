using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 120, 90), "Own IP address");
        GUI.Label(new Rect(25, 40, 100, 30), information.own_ip + ":" + information.own_port.ToString());

        GUI.Box(new Rect(Screen.width - 130, 10, 120, 90), "Ribal IP address");
        GUI.Label(new Rect(Screen.width - 100, 40, 100, 30), information.ribal_ip + ":" + information.ribal_port.ToString());

        //GUI.Box(new Rect(Screen.width / 3, 10, 400, 200), "Error Log");
        //GUI.Label(new Rect(Screen.width / 3, 40, 400, 200), );
    }
}