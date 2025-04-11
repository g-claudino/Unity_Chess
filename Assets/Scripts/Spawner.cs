using UnityEngine;

public class Spawner : MonoBehaviour
{
    float currTime = 0f;
    [SerializeField, Range(1f, 10f)] private float SpawnTime;
    [SerializeField, Range(1f, 10f)] private float wallMargin;
    [SerializeField] private Collider arenaFloor;
    [SerializeField] private Enemy SpawnEnemy;
    
    private Collider enemyCollider;
    private Vector3 MaximumPositions;
    private Player player;
    
    private float rX = 0;
    private float rY = 0;
    private float rZ = 0;

    void Start()
    {
        enemyCollider = SpawnEnemy.GetComponent<Collider>();
        player = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= SpawnTime && player != null)
        {
            MaximumPositions = arenaFloor.bounds.center + arenaFloor.bounds.extents;
            currTime = 0f;
            rX = Random.Range(-MaximumPositions.x + wallMargin, MaximumPositions.x - wallMargin);
            rY = MaximumPositions.y + enemyCollider.bounds.extents.y;
            rZ = Random.Range(-MaximumPositions.z + wallMargin, MaximumPositions.z - wallMargin);
            Enemy clone = Instantiate(SpawnEnemy, new Vector3(rX, rY, rZ),
                Quaternion.identity);
            clone.ActivateEnemy();
        }
    }
}