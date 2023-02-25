using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
