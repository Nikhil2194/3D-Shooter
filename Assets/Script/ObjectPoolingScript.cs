using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingScript : MonoBehaviour
{
    public static ObjectPoolingScript Instance;  // ObjectPooling is Singleton Script
    public GameObject bulletPrefab;
    public int poolSize = 15;
    [SerializeField] AudioClip BGM_SFX;

    private List<GameObject> bulletPool;  //is a list which will stores the bullet instances 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        bulletPool = new List<GameObject>();
        AudioManager.Instance.PlayMusic(BGM_SFX);

        for (int i = 0; i < poolSize; i++)     //In Start '15' bullet prefabs are being 'instantaited' and stored in a list
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(this.transform);
            bullet.SetActive(false);            // Deactivated first and then add to bulletpool list
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()    //The GetBullet method iterates through the bulletPool list and checks if any bullet is inactive (!bullet.activeInHierarchy).
                                     //If an inactive bullet is found, it is returned to be used. This allows for reusing bullets from the object pool instead of creating new instances.
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    public void ShootBullet(Vector3 position, Quaternion rotation)  // It retrives an available bullet from GetBullet,
                                                                    // sets transform and rotation and Activate It
    {
        GameObject bullet = GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
        }
    }
}
