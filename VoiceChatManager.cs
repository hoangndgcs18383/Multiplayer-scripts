using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using System;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    string appID = "ace915f9c77d4f57afc43bfe30512204";

    public static VoiceChatManager Instance;

    IRtcEngine rtcEngine;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        rtcEngine = IRtcEngine.GetEngine(appID);

        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += OnError;

        rtcEngine.EnableSoundPositionIndication(true);
    }

    void OnError(int error, string msg)
    {
        Debug.Log("Error with Argo " + msg);
    }

    void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Leave channel with duration " + stats.duration);
    }

    void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Chanel name " + channelName);

        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
    }

    public override void OnJoinedRoom()
    {
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }

    private void OnDestroy()
    {
        IRtcEngine.Destroy();
    }
}
