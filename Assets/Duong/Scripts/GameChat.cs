using TMPro;
using UnityEngine;
using WebSocketSharp;
using Photon.Pun;

public class GameChat : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public TMP_InputField inputField;

    private bool isInputFieldToggled;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !isInputFieldToggled)
        {
            isInputFieldToggled = true;
            inputField.Select();
            inputField.ActivateInputField();
            Debug.Log("Toggled on");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isInputFieldToggled)
        {
            isInputFieldToggled = false;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("Toggled off");
        }

        // Sending a message
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && isInputFieldToggled && !inputField.text.IsNullOrEmpty())
        {
            string messageToSend = $"{PhotonNetwork.LocalPlayer.NickName}: {inputField.text}";

            GetComponent<PhotonView>().RPC("SendChatMessage", RpcTarget.All, messageToSend);

            // Sending a message
            inputField.text = "";
            isInputFieldToggled = false;

            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

            Debug.Log(message: "Message sent");
        }

    }

    [PunRPC]
    public void SendChatMessage(string _message)
    {
        chatText.text = chatText.text + "\n" + _message;
    }
}