﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 8f;
    public float sensibility = 1;
    public bool paperFound = false;
    public int score = 0;

    Vector3 moveAmount;
    public float turn;
    Vector3 smoothMoveSpeed;

    Rigidbody rb;
    Transform tf;

    enum PlayerState
    {
        Walk, Attack, Jump
    }


    public bool attacking;
    public SkillBarController skillBar;

    public HealthBar healthBar;
    public int vidas;
    public int bajas = 0;
    public int muertos;
    public GameObject pickMessage;
    public float maxPickDistance = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        attacking = false;
        skillBar.SetAttackBar();
        healthBar.SetMaxHealth(vidas);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento adelante y atrás
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(0, 0, moveVertical).normalized;

        Vector3 targetMoveAmount = moveDir * playerSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveSpeed, .15f);

        //Move and face movement direction
        rb.MovePosition(rb.position + tf.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        tf.Rotate(0, moveHorizontal, 0);

        //Level_02 victory
        if (SceneManager.GetActiveScene().name == "Level_02")
        {
            GameObject item = GameObject.FindGameObjectWithTag("Item");
            if (item != null)
            {
                Vector3 paper_pos = item.GetComponent<Rigidbody>().position;
                float dist = Vector3.Magnitude(paper_pos - rb.position);
                Debug.Log("Distancia: " + dist);

                if (dist < maxPickDistance)
                {
                    //Show pick drop message
                    pickMessage.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        FindObjectOfType<GameManager>().Victory();
                    }

                }
                else
                {
                    pickMessage.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!attacking)
            {
                vidas = vidas - 1;
                healthBar.SetHealthBar(vidas);
                if (vidas < 1)
                {
                    FindObjectOfType<GameManager>().GameOver();
                }
            }
            else
            {
                //LEVEL_01
                if (SceneManager.GetActiveScene().name == "Level_01")
                {
                    muertos = muertos - 1;
                    if (muertos == 0)
                    {
                        FindObjectOfType<GameManager>().Victory();
                    }
                }

                //LEVEL_02
                if (SceneManager.GetActiveScene().name == "Level_02")
                {
                    bajas++;
                    Debug.Log("Kills: " + bajas);

                    if (bajas % GameObject.Find("Planet").GetComponent<EnemySpawner>().enemiesPerWave == 0
                    //if (GameObject.Find("Planet").GetComponent<EnemySpawner>().enemiesPerWave / bajas == 1
                    && GameObject.Find("Planet").GetComponent<EnemySpawner>().currentWave != GameObject.Find("Planet").GetComponent<EnemySpawner>().waves)
                    {
                        Debug.Log("Bajas: " + bajas + " .... Se acercan nuevos enemigos");
                        GameObject.Find("Planet").GetComponent<EnemySpawner>().SpawnEnemies();

                        //Ultima oleada dropear papel
                    }

                    if (other.gameObject.GetComponent<EnemyController>().paper)
                    {
                        other.gameObject.GetComponent<EnemyController>().DropPaper();
                    }
                }

                Destroy(other.gameObject);
            }
        }
    }

    void addPoints(int points)
    {
        this.score += points;
    }


}
