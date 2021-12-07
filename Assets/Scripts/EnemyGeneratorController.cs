using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float generatorTimer = 4f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy(){
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    //Inicia el generador de enemigos repitiendo
    public void StartGenerator(){
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }

    // Cancela el generador de enemigos
    public void CancelGenerator(bool clean = false){
        CancelInvoke("CreateEnemy");
        if(clean){
            object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach( GameObject enemy in allEnemies){
                Destroy(enemy);
            }
        }
    }
}
