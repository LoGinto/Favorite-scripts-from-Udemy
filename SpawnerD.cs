using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour {

    Defender defender;

    private void OnMouseDown()
    {
        SpawnDefender(GetSquareClicked());//on mouse click spawn on clicked square
    }

    public void SetSelectedDefender(Defender selectedDefender)
    {
        defender = selectedDefender;
    }
    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);//input of mouse click position
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);//converting to integer
        return gridPos;//returning new position
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);//convert x to int
        float newY = Mathf.RoundToInt(rawWorldPos.y);//convert y to int
        return new Vector2(newX, newY);//return new vector 
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        Defender newDefender = Instantiate(defender, roundedPos, Quaternion.identity) as Defender;//actual spawning 
        Debug.Log(roundedPos);
    }

}
