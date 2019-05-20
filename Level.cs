using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableblocks;//for debug

    SceneLoader sceneloader;//cache 
    public void CountBreakeableBlocks()
    {
        breakableblocks++;
    }

    public void BlockDestroyed()
    {
        breakableblocks--;
        if (breakableblocks<=0)
        {
            sceneloader.LoadNextScene();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
