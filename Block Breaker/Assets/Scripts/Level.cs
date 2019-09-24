using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    Block[] levelBlocks;
    List<Block> blockList;
    Block blockToRemove;
    int blockCount;
    SceneLoader sceneLoader;

	// Use this for initialization
	void Start () {
        sceneLoader = FindObjectOfType<SceneLoader>();
        //obtain all gameObjects of type Block in the scene
        levelBlocks = GameObject.FindObjectsOfType<Block>();

        //turn array into list to manipulate it
        blockList = new List<Block>(levelBlocks);
        RemoveUnbreakableBlocks();
        blockCount = blockList.Capacity;
	}
	
	// Update is called once per frame
	void Update (){
        //when a gameObject is destroyed, its reference in a collection turns "null"
        RemoveDestroyedBlocks();

        blockCount = blockList.Count;
        if (blockCount <= 0){
            sceneLoader.LoadNextScene();
        }
    }

    private void RemoveDestroyedBlocks(){
        blockList.RemoveAll(block => block == null);
    }

    private void RemoveUnbreakableBlocks(){
        //keep only breakable blocks
       blockList = blockList.FindAll(block => block.tag == "Breakable");
    }
}
