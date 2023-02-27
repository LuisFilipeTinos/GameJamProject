using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] GameObject gate;
    Rigidbody2D gateRb2d;

    private bool canPressButton;

    void Start()
    {
        canPressButton = true;
        gateRb2d = gate.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider") && canPressButton)
        {
            canPressButton = false;
            gateRb2d.velocity = new Vector2(0, 2f);
            gateRb2d.gravityScale = 0;
            StartCoroutine(LiftGate());
        }
            
    }

    private IEnumerator LiftGate()
    {
        yield return new WaitForSeconds(2.5f);
        gateRb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        gateRb2d.gravityScale = 1;
        yield return new WaitForSeconds(2f);
        canPressButton = true;
    }
}
