using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // variables
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
   // [SerializeField] int maxHits;//hp
    [SerializeField] Sprite[] hitSprites;//array of sprites for the destruction

    
    Level level;

    // state variables
    [SerializeField] int timesHit;  // debug

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();//preparing to geth the method from level
        if (tag == "Breakable")//checking the tag
        {
            level.CountBreakeableBlocks();//getting the method 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            CheckHit();
        }
    }

    private void CheckHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;//hp
        if (timesHit >= maxHits)//if maximum hits are achieved
        {
            DestroyBlock();
        }
        else
        {
            NextSprite();//show the next sprite from array
        }
    }

    private void NextSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("No block sprite in array."+gameObject.name);//debug + indication of game object name

        }
        //getting the next index
    }

    private void DestroyBlock()
    {
        DestroySFX();//playing the sound of destruction
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();//visual effect 
    }

    private void DestroySFX()//method that plays the sound
    {
        FindObjectOfType<GameStatus>().AddScore();

        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);//play the sound on camera's transform position
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);//create sparkles on the block's transform form
        Destroy(sparkles, 1f);//destroy the VFX
    }

}