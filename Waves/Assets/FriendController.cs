using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    public GameObject slider;
    public int vidas;
    public Rigidbody rb;
    public Transform tf;
    private Color attackedColor;
    public bool hasPaper;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        this.slider.GetComponent<Transform>().position = tf.localPosition + new Vector3(0, 2.5f, 3);
        this.SetMaxHealth(5); ///Dificultad?
        this.vidas = 5;
        this.hasPaper = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement???
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            SetHealthBar(vidas - 1);
            if (vidas < 1)
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.GetComponent<Slider>().maxValue = maxHealth;
        slider.GetComponent<Slider>().value = maxHealth;
    }

    public void SetHealthBar(int vida)
    {
        this.vidas = vida;
        slider.GetComponent<Slider>().value = vida;
    }
}
