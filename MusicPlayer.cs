using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);//music through the game 
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();//Master volume float = volume
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
