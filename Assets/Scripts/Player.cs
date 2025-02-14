using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(gameObject.transform.forward * acceleration, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(gameObject.transform.forward * -acceleration, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(gameObject.transform.right * -acceleration, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(gameObject.transform.right * acceleration, ForceMode.Impulse);
        }
    }
}
