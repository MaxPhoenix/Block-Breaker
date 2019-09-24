using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    //Configuration variables
    [SerializeField] float cameraHorizontalSize = 16f;
    [SerializeField] float paddleXMinPos = 1f;
    [SerializeField] float paddleXMaxPos = 15f;

    //cached variables
    GameSession gameSession;
    Ball ball;

    // Use this for initialization
    void Start () {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        //Calculate mouse position in world units
        float mouseXPosition = cameraHorizontalSize/2;
        Vector2 paddlePosition = new Vector2(mouseXPosition, transform.position.y);

        transform.position = paddlePosition;
        Debug.Log(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        //Calculate mouse position in world units

        Vector2 paddlePosition = new Vector2(GetXPos(), transform.position.y);
        
        //limit paddle position to both sides of the screen
        paddlePosition.x = Mathf.Clamp(GetXPos(),paddleXMinPos, paddleXMaxPos);

        //access transform component of the game object
        transform.position = paddlePosition;

	}

      private float GetXPos(){
        if(gameSession.IsAutoPlayEnabled()){
            return ball.transform.position.x;
        }
        else{
            return Input.mousePosition.x / Screen.width * cameraHorizontalSize;
        }
    }
}
