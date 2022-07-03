using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserNameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView playerPV;
    [SerializeField] Text username;

    private void Start()
    {
        if (playerPV.IsMine)
        {
            gameObject.SetActive(false);
        }

        username.text = playerPV.Owner.NickName;
    }
}
