using UnityEngine;

namespace CollisionHandler
{
    public class UpsideDownCollision : InteractableCollisionHandler {

        public override void OnCollisionWithPlayer(BaseCollisionHandler player)
        {
            Debug.Log("upside down now!");
            player.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
        }

        public override void OnExitCollisionWithPlayer(BaseCollisionHandler player)
        {
            player.GetComponent<PlayerController>().ToggleUpsideDownValues();
        }
    }
}
