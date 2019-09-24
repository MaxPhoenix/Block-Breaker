using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLevel : MonoBehaviour {

    [SerializeField] int amountOfBlocks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddBlock(){
        this.amountOfBlocks++;
    }

    public void RemoveBlock(){

    }
}
