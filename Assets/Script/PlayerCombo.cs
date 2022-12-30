using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombo : MonoBehaviour
{

    public Combo[] combos;
	public Attack attack;
    public Animator anim;
    public List<string> currentCombo;

	public UnityEvent onStartCombo, onFinishCombo;

    private bool startCombo;
    private float comboTimer;
    private Hit currentHit, nextHit;
    private bool canHit = true;
    private bool resetCombo;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Variaveis.podeAtacar == true)
        {
			CheckInputs();
		} 
    }

	void CheckInputs()
	{

		if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && !canHit)
		{
			resetCombo = true;
		}

		for (int i = 0; i < combos.Length; i++)
		{
			if (combos[i].hits.Length > currentCombo.Count)
			{
				if (Input.GetButtonDown(combos[i].hits[currentCombo.Count].inputButton))
				{
					if (currentCombo.Count == 0)
					{
						onStartCombo.Invoke();
						PlayHit(combos[i].hits[currentCombo.Count]);
						break;
					}
					else
					{
						bool comboMatch = false;
						for (int y = 0; y < currentCombo.Count; y++)
						{
							if (currentCombo[y] != combos[i].hits[y].inputButton)
							{
								comboMatch = false;
								break;
							}
							else
							{
								comboMatch = true;
							}
						}

						if (comboMatch && canHit)
						{
							Debug.Log("Hit adicionado ao combo");
							nextHit = combos[i].hits[currentCombo.Count];
							canHit = false;
							break;
						}
					}

				}
			}


		}

		if (startCombo)
		{
			comboTimer += Time.deltaTime;
			if (comboTimer >= currentHit.animationTime && !canHit)
			{
				PlayHit(nextHit);
				if (resetCombo)
				{
					canHit = false;
					CancelInvoke();
					Invoke("ResetCombo", currentHit.animationTime);
				}
			}

			if (comboTimer >= currentHit.resetTime)
			{
				ResetCombo();
			}

		}
	}

	void PlayHit(Hit hit)
    {
        comboTimer = 0;
		attack.SetAttack(hit);
		anim.Play(hit.animation);
        startCombo = true;
        currentCombo.Add(hit.inputButton);
        currentHit = hit;
        canHit = true;
    }

    void ResetCombo()
    {
		resetCombo = false;
		onFinishCombo.Invoke();
        startCombo = false;
        comboTimer = 0;
        currentCombo.Clear();
        anim.Rebind();
        canHit = true;
    }
}
