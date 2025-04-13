using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class GameLauncher: MonoBehaviourPunCallbacks
{
    public static GameLauncher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    private void Awake()
    {
        Instance = this;
    }
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
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public override void OnJoinedLobby()
    {
        GameManagerScript.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 100).ToString("000");

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
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient); 
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        GameManagerScript.Instance.OpenMenu("Error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        GameManagerScript.Instance.OpenMenu("lOADING...");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        GameManagerScript.Instance.OpenMenu("loading");

        
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        {
            GameManagerScript.Instance.OpenMenu("title");
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i< roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
