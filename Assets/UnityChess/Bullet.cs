using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(10f, 100f)] private float LifeSpan;
    [SerializeField] [Range(1f, 10f)] private float Speed;
    [SerializeField] private int Damage;
    private Vector3 bulletVelocity;

    private float currTime;
    private ETeam team;
    private bool wasFired;

    // Update is called once per frame
    private void Update()
    {
        if (wasFired)
        {
            transform.position += bulletVelocity * Time.deltaTime;
            currTime += Time.deltaTime;
            RemoveFromPlay(currTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var collisionSource = other.gameObject;
        IDamageable character = collisionSource.GetComponent<BaseCharacter>();
        character?.TakeDamage(Damage, team);
        RemoveFromPlay(float.PositiveInfinity);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Vector3 direction, ETeam team, int damage)
    {
        bulletVelocity = direction * Speed;
        this.team = team;
        Damage = damage;
    }

    private void RemoveFromPlay(float passedTime)
    {
        if (passedTime >= LifeSpan) Destroy(gameObject);
    }

    public void Fire()
    {
        wasFired = true;
    }
}