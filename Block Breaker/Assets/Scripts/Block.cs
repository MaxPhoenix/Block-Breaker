using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesFX;
    //Sprites used to show damage levels
    [SerializeField] Sprite[] hitSprites;

    //cached game status
    GameSession gameStatus;
    float soundVolume = 0.2f;
    int timesHit;
    int maxHits;

    private void Start(){
        gameStatus = FindObjectOfType<GameSession>();
        maxHits = hitSprites.Length + 1;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(tag == "Breakable")
            HandleBlockHit();  
    }

    private void HandleBlockHit(){
        timesHit++;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
            DisplayDifferentHitSprite();
    }

    private void DisplayDifferentHitSprite(){
        int spriteIndex = timesHit-1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock(){
        //creates a temporary audiosource which is destroyed after the sound is finished
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, soundVolume);
        TriggerSparklesFX();
        //gameObject means this gameObject
        Destroy(gameObject);
        gameStatus.AddToScore();
    }

    private void TriggerSparklesFX(){
        //instantiate is used to create a copy of an object 
        GameObject sparkles = Instantiate(blockSparklesFX,transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
