using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarController : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown;
    public bool isCooldown;
    Rigidbody player;
    public Color initialColor;
    public Color AttackColor;
    public GameObject AttackLabel;

    public void SetAttackBar()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackLabel.SetActive(true);
            isCooldown = true;
            //Activar anim ataque en loop
            player.GetComponent<Animator>().SetBool("Space", true);

            player.GetComponent<PlayerController>().attacking = true;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            //50% TIEMPO EN ATAQUE - 50% EN COOLDOWN
            if (imageCooldown.fillAmount > 0.5)
            {
                //En cooldown
                AttackLabel.SetActive(false);
                player.GetComponent<PlayerController>().attacking = false;

                //Cortar habilidad de ataque
                player.GetComponent<Animator>().SetBool("Space", false);

            } else{
                //En ataque
                player.GetComponent<Animator>().SetBool("Space", true);
            }

            if (imageCooldown.fillAmount == 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
