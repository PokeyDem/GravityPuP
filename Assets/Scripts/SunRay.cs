using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunRay : MonoBehaviour
{
    [SerializeField] private float appearTime = 1.5f;
    [SerializeField] private float disappearTime = 5f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        StartCoroutine(DisappearForTime());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private IEnumerator AppearForTime() {
        spriteRenderer.enabled = true;
        collider.enabled = true;
        yield return new WaitForSeconds(appearTime);
        StartCoroutine(DisappearForTime());
    }

    private IEnumerator DisappearForTime() {
        spriteRenderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(disappearTime);
        StartCoroutine(AppearForTime());
    }
}
