using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Collider hitbox;
    
    private void OnTriggerEnter(Collider playerCollider)
    {
        var player = playerCollider.gameObject.GetComponent<Player>();
        if (player == null) return;
        player.TakeDamage(enemy.Damage, enemy.Team);
    }
    
    public void ActivateHitbox()
    {
        hitbox.enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void DeactivateHitbox()
    {
        hitbox.enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}