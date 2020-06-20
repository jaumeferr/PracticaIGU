using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    [HideInInspector]
    public Slider slider;
    public int vidas;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Transform tf;
    private Color attackedColor;
    public bool hasPaper;
    private float damageDelay;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        slider = GameObject.Find("FriendLife").GetComponent<Slider>();
        this.SetMaxHealth(Variables.maxNPCLifes); ///Dificultad?
        this.vidas = Variables.maxNPCLifes;
        this.hasPaper = false;
        this.damageDelay = Variables.maxDamageDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement???
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (this.damageDelay == Variables.maxDamageDelay)
            {
                SetHealthBar(vidas - 1);
                if (vidas < 1)
                {
                    FindObjectOfType<GameManager>().GameOver();
                }
                this.damageDelay -= Time.deltaTime;
            }

            if (this.damageDelay <= 0)
            {
                this.damageDelay = Variables.maxDamageDelay;
            }

            if ((this.damageDelay > 0) && (this.damageDelay < Variables.maxDamageDelay))
            {
                this.damageDelay -= Time.deltaTime;
            }
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.slider.maxValue = maxHealth;
        this.slider.value = maxHealth;
    }

    public void SetHealthBar(int vida)
    {
        this.vidas = vida;
        this.slider.value = vida;
    }
}
