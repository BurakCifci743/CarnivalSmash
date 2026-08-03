using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanKnockdownDetector))]
public class CanFadeOutOnKnockdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Rigidbody rb;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDelay = 1f;
    [SerializeField] private float fadeDuration = 0.35f;
    [SerializeField] private bool disableAfterFade = true;

    private CanKnockdownDetector knockdownDetector;
    private Material[] materials;
    private Vector3 initialScale;
    private bool fadeStarted;

    private void Awake()
    {
        knockdownDetector = GetComponent<CanKnockdownDetector>();

        if (renderers == null || renderers.Length == 0)
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        if (colliders == null || colliders.Length == 0)
        {
            colliders = GetComponentsInChildren<Collider>();
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }

        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        knockdownDetector.KnockedDown += HandleKnockedDown;
    }

    private void OnDisable()
    {
        knockdownDetector.KnockedDown -= HandleKnockedDown;
    }

    private void HandleKnockedDown()
    {
        if (fadeStarted) return;

        fadeStarted = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeDelay);

        foreach (Collider targetCollider in colliders)
        {
            targetCollider.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float alpha = Mathf.Lerp(1f, 0f, t);

            SetAlpha(alpha);
            transform.localScale = Vector3.Lerp(initialScale, initialScale * 0.85f, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);

        if (disableAfterFade)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetAlpha(float alpha)
    {
        foreach (Material material in materials)
        {
            if (material.HasProperty("_BaseColor"))
            {
                Color color = material.GetColor("_BaseColor");
                color.a = alpha;
                material.SetColor("_BaseColor", color);
            }
            else if (material.HasProperty("_Color"))
            {
                Color color = material.GetColor("_Color");
                color.a = alpha;
                material.SetColor("_Color", color);
            }
        }
    }
}