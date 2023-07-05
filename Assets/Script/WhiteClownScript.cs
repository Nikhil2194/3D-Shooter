using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteClownScript : MonoBehaviour
{
    [SerializeField] private GameObject playerTarget;
    [SerializeField] private float clownSpeed = 0.1f;
    private float distance;
    Animator anim;
    [SerializeField] private float clownHealth = 1f;
    bool isDead;
    [SerializeField] private Rigidbody skullPrefab;
    [SerializeField] private Transform skullspawnPoint;


    private void Start()
    {
        anim = GetComponent<Animator>();
        playerTarget = GameObject.Find("Player");
    }

    void Update()
    {
        HealthCheckZombie();
        distance = Vector3.Distance(playerTarget.transform.position, transform.position);

       if (distance < 25 && !isDead)
        {
            if (distance <= 4)     // Clown Throw Skulls
            {
                clownSpeed = 0;
                anim.SetTrigger("ClownThrow");
            }
            else
            {
                clownSpeed = 1f;
                anim.SetTrigger("ClownWalk");
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, clownSpeed * Time.deltaTime);
                transform.forward = playerTarget.transform.position - transform.position;
            }
        }
        else
        {
            anim.SetTrigger("ClownIdle");
        }
    }

    private void OnTriggerEnter(Collider other)   // WhiteClown Damage by Bullet
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            clownHealth = clownHealth - 0.35f;
        }
    }


    public void HealthCheckZombie()
    {
        if (clownHealth < 0)
        {
            anim.SetTrigger("ClownDead");
            clownSpeed = 0f;
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

    public void ThrowSkulls()
    {
        skullPrefab = Instantiate(skullPrefab, skullspawnPoint.position, skullspawnPoint.rotation);
        skullPrefab.transform.SetParent(this.transform);
        skullPrefab.AddForce(skullspawnPoint.forward * 650);
    }
}
