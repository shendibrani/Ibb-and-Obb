using UnityEngine;

namespace CollisionHandler
{
    public class BaseCollisionHandler : MonoBehaviour {

        public virtual void OnCollisionWithPlayer(BaseCollisionHandler player)
        {
            Debug.Log("Also wrong");
        }
    }
}
