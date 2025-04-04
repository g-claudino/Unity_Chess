using UnityEngine;

public class Spawner : MonoBehaviour
{
    float currTime = 0f;
    [SerializeField,Range(1f,10f)] private float SpawnTime;
    [SerializeField] private Vector2 MaximumPositions;
    private float rX = 0;
    private float rZ = 0;
    [SerializeField] private Enemy SpawnEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= SpawnTime)
        {
            currTime = 0f;
            rX = Random.Range(-MaximumPositions.x, MaximumPositions.x);
            rZ = Random.Range(-MaximumPositions.y, MaximumPositions.y);
            Enemy clone = Instantiate(SpawnEnemy, new Vector3(rX, transform.position.y, rZ), Quaternion.identity);
        }
    }
}
