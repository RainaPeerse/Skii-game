using UnityEngine;

public class ObsticleExplode : Obstacle
{
    public GameObject explosionParticlesPrefab; // Assign this in the Unity Editor with your ExplosionParticles prefab

    public override void HitPlayer(GameObject player)
    {
        base.HitPlayer(player);
        Explode();
    }

    private void Explode()
    {
        // Instantiate the explosion particles at the obstacle's position
        if (explosionParticlesPrefab != null)
        {
            Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the obstacle
        Destroy(gameObject);
    }
}
