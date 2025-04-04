using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float currTime = 0f;
    [SerializeField,Range(10f,100f)] private float LifeSpan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= LifeSpan)
        {
            RemoveFromPlay();
        }
    }

    void RemoveFromPlay()
    {
        Destroy(transform.gameObject);
    }
    
}
