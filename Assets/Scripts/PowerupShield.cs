namespace ESCape
{
	public class PowerupShield : Collectible
    {
        /// <summary>
        /// Amount of shield points to add per consumption.
        /// </summary>
        public int amount = 3;


        /// <summary>
        /// Overrides the default behavior with a custom implementation.
        /// Check for the current shield and adds additional shield points.
        /// </summary>
		public override bool Apply(Player p)
        {
            if (p == null)
                return false;

            int value = p.GetView().GetShield();

            //don't add shield if it is at the maximum already
            if (value == amount)
                return false;

            //assign absolute shield points to player
            //we can't go over the maximum thus no need to check it here
            p.GetView().SetShield(amount);

            //return successful collection
            return true;
        }
    }
}
