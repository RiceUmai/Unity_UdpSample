using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class inform
{
    public string address;
    public int port;

    public string name;

    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scal;
}


[Serializable]
public class JsonOBJ
{
    public string type;
    public string msg;

    public inform own;
    public inform ribal;

    public JsonOBJ(eventType state)
    {
        this.type = state.ToString();
    }

    public static JsonOBJ CreateFromJSON(string data)
    {
        try
        {
            return JsonUtility.FromJson<JsonOBJ>(data);
        }
        catch (Exception e)
        {
            Debug.Log("<color=green> Client: </color> JSON«¿«¤«×ªÇªÏª¢ªêªÞª»ªó¡£NULLªÇÚ÷ª·ªÞª¹");
            return null;
        }
    }
    public static string CreateToJSON(JsonOBJ data)
    {
        return JsonUtility.ToJson(data);
    }
}
