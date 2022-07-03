using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    int numberInRoom;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            foreach (Player pl in PhotonNetwork.PlayerList)
            {
                if (pl != PhotonNetwork.LocalPlayer)
                    PV.RPC("CreateController", pl, numberInRoom);
                numberInRoom++;
            }
        }

        /*foreach (Player pl in PhotonNetwork.PlayerList)
        {
            if (pl != PhotonNetwork.LocalPlayer)
                PV.RPC("CreateController", pl, numberInRoom);
                numberInRoom++;
        }*/
    }

    [PunRPC]
    void CreateController(int index)
    {
        Debug.Log("Instantiated Player Controller");
        Transform spawnPoint = SpawnPointManager.Instance.GetSpawnPoint(index);

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnPoint.position , spawnPoint.rotation, 0 , new object[] { PV.ViewID });
    }
}
