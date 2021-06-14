using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb; 
    AudioSource audioSource;
    [SerializeField] float Thrust=10f;
    [SerializeField] float RotationThrust=1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles, RightThrusterParticle, LeftThrusterParticle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.W))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 1f);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
        rb.AddRelativeForce(Vector3.up * Thrust * Time.deltaTime);
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        RightThrusterParticle.Stop();
        LeftThrusterParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-RotationThrust);
        if (!LeftThrusterParticle.isPlaying)
        {
            LeftThrusterParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(RotationThrust);
        if (!RightThrusterParticle.isPlaying)
        {
            RightThrusterParticle.Play();
        }
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
