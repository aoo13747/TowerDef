using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    protected override void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode();
        }
        base.HitTarget();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
}
