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

//struct TransformData
//{
//    Transform trsform;
//    bool jump;
//    bool shot;
//}

public class ClientNetwork : MonoBehaviour {

    TransformData tData = new TransformData();

    const int PORT_NUM = 57777;
    //IPEndPoint serverIP = new IPEndPoint(IPAddress.Parse("117.123.188.13"), PORT_NUM);
    //IPEndPoint serverIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT_NUM);
    IPEndPoint serverIP;
    //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    //UdpClient client = new UdpClient("117.123.188.13", PORT_NUM);
    //TcpClient client = new TcpClient("117.123.188.13", PORT_NUM +1 );
    TcpClient client;
    NetworkStream ns;
    GameObject player;
    

    private void Awake()
    {
        serverIP = new IPEndPoint(IPAddress.Parse("117.123.188.13"), PORT_NUM);
        client = new TcpClient("117.123.188.13", PORT_NUM);
        ns = client.GetStream();
    }


    public void ConnectToServer()
    {
        client.Connect(serverIP);
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null)
        {
            //tData.pos = player.transform.position;
            //tData.rot = player.transform.rotation.eulerAngles;
            //tData.camRotX = GameObject.FindGameObjectWithTag("pCamera").transform.rotation.x;
            tData.posX = player.transform.position.x;
            tData.posY = player.transform.position.y;
            tData.posZ = player.transform.position.z;
            tData.rotX = GameObject.FindGameObjectWithTag("pCamera").transform.rotation.x;
            tData.rotY = player.transform.rotation.y;
            tData.rotZ = player.transform.rotation.z;

            //var buffer = new byte[Marshal.SizeOf(typeof(TransformData))];
            //var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            //var pBuffer = gch.AddrOfPinnedObject();

            //Marshal.StructureToPtr(buffer, pBuffer, false);
            //client.Send(buffer, buffer.Length, serverIP);
            
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ns, tData);
            //gch.Free();

            //byte[] bytes = client.Receive(ref serverIP);

            //TransformData rData = (TransformData)formatter.Deserialize(ns);

            //NetworkStream ns = server.getStream();
            //formatter.Serialize(ns, tData);
            
            
            //byte[] bytes = tData;
            //client.SendTo()

        }
    }



}
