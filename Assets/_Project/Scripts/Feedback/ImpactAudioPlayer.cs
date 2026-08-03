using UnityEngine;

public class ImpactAudioPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallImpactReporter ballImpactReporter;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] impactClips;

    [Header("Audio Settings")]
    [SerializeField] private float minPitch = 0.95f;
    [SerializeField] private float maxPitch = 1.08f;

    private void OnEnable()
    {
        ballImpactReporter.BallImpacted += PlayImpactSound;
    }

    private void OnDisable()
    {
        ballImpactReporter.BallImpacted -= PlayImpactSound;
    }

    private void PlayImpactSound(Vector3 impactPoint, float impactSpeed)
    {
        if (impactClips == null || impactClips.Length == 0) return;

        AudioClip clip = impactClips[Random.Range(0, impactClips.Length)];

        audioSource.transform.position = impactPoint;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }
}