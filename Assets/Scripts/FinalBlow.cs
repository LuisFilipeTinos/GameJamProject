using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBlow : MonoBehaviour
{
    [SerializeField] GameObject bloodBurst;

    void Update()
    {
        Invoke("Explode", 1f);
    }

    public void Explode()
    {
        Instantiate(bloodBurst, Random.insideUnitSphere * 5 + transform.position, Random.rotation);
    }
}
