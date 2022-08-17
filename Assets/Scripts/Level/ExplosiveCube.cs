using UnityEngine;

public class ExplosiveCube : BonusCube
{
    private float power = 300f;
    private float radius = 3f;

    protected override void DetectCollision(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            Destroy(collision.gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                collider.attachedRigidbody?.AddExplosionForce(power, transform.position, radius);
            }

            Destroy(gameObject);
        }
    }
}
