using UnityEngine;

public class Enemy : BaseCharacter
{
    private bool activeEnemy;
    private float currTime;
    private Vector3 deltaPosition;
    private Vector3 enemyPosition;

    private Player player;

    private Vector3 playerPosition;

    public int Damage => damage;

    // Update is called once per frame
    private void Update()
    {
        if (!activeEnemy || player == null) return;

        currTime += Time.deltaTime;
        TrackPlayerPosition();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void TrackPlayerPosition()
    {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        deltaPosition = playerPosition - enemyPosition;
        deltaPosition.Normalize();
        MoveCharacter(deltaPosition * speed);
    }

    public void ActivateEnemy()
    {
        activeEnemy = true;
    }
}