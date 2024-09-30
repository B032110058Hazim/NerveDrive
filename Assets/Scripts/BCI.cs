using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmotivUnityPlugin;

public class BCI : MonoBehaviour
{
    //Client information
    [SerializeField] string clientId;
    [SerializeField] string clientSecret;
    //App information
    [SerializeField] string appName;
    [SerializeField] string appVersion;
    //Headset information
    [SerializeField] string headsetId;
    //Profile information
    [SerializeField] string profileName;

    EmotivUnityItf eItf = EmotivUnityItf.Instance;

    public static string action;
    public static double power;

    void Start()
    {
        StartCoroutine(Setup());
    }

    void Update()
    {
        if(eItf.LatestMentalCommand.act != "NULL")
        {
            action = eItf.LatestMentalCommand.act;
            power = eItf.LatestMentalCommand.pow;
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Stopping Emotiv Unity Interface");
        eItf.Stop();
    }

    IEnumerator Setup()
    {
        //Initialize and start authorization based on provided information
        eItf.Init(clientId, clientSecret, appName, appVersion);
        eItf.Start();

        //Wait until authorized, scan headsets
        while (!eItf.IsAuthorizedOK) { yield return null; }
        DataStreamManager.Instance.ScanHeadsets();
        
        //Wait until headset scanning finishes, then create session
        while (DataStreamManager.Instance.IsHeadsetScanning) { yield return null; }
        eItf.CreateSessionWithHeadset(headsetId);

        //Once session is created, then subscribe to data stream(s)
        while (!eItf.IsSessionCreated) { yield return null; }
        eItf.SubscribeData(new List<string> { "com" });

        yield break;
    }
}