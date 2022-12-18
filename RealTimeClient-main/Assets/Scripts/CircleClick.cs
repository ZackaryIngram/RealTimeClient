using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleClick : MonoBehaviour
{
    public int balloonID;
    void OnMouseDown()
    {
        NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.BalloonClicked + "," + balloonID);
        //Destroy(gameObject);
    }

    public void BalloonWasClicked(int balloonID)
    {

    }
}
