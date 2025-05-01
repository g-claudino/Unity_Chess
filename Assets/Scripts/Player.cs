using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private Bullet projectile;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float shootingCooldownSeconds;

    private float shootingElapsedSeconds = float.PositiveInfinity;

    // Update is called once per frame
    private void Update()
    {
        shootingElapsedSeconds += Time.deltaTime;
        ProcessInput();
    }

    private void OnCollisionStay(Collision collisionEvent)
    {
        var collisionSource = collisionEvent.gameObject;
        var enemy = collisionSource.GetComponent<Enemy>();
        if (enemy == null) return;
        TakeDamage(enemy.Damage, enemy.Team);
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W))
            MoveCharacter(gameObject.transform.forward * speed);
        else if (Input.GetKey(KeyCode.S)) MoveCharacter(gameObject.transform.forward * -speed);

        if (Input.GetKey(KeyCode.A))
            MoveCharacter(gameObject.transform.right * -speed);
        else if (Input.GetKey(KeyCode.D)) MoveCharacter(gameObject.transform.right * speed);

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            if (shootingElapsedSeconds > shootingCooldownSeconds)
            {
                shootingElapsedSeconds = 0f;
                ShootBullet();
            }
    }

    private void ShootBullet()
    {
        var clone = Instantiate(projectile);
        clone.transform.position = bulletSpawn.position;
        clone.Setup(transform.forward, Team, damage);
        clone.Fire();
    }
}