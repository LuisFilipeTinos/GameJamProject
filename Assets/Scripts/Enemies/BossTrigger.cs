using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] CameraShake cameraShake;
    [SerializeField] GameObject blockSpawner;

    public void SpawnSpawner()
    {
        Instantiate(blockSpawner);
    }

    public void Shake()
    {
        StartCoroutine(ShakeAndWait());
    }

    public IEnumerator ShakeAndWait()
    {
        while (cameraShake.intensity <= 0.3f)
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
    }

    public void ResetFieldandAttacks()
    {
        cameraShake.intensity = 0.00f;
    }
}