using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class inform
{
    public string address;
    public int port;

    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scal;
}


[Serializable]
public class JSON
{
    //public string Ribal_ip;
    //public string Ribal_port;

    //public string type;
    //public string uuid;
    //public string name;
    //public string message;

    public string type;

    public inform own;
    public inform ribal;

    public JSON(eventType state)
    {
        this.type = state.ToString();
    }

    public static JSON CreateFromJSON(string data)
    {
        try
        {
            return JsonUtility.FromJson<JSON>(data);
        }
        catch (Exception e)
        {
            Debug.Log("<color=green> Client: </color> JSON«¿«¤«×ªÇªÏª¢ªêªÞª»ªó¡£NULLªÇÚ÷ª·ªÞª¹");
            return null;
        }
    }
    public static string CreateToJSON(JSON data)
    {
        return JsonUtility.ToJson(data);
    }
}
