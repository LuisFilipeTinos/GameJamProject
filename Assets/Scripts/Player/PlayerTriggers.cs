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

    bool notTriggeredYet = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IntroBossTriggerStage1") && notTriggeredYet)
        {
            player.canMove = false;
            collision.enabled = false;
            notTriggeredYet = false;
            player.rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            cameraMovement.upLimit = 0;
            cameraMovement.rightLimit = 175;
            cameraMovement.leftLimit = 175;
            StartCoroutine(ShakeAndWait());
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
