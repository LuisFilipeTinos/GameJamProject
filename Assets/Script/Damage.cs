using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Color damageColor;
    public float damageTime = 0.1f;

    public GameObject damageTxt;
    public Transform damageTxtPosition;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }


    public void TakeDamage(int damage)
    {
        spriteRenderer.color = damageColor;
        Invoke("ReleaseDamage", damageTime);
        GameObject newDamageTxt = Instantiate(damageTxt, damageTxtPosition.position, Quaternion.identity);
        newDamageTxt.GetComponentInChildren<Text>().text = damage.ToString();
        Destroy(newDamageTxt, 1f);
    }

    void ReleaseDamage()
    {
        spriteRenderer.color = defaultColor;
    }
}
