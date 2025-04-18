using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(10f, 100f)] private float LifeSpan;
    [SerializeField, Range(1f, 10f)] private float Speed;
    [SerializeField] private int Damage;
        
    private float currTime = 0f;
    private Vector3 bulletVelocity;
    private bool wasFired = false;
    private ETeam team;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Vector3 direction, ETeam team)
    {
        bulletVelocity = direction * Speed;
        this.team = team;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasFired){
            transform.position += bulletVelocity * Time.deltaTime;
            currTime += Time.deltaTime;
            RemoveFromPlay(currTime);
        }
    }
    
    void RemoveFromPlay(float passedTime)
    {
        if (passedTime >= LifeSpan)
        {
            Destroy(gameObject);
        }
    }

    public void Fffire()
    {
        wasFired = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionSource = other.gameObject;
        Enemy enemy = collisionSource.GetComponent<Enemy>();
        Player player = collisionSource.GetComponent<Player>();
        if (enemy != null && team != ETeam.Enemy)
        {
            enemy.TakeDamage(Damage);
        }
        else if (player != null && team != ETeam.Player)
        {
            player.TakeDamage(Damage);
        }
        else
        {
            RemoveFromPlay(float.PositiveInfinity);
        }
    }
}
