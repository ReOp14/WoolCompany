using UnityEngine;

namespace WoolCompany.Behaviors
{
    internal class DamagePlayerItem : PhysicsProp
    {
        public override void ItemActivate(bool used, bool buttonDown = true)
        {
            base.ItemActivate(used, buttonDown);
            if (buttonDown)
            {
                if (playerHeldBy != null) playerHeldBy.DamagePlayer(5);
                else Debug.Log("Could not damage player!");
            }
        }
    }
}
