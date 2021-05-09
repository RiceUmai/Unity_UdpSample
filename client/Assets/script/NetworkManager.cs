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

    public static int Ribal_port;
    public static int Own_port;

    [SerializeField]
    private string Addrass = "localhost";

    [SerializeField]
    private int Port = 6060;

    private UdpClient client;


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

            Own_port = ((IPEndPoint)client.Client.LocalEndPoint).Port;

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
                Debug.Log(data.Ribal_ip);
                Debug.Log(data.Ribal_port);
                break;

            default:
                break;
        }

    }
}
