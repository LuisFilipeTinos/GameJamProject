using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    float intervalTime;
    public bool canSpawnBlocks;

    [SerializeField] GameObject blockPrefab;

    void Start()
    {
        intervalTime = 0.5f;
        StartCoroutine(SpawnBlocks());
    }

    IEnumerator SpawnBlocks()
    {
        yield return new WaitForSeconds(intervalTime);
        GameObject newBlock = Instantiate(blockPrefab, new Vector3(Random.Range(170, 183), 10, 0), Quaternion.identity);
        StartCoroutine(SpawnBlocks());
    }
}
