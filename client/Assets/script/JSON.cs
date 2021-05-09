using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSON
{
    public string Ribal_ip;
    public string Ribal_port;

    public string type;
    public string uuid;
    public string name;
    public string message;

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


[Serializable]
public class JSON_gachar
{
    public string rank;
    public string id;
    public string num;

    public static JSON_gachar CreateFromJSON(string data)
    {
        try
        {
            return JsonUtility.FromJson<JSON_gachar>(data);
        }
        catch (Exception e)
        {
            Debug.Log("<color=green> Client: </color> JSON«¿«¤«×ªÇªÏª¢ªêªÞª»ªó¡£NULLªÇÚ÷ª·ªÞª¹");
            return null;
        }
    }
    public static string CreateToJSON(JSON_gachar data)
    {
        return JsonUtility.ToJson(data);
    }
}

