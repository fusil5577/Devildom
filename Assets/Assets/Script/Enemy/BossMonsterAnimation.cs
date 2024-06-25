using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterAnimation : MonoBehaviour
{
    private Animator animator;
    public GameObject endPanel;

    private static readonly int isDeath = Animator.StringToHash("IsDeath");
    private static readonly int isAttack1 = Animator.StringToHash("IsAttack1");
    private static readonly int isAttack2 = Animator.StringToHash("IsAttack2");
    private static readonly int isHit = Animator.StringToHash("IsTakeHit");

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
        //몬스터 죽는다.
        endPanel.SetActive(true);
        GameManager.Instance.fadeImage.FadeOut(GameManager.Instance.Screenimage);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
    public void Attack1()
    {
        animator.SetBool(isAttack1, true);
    }
    public void Attack2()
    {
        animator.SetBool(isAttack2, true);
    }
    public void Hit()
    {
        animator.SetBool(isHit, true);
    }

    public void Attack1End()
    {
        animator.SetBool(isAttack1, false);
    }
    public void Attack2End()
    {
        animator.SetBool(isAttack2, false);
    }
    public void HitEnd()
    {
        animator.SetBool(isHit, false);
    }
}
