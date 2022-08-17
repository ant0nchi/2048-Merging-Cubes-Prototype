using UnityEngine;

public abstract class BonusCube : MonoBehaviour
{
    protected abstract void DetectCollision(Collision collision);

    void OnCollisionStay(Collision collision)
    {
        DetectCollision(collision);
        DetectWallColision(collision);
    }

    private void DetectWallColision(Collision collision)
    {
        if (collision.gameObject.tag == "EndWall")
        {
            Destroy(gameObject);
        }
    }
}
