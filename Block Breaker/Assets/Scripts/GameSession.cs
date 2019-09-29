using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour {

    //Range puts a slider in the inspector to set the game speed between a range of two values 
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;

    //state variables
    [SerializeField] int currentScore = 0;
    [SerializeField] int scorePerBlockdDestroyed = 100;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    /*
     *Method called before Start functions, the GameObject must be active
     * is also called when a Prefab is instanciated
     */
    private void Awake(){
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1){
            //if there is already another created, destroy this GameObject
            gameObject.SetActive(false); //to avoid bugs, objects that are not active won't be called Awke
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        this.currentScore = 0;
        this.scoreText.text = this.scoreText.text.Split(':')[0] + ": " + this.currentScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        //seting how fast the game will go
        Time.timeScale = gameSpeed;
        this.scoreText.text = this.scoreText.text.Split(':')[0] + ": " + this.currentScore.ToString();
    }

    public void AddToScore(){
        this.currentScore += this.scorePerBlockdDestroyed;
    }

    public void ResetGameSession(){
        Destroy(gameObject);
    }

  

    public bool IsAutoPlayEnabled(){
        return this.isAutoPlayEnabled;
    }
}
