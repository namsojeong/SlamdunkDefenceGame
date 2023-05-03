using DG.Tweening;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerTypeSO _player;
    [SerializeField] ParticleSystem levelUpParticle;

    private Transform targetTransform;
    private HealthSystem healthSystem;
    private Rigidbody2D rigid;

    private float moveSpeed = 5f;
    private float targetMaxRadius = 10f;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;

    public PlayerTypeSO GetPlayer { get { return _player; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }


    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        ChromaticAberrationEffect.Instance.SetWeight(.5f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(.5f);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargetting();
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;

            rigid.velocity = moveDir * moveSpeed;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void HandleTargetting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            HealthSystem healthSystem = enemy.GetComponent<HealthSystem>();
            healthSystem.Damage(999);
        }
    }

    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = enemy.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = enemy.transform;
                    }
                }
            }
        }
    }

    public void AddScale(float _scale)
    {
        transform.DOScale(transform.localScale += new Vector3(_scale, _scale, 0), 0.5f);
    }

    public void AddSpeed(float _speed)
    {
        moveSpeed += _speed;
    }

    public void AddRange(float _radius)
    {
        targetMaxRadius += _radius;
    }

    public void AddAbillity(PowerType powerType, float _amount)
    {
        levelUpParticle.Play();
        transform.DOScale(transform.localScale * new Vector2(1.2f, 1.2f), 1f);

        healthSystem.HealFull();

        switch(powerType)
        {
            case PowerType.SPEED:
                AddSpeed(_amount);
                break;
            case PowerType.HP:
                AddScale(_amount);
                break;
            case PowerType.RADIUS:
                AddRange(_amount);
                break;
        }
    }
}
