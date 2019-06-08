using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerD : MonoBehaviour
{
    [SerializeField] GameObject def;//defender game object
    private void OnMouseDown()
    {
        //Debug.Log("Clicked");
        DSpawn(GetClicked());//spawn
    }
    private Vector2 GetClicked()
    {
        Vector2 posclick = new Vector2(Input.mousePosition.x, Input.mousePosition.y);//getting click coordinates
        Vector2 rawgrid = Camera.main.ScreenToWorldPoint(posclick);//convert it to understandable x,y
        Vector2 gridPos = AttachToGrid(rawgrid);//calling  coordinate conversion method 
        return gridPos;
    }
    private Vector2 AttachToGrid(Vector2 nogridpos)
    {
        float newX = Mathf.RoundToInt(nogridpos.x);//converting the float position to integer
        float newY = Mathf.RoundToInt(nogridpos.y);//convert the float position to integer
        return new Vector2(newX, newY);

    }
    private void DSpawn(Vector2 ConvertedParameter)
    {
        //coordinates = GetClicked();
        GameObject defender = Instantiate(def,ConvertedParameter,transform.rotation
            ) as GameObject;//actual spawning 

    }
}
