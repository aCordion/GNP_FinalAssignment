using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[SerializeField]
[StructLayout(LayoutKind.Sequential)]
public struct TransformData
{
    //public Vector3 pos;
    //public Vector3 rot;
    [MarshalAs(UnmanagedType.R4)]
    public float posX;
    [MarshalAs(UnmanagedType.R4)]
    public float posY;
    [MarshalAs(UnmanagedType.R4)]
    public float posZ;
    [MarshalAs(UnmanagedType.R4)]
    public float rotX;
    [MarshalAs(UnmanagedType.R4)]
    public float rotY;
    [MarshalAs(UnmanagedType.R4)]
    public float rotZ;
    [MarshalAs(UnmanagedType.R4)]
    public float camRotX;
    [MarshalAs(UnmanagedType.Bool)]
    public bool jump;
    [MarshalAs(UnmanagedType.Bool)]
    public bool shot;
}

struct ClientData
{
    public string name;
    public bool state;
    public IPEndPoint ip;
    public NetworkStream ns;
    //public Socket sock;
    //public TcpClient TcpClient;
    //ClientData(int id, string name, bool state, IPEndPoint ipep, Socket socket)
    //{
    //    cID = id;
    //    this.name = name;
    //    this.state = state;
    //    ip = ipep;
    //    sock = socket;
    //}
}

public class ServerNet : MonoBehaviour {

    enum ServerState
    {
        Loading,
        Live,
        Closed
    };

    const int PORT_NUM = 57777;

    //GameData gData;
    IPEndPoint serverIPEP = new IPEndPoint(IPAddress.Any, PORT_NUM);
    //Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    TcpListener listener = new TcpListener(IPAddress.Any, PORT_NUM);
    //TcpClient client = new TcpClient();

    //NetworkStream ns;

    //List<IPEndPoint> clientIPEP = new List<IPEndPoint>();
    //List<Socket> clients = new List<Socket>();
    //List<string> clientName = new List<string>();
    //List<bool> bClients = new List<bool>();
    List<Thread> tRecv = new List<Thread>();
    List<ClientData> clients = new List<ClientData>();
    List<GameObject> players = new List<GameObject>();
    List<NetworkStream> ns = new List<NetworkStream>();
    List<TcpClient> clientTCP = new List<TcpClient>();

    int clientCount = 0;
    int MAX_CLIENTS = 8;


    [SerializeField]
    ServerState state;
    UnityEngine.Object playerPrefab;
    GameObject spawnPoint;
    //GameObject[] spawnPoints;

    private void Awake()
    {
        state = ServerState.Closed;
        //gData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        //server.Bind(serverIPEP);
        listener = new TcpListener(IPAddress.Any, PORT_NUM);
        listener.Start();


        playerPrefab = Resources.Load("Prefabs/RemotePlayer");
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        //spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        //for (int i = 0; i < MAX_CLIENTS; i++)
        //{
        //    tRecv.Add(new Thread(new ParameterizedThreadStart(RecvNSendFunc)));
        //}
        //for (int i = 0; i < MAX_CLIENTS; ++i)
        //{
        //    ClientData cd = new ClientData();
        //    cd.cID = i;
        //    cd.name = "Player" + i;
        //    cd.state = false;
        //    cd.ip = new IPEndPoint(IPAddress.Any, 0);
        //    cd.sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //    clients.Add(cd);
        //}
        new Thread(NewClient).Start();
    }

    void NewClient()
    {
        TcpClient temp = listener.AcceptTcpClient();
        NetworkStream nsSelf = temp.GetStream();
        IPEndPoint ipep = (IPEndPoint)temp.Client.RemoteEndPoint;
        GameObject player = new GameObject(ipep.ToString())
        {
            tag = ipep.ToString()
        };
        GameObject character = (GameObject)Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, player.transform);
        PlayerControlRemote pcr = character.GetComponent<PlayerControlRemote>();
        //clientTCP.Add(temp);
        //ns.Add(nsSelf);
        //players.Add(player);

        if(clients.Count < 7)
        {
            new Thread(NewClient).Start();
        }
        while (true)
        {

            var buffer = new byte[Marshal.SizeOf(typeof(TransformData))];

            int recved = 0;

            while((recved = nsSelf.Read(buffer, 0, buffer.Length)) > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                TransformData recvData = (TransformData)formatter.Deserialize(nsSelf);
                pcr.SetTransforms(recvData);
            }
            
        }
        temp.Close();

        //while (true)
        //{
        //    IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
        //    UdpClient udpCli = new UdpClient();
        //    byte[] bytes = udpCli.Receive(ref epRemote);
        //    if (bytes != null)
        //    {
        //        bool newOne = true;
        //        //int num = 0;
        //        int temp = 0;
        //        foreach (ClientData c in clients)
        //        {
                    
        //            if (c.ip == epRemote)
        //            {
        //                newOne = false;
        //            }
        //            temp++;
        //        }
        //        if (newOne == true && clients.Count < 8)
        //        {
        //            ClientData cd = new ClientData
        //            {
        //                ip = epRemote,
        //                state = true,
        //                name = "Player" + clientCount++.ToString(),
        //                //sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        //            };
        //            clients.Add(cd);
        //            Thread tTemp = new Thread(new ParameterizedThreadStart(RecvNSendFunc));
        //            tTemp.Start(cd);
        //            tRecv.Add(tTemp);

                    

        //        }
        //    }

        //    Thread.Sleep(5);
        //}
    }

    void CreateNewPlayer(ClientData client)
    {
        Debug.Log("NewPlayerCreating");
        GameObject player = new GameObject(client.ip.ToString())
        {
            tag = client.ip.ToString()
        };
        GameObject character = (GameObject)Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, player.transform);
        players.Add(player);
    }

    void RecvNSendFunc(object client)
    {
        ClientData cInfo = (ClientData)client;
        Debug.Log("void RecvNSendFunc() enter");
        while (true)
        {
            EndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = new byte[1024];
            //int recv = server.ReceiveFrom(bytes, ref epRemote);

            
            //todo
            Thread.Sleep(5);
        }
    }


    private void FixedUpdate()
    {
        if (players != null)
        {
            switch (state)
            {
                case ServerState.Loading:
                    break;

                case ServerState.Live:
                    break;

                case ServerState.Closed:
                    break;

                default:
                    break;
            }
        }
    }

    void BroadCast()
    {
        //byte[] bytes;
        //bytes = TransformData
        for (int i = 0; i < clients.Count; i++)
        {
          //  server.Send()
        }
    }

}
