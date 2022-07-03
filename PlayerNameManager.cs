using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] InputField nameInputField;

    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            nameInputField.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
        else
        {
            nameInputField.text = "Player" + Random.Range(0, 10000).ToString("00000");
        }
    }

    public void OnUsernameInputValueChanged()
    {
        PhotonNetwork.NickName = nameInputField.text;
        PlayerPrefs.SetString("username", nameInputField.text);
    }
}
