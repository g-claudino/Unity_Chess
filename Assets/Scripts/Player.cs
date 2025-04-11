using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    public event Action OnDeath; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(gameObject.transform.forward * speed, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(gameObject.transform.forward * -speed, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(gameObject.transform.right * -speed, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(gameObject.transform.right * speed, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collisionEvent)
    {
        GameObject collisionSource = collisionEvent.gameObject;
        Enemy isColliderEnemy = collisionSource.GetComponent<Enemy>();
        if (isColliderEnemy == null)
        {
            return;
        }
        Damage(1);
    }

    private void Damage(int dmgValue)
    {
        hp -= dmgValue;
        if (hp <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
