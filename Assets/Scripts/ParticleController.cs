using System;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class ParticleController : MonoBehaviour
{
    public GameObject ParticleSystem;
    public string titleTest;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, World! We are titled " + titleTest);
        // Create Client instance
        string host = "broker.hivemq.com";
        MqttClient client = new MqttClient(host);
        Debug.Log("Connected to " + host);

        // Register to message received
        client.MqttMsgPublishReceived += client_recievedMessage;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // Subscribe to topic
        client.Subscribe(new String[] { "/sebcapstone" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        // System.Console.ReadLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            ParticleSystem.SetActive(true);
        } else
        {
            ParticleSystem.SetActive(false);
        }
    }

    void client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
    {
        // Handle message received
        var message = System.Text.Encoding.Default.GetString(e.Message);
        Debug.Log("Message received: " + message);
        if (message == "off")
        {
            active = false;
            Debug.Log("Received 'off' signal");
        }
        else if (message == "on")
        {
            active = true;
            Debug.Log("Received 'on' signal");
        }
    }
}
