using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class _SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.NickName = "Player";
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinedRoom()
    {
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);

        /* if (PhotonNetwork.IsMasterClient) {
             PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
         }*/
    }
}