using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadLevelDelay = 1f;
    AudioSource audioSource;
    [SerializeField] AudioClip SuccessClip, DeathClip;
    [SerializeField] ParticleSystem SuccessParticles, DeathParticles;

    bool isTransitioning = false, CollisionStatus=true; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            CollisionStatus = !CollisionStatus;

        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || !CollisionStatus)
        {
            return;
        }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }        
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(SuccessClip, 1f);    
        Invoke("LoadNextLevel", LoadLevelDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(DeathClip, 1f);         
        Invoke("ReloadLevel", LoadLevelDelay);
    }

    void LoadNextLevel()
    {
        isTransitioning = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        isTransitioning = false;
        GetComponent<Movement>().enabled = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
