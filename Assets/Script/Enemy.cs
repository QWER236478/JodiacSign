using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; //��ü HP
    public float enemyHP; //���� ���� HP
    public Animator animator; //�ִϸ�����
    public AnimationClip attackClip; //���� �ִϸ����� Ŭ��
    public int attackCooldown; //���� ��Ÿ��
    public int CooldownResetValue; //���� ��Ÿ��
    public int attackDamage; //���� ������
    public GameObject AttackEffect; //���� �� ����Ʈ 
    public AudioSource AttackSounds; //���� �� �߻���ų ����
    public AudioSource ememyDie; //�� ��� �� �߻���ų ����
    public GameObject DieEffect; //�� ��� �� �߻���ų ����Ʈ

    public enum EnemyState //���� ����
    {
        Idle, Attack, Die
    }
    public EnemyState enemyState = EnemyState.Idle;

    public enum EnemyAttackState //���� ���� ����
    {
        None, Attack, Delay
    }
    public EnemyAttackState enemyAttackState = EnemyAttackState.None; //�� ���� ����

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
            case EnemyState.Idle: //��� �� ����
                {
                    break;
                }

            case EnemyState.Attack: // ���� ����
                {
                    switch (enemyAttackState)
                    {
                        case EnemyAttackState.Attack://���� ���� ����
                            {
                                if (attackCooldown <= 0)
                                {
                                    //camShake.ShakeOn();//ī�޶� ����      
                                    attackCooldown = CooldownResetValue; //���� ��� �ð� �ʱ�ȭ
                                    enemyAttackState = EnemyAttackState.Delay;
                                }
                                break;
                            }
                        case EnemyAttackState.Delay: //���� ��� ����
                            {
                                if (attackCooldown > 0)
                                {
                                    EnemyAnimOn(0); //��� �ִϸ��̼� ����                         
                                }

                                if (attackCooldown <= 0)  //������ ���� ��Ÿ���� 0���� �۰ų� ���� ��
                                {
                                    EnemyAnimOn(1); //���� �ִϸ��̼� ����        
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
