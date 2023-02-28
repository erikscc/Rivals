using UnityEngine;
using UnityEngine.SceneManagement;

namespace ESCape
{
    public class UIRestartButton : MonoBehaviour 
    {
        //listen to scene changes
        void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        
        //give the scene some time to initialize
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Invoke("EnterPlay", 0.5f);
        }
        
        
        //call the play button instantly on scene load
        //destroy itself after use
        void EnterPlay()
        {
            FindObjectOfType<UIMain>().Play();
            
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}
