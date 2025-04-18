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
    [SerializeField] private int damage;
    [SerializeField] private int hp;

    public int Damage => damage;
    
    public ETeam Team{get; private set;}
    void Start()
    {
        player = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];
        Team = ETeam.Enemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeEnemy || player == null) return;

        currTime += Time.deltaTime;
        TrackPlayerPosition();
    }

    void TrackPlayerPosition()
    {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
        deltaPosition = playerPosition - enemyPosition;
        deltaPosition.Normalize();
        rb.AddForce(deltaPosition * speed, ForceMode.VelocityChange);
    }

    void RemoveFromPlay()
    {
        Destroy(gameObject);
    }

    public void ActivateEnemy()
    {
        activeEnemy = true;
    }
    
    public void TakeDamage(int dmgValue)
    {
        hp -= dmgValue;
        if (hp <= 0)
        {
            RemoveFromPlay();
        }
    }
}