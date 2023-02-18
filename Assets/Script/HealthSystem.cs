using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public GameObject[] hearts;
    private int life;
    private bool dead;
    public bool onTrigger;

    // Start is called before the first frame update
    private void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if ( dead == true)
        {
            Debug.Log("morreu");
        }
    }

    public void TakeDamage(int d)
    {
        if (life >= 1)
        {
            life -= d;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
        }
    }
}
