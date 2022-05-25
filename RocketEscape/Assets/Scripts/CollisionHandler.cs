using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float KillLoadDelay = 2f;
    [SerializeField] AudioClip collision;
    [SerializeField] AudioClip success;

    AudioSource audioSource;
    bool isTransitioning = false;

    int nextSceneIndex;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return; 
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have hit a friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }        
        isTransitioning = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                Debug.Log("You have hit a Fuel Cell - Fuel Reloaded");
                break;
            default:
                Debug.Log("You have hit a Power Up - Your ship is now MOAR AWESOMER!!");
                break;
        }
    }

    int GetNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;        

        if (currentLevel == SceneManager.sceneCountInBuildSettings - 1)
        {
            return 0; //change later to finish the game
        }
        else 
        {
            return currentLevel + 1;
        }
    }

    void InvokeLevelChange(int sceneIndex, float loadDelay)
    {
        nextSceneIndex = sceneIndex;
        Invoke("LoadLevel", loadDelay);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        //TODO: Add particle effects
        audioSource.PlayOneShot(collision);        
        GetComponent<Movement>().enabled = false;
        InvokeLevelChange(SceneManager.GetActiveScene().buildIndex, KillLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        //TODO: Add particle effects
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        InvokeLevelChange(GetNextLevel(), LevelLoadDelay);
    }
}
