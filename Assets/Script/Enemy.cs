using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; //전체 HP
    public float enemyHP; //보스 현재 HP
    public Animator animator; //애니메이터
    public AnimationClip attackClip; //공격 애니메이터 클립
    public int attackCooldown; //공격 쿨타임
    public int CooldownResetValue; //리셋 쿨타임
    public int attackDamage; //공격 데미지
    public GameObject AttackEffect; //공격 시 이팩트 
    public AudioSource AttackSounds; //공격 시 발생시킬 사운드
    public AudioSource ememyDie; //적 사망 시 발생시킬 사운드
    public GameObject DieEffect; //적 사망 시 발생시킬 이팩트

    public enum EnemyState //적의 상태
    {
        Idle, Attack, Die
    }
    public EnemyState enemyState = EnemyState.Idle;

    public enum EnemyAttackState //적의 공격 상태
    {
        None, Attack, Delay
    }
    public EnemyAttackState enemyAttackState = EnemyAttackState.None; //적 공격 상태

    void Start()
    {
        maxHP = enemyHP;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateOn();
    }

    void EnemyStateOn()
    {
        switch (enemyState)
        {
            case EnemyState.Idle: //대기 중 상태
                {
                    break;
                }

            case EnemyState.Attack: // 공격 상태
                {
                    switch (enemyAttackState)
                    {
                        case EnemyAttackState.Attack://공격 시작 상태
                            {
                                if (attackCooldown <= 0)
                                {
                                    //camShake.ShakeOn();//카메라 흔들기      
                                    attackCooldown = CooldownResetValue; //공격 대기 시간 초기화
                                    enemyAttackState = EnemyAttackState.Delay;
                                }
                                break;
                            }
                        case EnemyAttackState.Delay: //공격 대기 상태
                            {
                                if (attackCooldown > 0)
                                {
                                    EnemyAnimOn(0); //대기 애니메이션 실행                         
                                }

                                if (attackCooldown <= 0)  //몬스터의 공격 쿨타임이 0보다 작거나 같을 때
                                {
                                    EnemyAnimOn(1); //공격 애니메이션 실행        
                                    enemyAttackState = EnemyAttackState.Attack;
                                }
                                break;
                            }             
                    }
                    break;
                }
        }
    }

    void EnemyAnimOn(int i)
    {
        animator.SetInteger("EnemyState", i);
    }
}
