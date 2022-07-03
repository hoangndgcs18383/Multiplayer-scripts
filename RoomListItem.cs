using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text roomName;

    public RoomInfo roomInfo;
    public void SetUp(RoomInfo _info)
    {
        roomInfo = _info;
        roomName.text = _info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
