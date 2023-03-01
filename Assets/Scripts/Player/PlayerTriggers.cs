using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] CameraShake cameraShake;

    [SerializeField] Animator bossAnim;
    [SerializeField] GameObject boss;

    public static bool inTriggerZoom;
    public static bool ampliedZoom;

    public static bool inTriggerLeftWall;
    public static bool inTriggerRightWall;

    bool notTriggeredYet = true;

    [SerializeField] BoxCollider2D leftWall;

    public static bool pegouEspada;
    public static bool pegouBoomerang;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IntroBossTriggerStage1") && notTriggeredYet)
        {
            player.canMove = false;
            collision.enabled = false;
            notTriggeredYet = false;
            player.rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            cameraMovement.upLimit = 30;
            cameraMovement.rightLimit = 341.56f;
            cameraMovement.leftLimit = 341.56f;
            leftWall.enabled = true;
            StartCoroutine(ShakeAndWait());
        }

        if(collision.gameObject.CompareTag("LeftWall"))
        {
            inTriggerLeftWall = true;
        }

        else if(collision.gameObject.CompareTag("RightWall"))
        {
            inTriggerRightWall = true;
        }


       
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Zoom"))
        {
            Debug.Log("colidiu");
            inTriggerZoom = true;
        }

        else
        {
           inTriggerZoom = false;
        }


        if(collision.gameObject.CompareTag("AmpliedScreen"))
        {
            Debug.Log("Ampliar");
            ampliedZoom = true;
        }

        else{
            ampliedZoom = false;
        }

        if(collision.gameObject.CompareTag("Espada"))
        {
            pegouEspada = true;
        }

        if(collision.gameObject.CompareTag("Boomerang"))
        {
            pegouBoomerang = true;
        }
    }

    public IEnumerator ShakeAndWait()
    {
        while(cameraShake.intensity <= 0.3f)
        {
            yield return new WaitForSeconds(0.1f);
            cameraShake.intensity += Time.deltaTime;
        }
            
        yield return new WaitForSeconds(0.5f);

        while (cameraShake.intensity >= 0f)
        {
            yield return new WaitForSeconds(0.1f);
            cameraShake.intensity -= Time.deltaTime;
        }

        if (cameraShake.intensity < 0.0f)
            cameraShake.intensity = 0.00f;
        
        yield return new WaitForSeconds(1f);

        boss.SetActive(true);
        yield return new WaitForSeconds(1f);
        boss.GetComponent<EnemyTakeDamage>().finishedAppearing = true;

        player.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.canMove = true;
    }
}
