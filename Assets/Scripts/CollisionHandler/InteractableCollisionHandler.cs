using System;

namespace CollisionHandler
{
    public class InteractableCollisionHandler : BaseCollisionHandler {

        public override void OnCollisionWithPlayer(BaseCollisionHandler player)
        {
            throw new NotImplementedException();
        }

        public virtual void OnExitCollisionWithPlayer(BaseCollisionHandler player)
        {
            throw new NotImplementedException();
        }
    }
}
