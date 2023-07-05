using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Animator animator;
    private float rotationSpeed = 5000f;
    public GameObject bulletPrefab;
    public Transform bulletPosition;
    public float bulletSPeed = 10f;
    public bool isFiring = false;

    public float PlayerHealth = 1f;
    public Image healthImage;
    [SerializeField] ParticleSystem bulletVFX;
    [SerializeField] ParticleSystem bloodVFX;
    [SerializeField]private bool isLive=true;
    [SerializeField] AudioClip [] bulletSFXArray ;
    [SerializeField] AudioClip HumanDeathSFX;

    
    void Start()
    {     
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        Fire();
        HealthCheck();
    }

    public void PlayerMovement()
    {
        if(isLive)
        {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;
        rb.velocity = movement;


        if (movement != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime));
        }

        animator.SetFloat("Speed", movement.magnitude);

        }
    }


    public void Fire()
    {
        if (Input.GetMouseButtonDown(0) )
        {  
           animator.SetTrigger("Fire");
        }
    }

    public void  BulletShoot()
    {
        /* var bullet = Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation);
         bullet.GetComponent<Rigidbody>().velocity = bulletPosition.forward * bulletSPeed;
         bullet.transform.SetParent(gameObject.transform);*/
         ObjectPoolingScript.Instance.ShootBullet(bulletPosition.position, bulletPosition.rotation); //Calling the method from ObjectPool Script
         bulletVFX.Play();
         AudioManager.Instance.RandomSoundEffect(bulletSFXArray);
    }

    public void HealthCheck()
    {
        if(PlayerHealth<=0)
        {
            AudioManager.Instance.Play(HumanDeathSFX);
            animator.SetTrigger("PlayerDied");
            isLive = false;
            StartCoroutine(DeathJumpToMainScreen());
        }
    }

    private void OnTriggerEnter(Collider other)    // Zombie Axe Attak Health 
    {
        if(other.gameObject.CompareTag("AxeHealthHit") || other.gameObject.CompareTag("Skull"))
        {
            bloodVFX.Play();
            PlayerHealth = PlayerHealth - 0.05f;
            healthImage.fillAmount -= 0.05f; 
        }
    }

  

    IEnumerator DeathJumpToMainScreen()
    {
        yield return new WaitForSeconds(12);
        GameManager.Instance.QuitGame();
    }
}
