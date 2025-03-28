using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [FormerlySerializedAs("velocity")] [FormerlySerializedAs("acceleration")] [SerializeField] private float speed;
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
}
