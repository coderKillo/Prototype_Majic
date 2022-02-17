using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Spell/Projectile Spell")]
public class ProjectileSpell : ISpell
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 50f;
    [SerializeField] private float projectileArc = 3f;

    private float projectileLifetime = 3f;
    private float impactLifetime = 3f;

    public override void AttemptToCastSpell(AnimationManager animationManager, Transform casterHand, Vector3 target, Transform parent)
    {
        var spellWarmup = GameObject.Instantiate(spellWarmUpFX, casterHand.position, Quaternion.identity, casterHand);
        animationManager.PlayTargetAnimation(spellAnimation, true);
        Destroy(spellWarmup, cooldownTime);
    }

    public override void SuccessfullyCastSpell(AnimationManager animationManager, Transform casterHand, Vector3 target, Transform parent)
    {
        var direction = (target - casterHand.position).normalized;

        var spellProjectile = GameObject.Instantiate(spellCastFX, casterHand.position, Quaternion.LookRotation(direction), parent);

        spellProjectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
        spellProjectile.GetComponent<Projectile>().OnCollision.AddListener((collision) => OnProjectileCollision(spellProjectile, collision));

        Destroy(spellProjectile, projectileLifetime);
    }

    void OnProjectileCollision(GameObject projectile, Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Physics.IgnoreCollision(other.collider, projectile.GetComponent<Collider>());
            return;
        }

        var spellImpact = GameObject.Instantiate(spellImpactFX, other.contacts[0].point, Quaternion.identity);
        Destroy(projectile);
        Destroy(spellImpact, impactLifetime);
    }
}
