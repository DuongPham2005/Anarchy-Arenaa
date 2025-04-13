using Photon.Pun;
using UnityEngine;
using TMPro;

public class GameLauncher: MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        GameManagerScript.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
    }

    // Update is called once per frame
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        GameManagerScript.Instance.OpenMenu("Loading");

    }
    public override void OnJoinedRoom()
    {
        GameManagerScript.Instance.OpenMenu("room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        GameManagerScript.Instance.OpenMenu("Error");
    }
}
