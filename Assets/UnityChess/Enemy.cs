using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Enemy : BaseCharacter
{
    private bool activeEnemy;
    private float currTime;
    private Vector3 deltaPosition;
    private Vector3 enemyPosition;

    private Player player;

    private Vector3 playerPosition;
    private Vector3 Velocity;

    private EyeMonsterAnimationController eyeMonsterAnimationController;

    [SerializeField] private Animator animator;
    [SerializeField, Range(0f, 4f)] private float attackDistance = 1f;
    public int Damage => damage;

    private void Start()
    {
        eyeMonsterAnimationController = new EyeMonsterAnimationController(animator);
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,attackDistance);
    }

    private void Update()
    {
        if (!activeEnemy || player == null)
        {
            eyeMonsterAnimationController.SetIsAttacking(false);
            eyeMonsterAnimationController.SetMoveSpeed(0f);
            return;
        }
        transform.LookAt(playerPosition);
        currTime += Time.deltaTime;
        Vector3 Distance = TrackPlayerPosition();
        float Magnitude = Distance.magnitude;
        if (Magnitude <= attackDistance || eyeMonsterAnimationController.GetIsAttacking())
        {
            eyeMonsterAnimationController.SetIsAttacking(true);
            eyeMonsterAnimationController.SetMoveSpeed(0f);
        }
        else
        {
            Velocity = Distance / Magnitude * (speed);
            MoveCharacter(Velocity);
            eyeMonsterAnimationController.SetMoveSpeed(speed);
            eyeMonsterAnimationController.SetIsAttacking(false);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private Vector3 TrackPlayerPosition()
    {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        deltaPosition = playerPosition - enemyPosition;
        return deltaPosition;
    }

    public void ActivateEnemy()
    {
        activeEnemy = true;
    }
}