// using UnityEngine;
// using System.Net;
// using System.Net.Sockets;
// using System.Text;

// public class GitaLiveStreamer : MonoBehaviour
// {
//     [Header("Coordinate Mapping")]
//     public float scaleFactor = 0.333f;
//     public float updateRate = 10f;
//     private Vector3 startPosition;
    
//     [Header("Network Streaming")]
//     public string macIP = "10.30.132.163"; // Find with: ifconfig | grep "inet "
//     public int port = 8888;
    
//     private float updateTimer;
//     private UdpClient udpClient;
//     private IPEndPoint macEndPoint;

//     void Start()
//     {
//         startPosition = transform.position;
        
//         // Origin marker
//         GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         marker.transform.position = startPosition;
//         marker.transform.localScale = Vector3.one * 0.3f;
//         marker.GetComponent<Renderer>().material.color = Color.red;
        
//         // Setup network - send to Mac
//         udpClient = new UdpClient();
//         macEndPoint = new IPEndPoint(IPAddress.Parse(macIP), port);
        
//         Debug.Log($"Streaming to Mac at {macIP}:{port}");
//     }

//     void Update()
//     {
//         updateTimer += Time.deltaTime;
        
//         if (updateTimer >= 1f / updateRate)
//         {
//             SendPosition();
//             updateTimer = 0f;
//         }
//     }

//     void SendPosition()
//     {
//         Vector3 relativePos = transform.position - startPosition;
//         Vector3 scaledPos = relativePos * scaleFactor;
        
//         GitaCommand command = new GitaCommand
//         {
//             x = scaledPos.x,
//             y = scaledPos.y,
//             z = scaledPos.z,
//             yaw = transform.eulerAngles.y,
//             timestamp = Time.time,
//             unity_x = relativePos.x,
//             unity_z = relativePos.z,
//             scale_factor = scaleFactor
//         };
        
//         string json = JsonUtility.ToJson(command);
//         byte[] data = Encoding.UTF8.GetBytes(json);
        
//         try
//         {
//             udpClient.Send(data, data.Length, macEndPoint);
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogWarning($"Send failed: {e.Message}");
//         }
//     }

//     void OnDestroy()
//     {
//         udpClient?.Close();
//     }
// }

// [System.Serializable]
// public class GitaCommand
// {
//     public float x;
//     public float y;
//     public float z;
//     public float yaw;
//     public float timestamp;
//     public float unity_x;
//     public float unity_z;
//     public float scale_factor;
// }






// using UnityEngine;
// using System.Net;
// using System.Net.Sockets;
// using System.Text;

// public class GitaLiveStreamer : MonoBehaviour
// {
//     [Header("Coordinate Mapping")]
//     public float scaleFactor = 0.333f;
//     public float updateRate = 10f;
//     private Vector3 startPosition;
    
//     [Header("Network Streaming")]
//     public string laptopIP = "10.34.163.45"; // "10.30.132.163"; "10.34.175.255"
//     public int port = 8888;
    
//     private float updateTimer;
//     private UdpClient udpClient;
//     private IPEndPoint laptopEndPoint;

//     void Start()
//     {
//         startPosition = transform.position;
        
//         // Origin marker
//         GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         marker.transform.position = startPosition;
//         marker.transform.localScale = Vector3.one * 0.3f;
//         marker.GetComponent<Renderer>().material.color = Color.red;
        
//         // Setup network
//         udpClient = new UdpClient();
//         laptopEndPoint = new IPEndPoint(IPAddress.Parse(laptopIP), port);
        
//         Debug.Log($"Streaming to laptop at {laptopIP}:{port}");
//     }

//     void Update()
//     {
//         updateTimer += Time.deltaTime;
        
//         if (updateTimer >= 1f / updateRate)
//         {
//             SendPosition();
//             updateTimer = 0f;
//         }
//     }

//     void SendPosition()
//     {
//         Vector3 relativePos = transform.position - startPosition;
//         Vector3 scaledPos = relativePos * scaleFactor;
        
//         GitaCommand command = new GitaCommand
//         {
//             x = scaledPos.x,
//             y = scaledPos.y,
//             z = scaledPos.z,
//             yaw = transform.eulerAngles.y,
//             timestamp = Time.time,
//             unity_x = relativePos.x,
//             unity_z = relativePos.z,
//             scale_factor = scaleFactor
//         };
        
//         // Send over network
//         string json = JsonUtility.ToJson(command);
//         byte[] data = Encoding.UTF8.GetBytes(json);
        
//         try
//         {
//             udpClient.Send(data, data.Length, laptopEndPoint);
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogWarning($"Send failed: {e.Message}");
//         }
//     }

//     void OnDestroy()
//     {
//         udpClient?.Close();
//     }
// }

// [System.Serializable]
// public class GitaCommand
// {
//     public float x;
//     public float y;
//     public float z;
//     public float yaw;
//     public float timestamp;
//     public float unity_x;
//     public float unity_z;
//     public float scale_factor;
// }


using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class GitaLiveStreamer : MonoBehaviour
{
    [Header("Coordinate Mapping")]
    public float scaleFactor = 0.333f;
    public float updateRate = 10f;
    private Vector3 startPosition;
    
    [Header("Network Streaming")]
    public string laptopIP = "10.136.92.84"; 
    public int port = 8888;
    
    private float updateTimer;
    private UdpClient udpClient;
    private IPEndPoint laptopEndPoint;
    private int packetsSent = 0;

    void Start()
    {
        
        startPosition = transform.position;
        
        // Origin marker - BIGGER so you can see it
        // GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // marker.transform.position = startPosition;
        // marker.transform.localScale = Vector3.one * 0.5f; // Bigger
        // marker.GetComponent<Collider>().enabled = false; // No collisions
        // marker.GetComponent<Renderer>().material.color = Color.red;

        Vector3 marker = startPosition;
        
        // Setup network
        try
        {
            udpClient = new UdpClient();
            laptopEndPoint = new IPEndPoint(IPAddress.Parse(laptopIP), port);
            Debug.Log("=== GITA STREAMER STARTED ===");
            Debug.Log($"Target: {laptopIP}:{port}");
            Debug.Log($"Start Position: {startPosition}");
            Debug.Log("=============================");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to setup UDP: {e.Message}");
        }
    }

    void Update()
    {
        updateTimer += Time.deltaTime;
        
        if (updateTimer >= 1f / updateRate)
        {
            SendPosition();
            updateTimer = 0f;
        }
    }

    void SendPosition()
    {
        Vector3 relativePos = transform.position - startPosition;
        Vector3 scaledPos = relativePos * scaleFactor;
        
        GitaCommand command = new GitaCommand
        {
            x = scaledPos.x,
            y = scaledPos.y,
            z = scaledPos.z,
            yaw = transform.eulerAngles.y,
            timestamp = Time.time,
            unity_x = relativePos.x,
            unity_z = relativePos.z,
            scale_factor = scaleFactor
        };
        
        string json = JsonUtility.ToJson(command);
        byte[] data = Encoding.UTF8.GetBytes(json);
        
        try
        {
            int bytesSent = udpClient.Send(data, data.Length, laptopEndPoint);
            packetsSent++;
            
            // Log every 50 packets so we know it's working
            if (packetsSent % 50 == 0)
            {
                Debug.Log($"✓ Sent {packetsSent} packets. Last: ({scaledPos.x:F2}, {scaledPos.z:F2})");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Send failed on packet {packetsSent}: {e.Message}");
        }
    }

    void OnDestroy()
    {
        Debug.Log($"Total packets sent: {packetsSent}");
        udpClient?.Close();
    }
}

[System.Serializable]
public class GitaCommand
{
    public float x, y, z, yaw, timestamp, unity_x, unity_z, scale_factor;
}