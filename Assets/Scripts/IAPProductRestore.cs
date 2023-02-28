using UnityEngine;

namespace ESCape
{
    public class IAPProductRestore : MonoBehaviour
    {
        //only show the restore button on iOS
        void Start()
        {
            #if !UNITY_IPHONE
                gameObject.SetActive(false);
            #endif
        }


        /// <summary>
        /// Calls Unity IAPs RestoreTransactions method.
        /// It makes sense to add this to an UI button event.
        /// </summary>
        public void Restore()
        {
            #if UNITY_IAP
            UnityIAPManager.RestoreTransactions();
            #endif
        }
    }
}
