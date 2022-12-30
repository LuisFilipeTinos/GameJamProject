using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public MeshRenderer mR;
    public float speed;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mR.material.mainTextureOffset += new Vector2(player.GetComponent<Player>().direction * speed * Time.deltaTime, 0);
    }
}
