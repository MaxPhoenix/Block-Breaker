using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] Paddle paddle;
    Vector2 distancePaddleBall;
    bool hasStarted = false;
    //Audio Files
    [SerializeField] AudioClip[] ballSounds;
    AudioSource ballAudioSource;
    [SerializeField] float randomFactor = 1f;
    Vector2 lastBouncePosition;
    Rigidbody2D ballRigidBody;
    Vector3 force;
    Vector2 screenLimits;
    short xLoopCounter;
    short yLoopCounter;

    // Use this for initialization
    void Start() {
        Vector2 newVector = transform.position;
        distancePaddleBall = transform.position - paddle.transform.position;
        
        transform.position = (newVector - distancePaddleBall);
        ballAudioSource = GetComponent<AudioSource>();

        ballRigidBody = GetComponent<Rigidbody2D>();
        force = new Vector3(1f , 1f, 0);

        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
        screenLimits = new Vector2(0,0);
        screenLimits.x = screenBounds.x;
        screenLimits.y = screenBounds.y;
    }
	
	// Update is called once per frame
	void Update (){
        if(!hasStarted)
            LockBallToPaddle();
        LunchBallOnMouseClick();
    }

    private void LunchBallOnMouseClick(){
        //if left click is pressed, move the rigid body component attached to the game object
        if (Input.GetMouseButtonDown(0) && !hasStarted){
            ballRigidBody.velocity = new Vector2(1f,10f);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle(){
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + distancePaddleBall;
    }

    

    private bool IsBallInYAxisLoop(){
        Vector2 currentPosition = transform.position;
        return (int)Math.Truncate(currentPosition.y) == (int)Math.Truncate(lastBouncePosition.y);
    }

    private bool IsBallInXAxisLoop(){
       Vector2 currentPosition = transform.position;
       return (int)Math.Truncate(currentPosition.x) == (int)Math.Truncate(lastBouncePosition.x);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        //obtain the AudioSource component attached to this game object and play its audioClip component
        if (hasStarted){
            AudioClip audioClip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            //playOneShot is used to play a sound and to not be interrupted by other ones
            ballAudioSource.PlayOneShot(audioClip);
            
            if(IsBallInXAxisLoop()){
                if(xLoopCounter >= 3)
                    AddForceToBall(true);
                else 
                    xLoopCounter++;
            }
            else if(IsBallInYAxisLoop()){
                if(yLoopCounter >= 3)
                    AddForceToBall(false);
                else
                    yLoopCounter++;
            }
            lastBouncePosition = transform.position;
        }
    }

    private void AddForceToBall(bool isXLoop){
        /*Add force to the gameobject so it can move out of the loop bouncing and remove it to restore the object's 
        initial velocity */
        if(isXLoop){
            bool isBallAtScreenWitdhLimit = (int)Math.Truncate(transform.position.x) == (int)Math.Truncate(screenLimits.x);
            bool isBallAtScreenWitdhStart = (int)Math.Truncate(transform.position.x) == 0;
            if( isBallAtScreenWitdhLimit || isBallAtScreenWitdhStart )
                force.x *= -1;
            xLoopCounter = 0;
            ballRigidBody.AddForce(new Vector3(force.x, 0, 0), ForceMode2D.Impulse);
        }
        else{
            bool isBallAtScreenHeightLimit = (int)Math.Truncate(transform.position.y) == (int)Math.Truncate(screenLimits.y);
            bool isBallAtScreenHeightStart = (int)Math.Truncate(transform.position.y) == 0;
            if( isBallAtScreenHeightLimit || isBallAtScreenHeightStart )
                force.y *= -1;
            yLoopCounter = 0;
            ballRigidBody.AddForce(new Vector3(0, force.y, 0), ForceMode2D.Impulse);
        }
        
        
    }
}
