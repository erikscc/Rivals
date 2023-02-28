namespace ESCape
{
	public class PowerupBullet : Collectible
    {
        /// <summary>
        /// Amount of shots before returning to the default bullet.
        /// </summary>
        public int amount = 5;

        /// <summary>
        /// Index of the new bullet, on the Player script, that should be assigned.
        /// </summary>
        public int bulletIndex = 1;


        /// <summary>
        /// Overrides the default behavior with a custom implementation.
        /// Check for the current bullet and refills its ammunition.
        /// </summary>
		public override bool Apply(Player p)
        {
            if (p == null)
                return false;

            int value = p.GetView().GetAmmo();
            int index = p.GetView().GetBullet();

            //do not consume powerup if the player owns the new bullet already
            //and the ammunition is at the maximum amount available
            if (value == amount && index == bulletIndex)
                return false;

            //otherwise assign new bullet and refill ammo
            p.GetView().SetAmmo(amount, bulletIndex);

            //return successful collection
            return true;
        }
    }
}