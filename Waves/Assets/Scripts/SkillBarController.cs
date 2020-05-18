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
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AttackLabel.SetActive(true);
            isCooldown = true;
            player.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
            player.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", AttackColor);
            player.GetComponent<PlayerController>().attacking = true;
        }

        if(isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if(imageCooldown.fillAmount > 0.5)
            {
                AttackLabel.SetActive(false);
                player.GetComponent<PlayerController>().attacking = false;
                player.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
                player.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", initialColor);
            }
            if(imageCooldown.fillAmount == 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
