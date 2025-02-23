using UnityEngine;
using UnityEngine.SceneManagement;

namespace CollisionHandler
{
    public class EnemyCollision : EnemyCollisionHandler {

        public override void OnCollisionWithPlayer(BaseCollisionHandler player)
        {
            Debug.Log("Dead!");
            SceneManager.LoadScene(0);
        }
    }
}
