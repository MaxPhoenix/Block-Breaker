using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltBlock : MonoBehaviour {

    [SerializeField] AudioClip breakSound;
    AltLevel level;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        //gameObject means this gameObject
        Destroy(gameObject);
        level.RemoveBlock();
    }

	// Use this for initialization
	void Start () {
        level = FindObjectOfType<AltLevel>();
        level.AddBlock();
	}
	
	
}
