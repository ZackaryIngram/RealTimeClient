using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    Sprite circleTexture;
    private LinkedList<GameObject> activeBalloons;

    void Start()
    {
        NetworkedClientProcessing.SetGameLogic(this);
        activeBalloons = new LinkedList<GameObject>();
    }
    public void SpawnNewBalloon(float xPosPercent, float yPosPercent, int balloonID)
    {
        Vector2 screenPosition = new Vector2(xPosPercent * (float)Screen.width, yPosPercent * (float)Screen.height);
        if (circleTexture == null)
            circleTexture = Resources.Load<Sprite>("Circle");

        GameObject balloon = new GameObject("Balloon");

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
        balloon.AddComponent<CircleClick>();
        balloon.AddComponent<CircleCollider2D>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
        pos.z = 0;
        balloon.transform.position = pos;

        balloon.GetComponent<CircleClick>().balloonID = balloonID;
        activeBalloons.AddLast(balloon);
    }

    public void BalloonWasPopped(int balloonID)
    {
        GameObject popMe = null;

        foreach (GameObject circ in activeBalloons)
        {
            if (circ.GetComponent<CircleClick>().balloonID == balloonID)
            {
                popMe = circ;
                break;
            }
        }

        if (popMe != null)
        {
            activeBalloons.Remove(popMe);
            Destroy(popMe);
        }
    }
}

