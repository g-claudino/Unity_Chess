using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private float iFramesSeconds;
    
    public event Action OnDeath; 
    
    private bool isInvincible;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
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

    private void OnCollisionStay(Collision collisionEvent)
    {
        if (isInvincible) return;
        
        GameObject collisionSource = collisionEvent.gameObject;
        Enemy enemy = collisionSource.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }
        TakeDamage(enemy.Damage);
    }
    
    private void TakeDamage(int dmgValue)
    {
        hp -= dmgValue;
        if (hp <= 0)
        {
            KillPlayer();
        }
        else
        {
            StartCoroutine(IFrames());
        }
    }

    private IEnumerator IFrames()
    {
        MeshRenderer playerMesh = GetComponent<MeshRenderer>();
        isInvincible = true;
        Coroutine isBlinking = StartCoroutine(BlinkPlayer(playerMesh));
        yield return new WaitForSeconds(iFramesSeconds);
        StopCoroutine(isBlinking);
        playerMesh.enabled = true;
        isInvincible = false;
    }

    private IEnumerator BlinkPlayer(MeshRenderer playerMesh)
    {
        while (true)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    private void KillPlayer()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
