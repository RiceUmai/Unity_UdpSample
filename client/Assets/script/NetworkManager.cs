using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public enum eventType
{
    init,
    chat,
    Message,
}

public class NetworkManager : MonoBehaviour
{
    static private NetworkManager _instance;
    static public NetworkManager instance
    {
        get
        { 
            if(_instance == null)
            {
                return null;
            }
            else
            {
                return _instance;
            }
        }
    }

    [SerializeField]
    private string Addrass = "localhost";

    [SerializeField]
    private int Port = 6060;

    private UdpClient client;


    [SerializeField]
    GUITest gui;

    private void Awake()
    {
        if (instance != null) return;

        Connect(Addrass, Port);
    }

    void Start()
    {

    }

    void OnDestroy()
    {
        information.own_ip = null;
        information.own_port = 0;
        information.ribal_ip = null;
        information.ribal_port = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Adddrass"></param>
    /// <param name="Port"></param>
    void Connect(string Adddrass, int Port)
    {
        client = new UdpClient();


        try
        {
            _instance = this;
            client.Connect(Adddrass, Port);

            information.own_ip = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            information.own_port = ((IPEndPoint)client.Client.LocalEndPoint).Port;

            //byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
            //client.Send(sendBytes, sendBytes.Length);

            JSON Massafe = new JSON(eventType.init);
            Data_Send(Massafe);

            Thread thread = new Thread(() => Data_Resiving());
            thread.Start();
        }
        catch (Exception e)
        {
            client.Close();
            client.Dispose();
            _instance = null;

            print("Exception thrown " + e.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="Massage"></param>
    public void Data_Send(JSON Massage)
    {
        string msg = JSON.CreateToJSON(Massage);
        byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
        client.Send(sendBytes, sendBytes.Length);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Data_Resiving()
    {
        while (true)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, Port);
            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receiveBytes);
            print("Message received from the server \n " + receivedString);

            JSON data = JSON.CreateFromJSON(receivedString);

            if (data != null)
                evectHandler(data);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void evectHandler(JSON data)
    {
        eventType state = (eventType)Enum.Parse(typeof(eventType), data.type);

        switch (state)
        {
            case eventType.init :

                information.ribal_ip = data.ribal.address;
                information.ribal_port = data.ribal.port;
                break;

            default:
                break;
        }

    }
}
