using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState {Idle, Playing, Ended, Ready};

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.04f;
    public RawImage background;
    public RawImage platform;
    public GameObject uiIdle;
    public GameObject uiScore;
    public Text pointsText;
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    public float scaleTime = 6f;
    public float scaleInc = .25f;

    private AudioSource musicPlayer;
    private int points = 0;
    
    // Random for game speed   
    private float[] speed = {1f, 1.2f, 1.4f, 1.6f, 1.7f, 1.8f, 1.9f, 2f, 2.1f, 2.2f, 2.3f};
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
        //Empieza el juego
        if(gameState == GameState.Idle && userAction)
        {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();            
        }
        // Juego en marcha
        else if(gameState == GameState.Playing)
        {
            Parallax();
        }        
        // Juego preparado para reiniciarse
        else if (gameState == GameState.Ready){
            if(userAction){
                RestartGame();
            }
        }
    }

    void Parallax(){
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }
    public void RestartGame(){
        ResetTimeScale();
        SceneManager.LoadScene("SampleScene");
    }

    void GameTimeScale(){          
        int rand = Random.Range(0,10);
        Debug.Log("Rand: "+rand+" Speed: "+ speed[rand]);
        Time.timeScale = speed[rand];
        Debug.Log("Ritmo incrementado: "+ Time.timeScale.ToString());
    }
    public void ResetTimeScale(float newTimeScale = 1f){
        CancelInvoke("GameTimeScale");
        Time.timeScale = newTimeScale;
        Debug.Log("Ritmo reestablecido: "+ Time.timeScale.ToString());
    }
    public void IncreasePoints(){        
        pointsText.text = (++points).ToString();
        if(points%3==0){
            GameTimeScale();
        }
    }
}
