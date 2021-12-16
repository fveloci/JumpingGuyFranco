using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;
    public GameObject enemyGenerator;
    public GameObject[] hearts;
    private int life;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = game.GetComponent<GameController>().gameState == GameState.Playing;
        if(gamePlaying &&(Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))){
            UpdateState("PlayerJump");
        }
    }

    public void UpdateState(string state = null){
        if( state != null){
            animator.Play(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){            
            if(life < 1)
            {
                UpdateState("PlayerDie");
                game.GetComponent<GameController>().gameState = GameState.Ended;
                enemyGenerator.SendMessage("CancelGenerator", true);
                game.SendMessage("ResetTimeScale", 0.5f);
                game.GetComponent<AudioSource>().Stop();
                game.SendMessage("ShowLoserText");
            }else
            {
                life --;
                Destroy(hearts[life].gameObject);
            }            
        }else if(other.gameObject.tag == "Point"){
            game.SendMessage("IncreasePoints");
        }
        
    }

    // Setea el juego a listo para comenzar
    void GameReady(){
        game.GetComponent<GameController>().gameState = GameState.Ready;
    }
}
