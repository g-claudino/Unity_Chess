using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    private float currTime = 0f;
    
    private Player player = null;

    private Vector3 playerPosition;
    private Vector3 enemyPosition;
    private Vector3 deltaPosition;

    private bool activeEnemy = false;

    [SerializeField] private Rigidbody rb;
    [SerializeField, Range(0f, 2f)] private float speed;
    [SerializeField, Range(10f, 100f)] private float LifeSpan;
    [SerializeField] private int damage;

    public int Damage => damage;
    
    void Start()
    {
        player = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeEnemy || player == null) return;

        currTime += Time.deltaTime;
        TrackPlayerPosition();
        RemoveFromPlay(currTime);
    }

    void TrackPlayerPosition()
    {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        deltaPosition = playerPosition - enemyPosition;
        deltaPosition.Normalize();
        rb.AddForce(deltaPosition * speed, ForceMode.VelocityChange);
    }

    void RemoveFromPlay(float passedTime)
    {
        if (passedTime >= LifeSpan)
        {
            activeEnemy = false;
            Destroy(gameObject);
        }
    }

    public void ActivateEnemy()
    {
        activeEnemy = true;
    }
    
}