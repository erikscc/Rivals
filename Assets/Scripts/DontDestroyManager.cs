using UnityEngine;

namespace ESCape
{
    public class DontDestroyManager : MonoBehaviour
    {
        //reference to this script instance
        private static DontDestroyManager instance;
        
        //set the whole gameobject to 'dont destroy',
        //or destroy the other one if there's a duplicate
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);
        }
    }
}