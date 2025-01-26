using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Smoke : MonoBehaviour
{
    [SerializeField] public GameObject smokePrefab;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask smokeLayer;
    private float raycastDistance = 1f;
    private bool canSpawn = true;
    private SpawnDirection spawnDirection = SpawnDirection.Right;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            SpaceSuit spaceSuit = other.GetComponent<SpaceSuit>();
            spaceSuit.SetInSmoke(true);
            if (spaceSuit.timeToLiveInSmoke <= 0f) {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") {
            SpaceSuit spaceSuit = other.GetComponent<SpaceSuit>();
            spaceSuit.SetInSmoke(false);
        }
    }

    private void Start()
    {
        UseSmoke();
    }

    private void UseSmoke() {
        if (IsSmokeAndGroundInAllDirections())
        {
            return; 
        }
        RaycastHit2D hitDownGround = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitGroundRight = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance, groundLayer);
        RaycastHit2D[] hitDownSmoke = Physics2D.RaycastAll(transform.position, Vector2.down, raycastDistance, smokeLayer);
        RaycastHit2D[] hitRightSmoke = Physics2D.RaycastAll(transform.position, Vector2.right, raycastDistance, smokeLayer);

        if (hitDownGround.collider == null && hitDownSmoke.Length == 1)
        {
            StartCoroutine(SpawnSmokeBelow());
        }
        else if (hitGroundRight.collider == null && hitRightSmoke.Length == 1)
        {
            StartCoroutine(SpawnSmokeRight());
        } else {
            StartCoroutine(SpawnSmokeAbove());
        }
    }

     private bool IsSmokeAndGroundInAllDirections()
    {
        RaycastHit2D hitGroundRight = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance, groundLayer);
        RaycastHit2D hitGroundLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance, groundLayer);
        RaycastHit2D hitGroundUp = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, groundLayer);
        RaycastHit2D hitGroundDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D[] hitDownSmoke = Physics2D.RaycastAll(transform.position, Vector2.down, raycastDistance, smokeLayer);
        RaycastHit2D[] hitUpSmoke = Physics2D.RaycastAll(transform.position, Vector2.up, raycastDistance, smokeLayer);
        RaycastHit2D[] hitLeftSmoke = Physics2D.RaycastAll(transform.position, Vector2.left, raycastDistance, smokeLayer);
        RaycastHit2D[] hitRightSmoke = Physics2D.RaycastAll(transform.position, Vector2.right, raycastDistance, smokeLayer);

        return (hitGroundRight.collider != null || hitRightSmoke.Length > 1)
                && (hitGroundLeft.collider != null || hitLeftSmoke.Length > 1)
                && (hitGroundUp.collider != null || hitUpSmoke.Length > 1)
                && (hitGroundDown.collider != null || hitDownSmoke.Length > 1);
    }

    private IEnumerator SpawnSmokeBelow()
    {
        yield return new WaitForSeconds(1f);
        SpawnSmoke(Vector3.down);
    }

    private IEnumerator SpawnSmokeRight()
    {
        yield return new WaitForSeconds(3f);
        SpawnSmoke(Vector3.right);
    }

    private IEnumerator SpawnSmokeAbove()
    {
        yield return new WaitForSeconds(15f);
        SpawnSmoke(Vector3.up);
    }

    private void SpawnSmoke(Vector3 direction)
    {
        Vector3 spawnPosition = transform.position + direction;
        GameObject instance = Instantiate(smokePrefab, spawnPosition, Quaternion.identity);
        
        Smoke smoke = instance.GetComponent<Smoke>();
        smoke.smokePrefab = gameObject;
        smoke.canSpawn = true;

        UseSmoke();
    }
}

public enum SpawnDirection
{
    Right,
    Down,
    Up
}
