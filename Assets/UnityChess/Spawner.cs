using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] [Range(1f, 10f)] private float SpawnTime;
    [SerializeField] [Range(1f, 10f)] private float wallMargin;
    [SerializeField] private Collider arenaFloor;
    [SerializeField] private Enemy enemyPreFab;
    private float currTime;

    private Collider enemyCollider;
    private Vector3 MaximumPositions;
    private Player player;

    private float rX;
    private float rY;
    private float rZ;

    private void Start()
    {
        enemyCollider = enemyPreFab.GetComponent<Collider>();
        player = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)[0];
    }

    // Update is called once per frame
    private void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= SpawnTime && player != null) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        currTime = 0f;
        var clone = Instantiate(enemyPreFab, new Vector3(0, 0, 0),
            Quaternion.identity);
        var spawnPoint = CalculateSpawnPoint(clone);
        clone.transform.position = spawnPoint;
        clone.SetPlayer(player);
        clone.ActivateEnemy();
    }

    private Vector3 CalculateSpawnPoint(Enemy clone)
    {
        MaximumPositions = arenaFloor.bounds.center + arenaFloor.bounds.extents;
        rX = Random.Range(-MaximumPositions.x + wallMargin, MaximumPositions.x - wallMargin);
        rY = MaximumPositions.y + clone.GetComponent<Collider>().bounds.extents.y;
        rZ = Random.Range(-MaximumPositions.z + wallMargin, MaximumPositions.z - wallMargin);
        return new Vector3(rX, rY, rZ);
    }
}