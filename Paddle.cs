using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    // configuration paramaters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    GameStatus gameSession;
    ball theBall;

    // Use this for initialization
    void Start()
    {
        gameSession = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        //input position of mouse from x axis divided by units
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);//paddle transform vector
        paddlePos.x = Mathf.Clamp(xgetit(), minX, maxX);//The float result between the min and max values.
        transform.position = paddlePos;//positioning paddle according to vector
    }
    private float xgetit()
    {
        if (gameSession.AutoplayEnable())
        {
            return theBall.transform.position.x;

        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }


}