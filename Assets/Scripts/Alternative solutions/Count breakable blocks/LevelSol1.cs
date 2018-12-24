using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSol1 : MonoBehaviour{

    private Block [] levelBlocksArray;
    private List<Block> levelBlocksList = new List<Block>();
    private int blockCount;

    [SerializeField] SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start(){
        levelBlocksArray = GameObject.FindObjectsOfType<Block>();
        foreach (Block block in levelBlocksArray){
            levelBlocksList.Add(block);
        }
        

    blockCount = levelBlocksList.Count;
    }

    // Update is called once per frame
    void Update(){
        checkBlockDestruction();

    }

    private void checkBlockDestruction(){
        for (int i = 0; i < levelBlocksList.Count; i++){
            if(levelBlocksList[i] == null){
                levelBlocksList.Remove(levelBlocksList[i]);
                blockCount = levelBlocksList.Count;
            }
        }

        if(blockCount  == 0){
            sceneLoader.LoadNextScene();
        }
    }

}
