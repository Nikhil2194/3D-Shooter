using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieMovementScript : MonoBehaviour
{
    [SerializeField] private GameObject playerTarget;
    [SerializeField] private float zombieMoveSpeed = 0.45f;
    private float distance;
    Animator anim;
    [SerializeField] private float zombieHealth = 1f;
    [SerializeField] Image zombieHealthImage ;
    bool isDead;
    PlayerController pc;

    public Transform zombieBodycharacterMedium;
    public Material[] ZombieBloodMat;
    public SkinnedMeshRenderer rend;
    [SerializeField] AudioClip zombieSFX;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTarget = GameObject.Find("Player");

    }

    void Update()
    {
        HealthCheckZombie();
        distance = Vector3.Distance(playerTarget.transform.position, transform.position);

       /* if(distance<10)
        {
           zombieMoveSpeed = 0.45f;
            anim.SetTrigger("ZombieRun");
            Debug.Log("Distance between is - "+ distance);
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, zombieMoveSpeed * Time.deltaTime);
            transform.forward = playerTarget.transform.position - transform.position;
        }
        if (distance <= 1.5)
        {
            zombieMoveSpeed = 0;
            anim.ResetTrigger("ZombieRun");
            anim.SetTrigger("Zombie_Hitting");
        }
        if(distance>=10)
        {
            anim.SetTrigger("Zombie_Idle");
        }*/

        if (distance < 25 && !isDead)
        {
            if (distance <= 1.5)
            {
                zombieMoveSpeed = 0;
                anim.ResetTrigger("ZombieRun");
                anim.SetTrigger("Zombie_Hitting");
            }
            else
            {
                zombieMoveSpeed = 1f;
                anim.SetTrigger("ZombieRun");
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, zombieMoveSpeed * Time.deltaTime);
                transform.forward = playerTarget.transform.position - transform.position;
            }
        }
        else
        {
            anim.SetTrigger("Zombie_Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            zombieHealth = zombieHealth - 0.2f;
            zombieHealthImage.fillAmount = zombieHealthImage.fillAmount - 0.333f;
        }
    }


    public void HealthCheckZombie()
    {
        if (zombieHealth < 0.12)
        {
            
            rend = zombieBodycharacterMedium.GetComponent<SkinnedMeshRenderer>();
            rend.sharedMaterials = ZombieBloodMat;
           // rend.sharedMaterial.color = Color.Lerp(Color.white, rend.material.color, Mathf.Abs(Mathf.Sin(Time.deltaTime * 2)));
        }

        if(zombieHealth < 0)
        {
            //anim.ResetTrigger("Zombie_Hitting");
            AudioManager.Instance.PlayZombieSFX(zombieSFX);
            anim.SetTrigger("Zombie_Dead");
            zombieMoveSpeed = 0f;
            isDead = true;
            StartCoroutine(DestroyEnemyGameObject());
        }
    }

    private IEnumerator DestroyEnemyGameObject()
    {
        yield return new WaitForSeconds(8);
        GameManager.Instance.ScoreUp();
        Destroy(this.gameObject);
    }
}
