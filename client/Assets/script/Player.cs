using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {
        JsonOBJ jsonObj = new JsonOBJ(eventType.Message);
        jsonObj.msg = "Player sender";
        NetworkManager.instance.Data_Send(jsonObj);
    }
}
