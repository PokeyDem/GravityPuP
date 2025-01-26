using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed = 0.3f;
    [SerializeField] private float sizeThreshold = 0.3f;

    public void Die()
    {
        StartCoroutine(ShrinkAndReload());
    }

    private IEnumerator ShrinkAndReload()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Transform playerTransform = transform;

        while (playerTransform.localScale.x > sizeThreshold && playerTransform.localScale.y > sizeThreshold)
        {
            playerTransform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, 0) * Time.deltaTime;

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a -= shrinkSpeed * Time.deltaTime;
                spriteRenderer.color = color;
            }

            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
