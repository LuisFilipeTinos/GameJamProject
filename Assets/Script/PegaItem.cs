using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegaItem : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTriggers.pegouEspada == true)
        {
            Destroy(gameObject);

        }

        if(PlayerTriggers.pegouBoomerang == true)
        {
            Destroy(gameObject);
        }
    }
}
