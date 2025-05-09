using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    [SerializeField] protected float iFramesSeconds;
    [SerializeField] protected int damage;
    [SerializeField] private ETeam team;

    protected bool isInvincible;

    private const float BlinkIntervalSeconds = 0.05f;

    public ETeam Team => team;

    private void Start()
    {
        isInvincible = false;
    }

    public void TakeDamage(int dmgValue, ETeam dmgCauser)
    {
        if (Team == dmgCauser || isInvincible) return;

        hp -= dmgValue;
        if (hp <= 0)
            Kill();
        else
            IFrames().Forget();
    }

    public event Action OnDeath;

    protected virtual async UniTaskVoid IFrames()
    {
        var playerMesh = GetComponent<MeshRenderer>();
        isInvincible = true;
        float invincibilityTime = iFramesSeconds;
        while (invincibilityTime > 0)
        {
            BlinkPlayer(playerMesh);
            await UniTask.Delay(TimeSpan.FromSeconds(BlinkIntervalSeconds));
            invincibilityTime -= BlinkIntervalSeconds;
        }
        playerMesh.enabled = true;
        isInvincible = false;
    }

    private void BlinkPlayer(MeshRenderer playerMesh)
    {
        playerMesh.enabled = !playerMesh.enabled;
    }

    protected void MoveCharacter(Vector3 target)
    {
        rb.AddForce(target, ForceMode.VelocityChange);
    }

    private void Kill()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}