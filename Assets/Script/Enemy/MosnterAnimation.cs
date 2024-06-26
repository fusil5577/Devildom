using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MosnterAnimation : MonoBehaviour
{
    private Animator animator;  

    private static readonly int isDeath = Animator.StringToHash("IsDeath");

    public bool isAlive = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            animator.SetBool(isDeath, true);
        }
    }
    private void Monsterend()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
