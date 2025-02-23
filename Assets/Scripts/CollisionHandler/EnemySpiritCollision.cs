using UnityEngine;

namespace CollisionHandler
{
    public class EnemySpiritCollision : EnemyCollisionHandler {

        public override void OnCollisionWithPlayer(BaseCollisionHandler player)
        {
            Debug.Log("Colliding with enemy's spirit");
            Destroy(transform.parent.gameObject);
        }
    }
}
