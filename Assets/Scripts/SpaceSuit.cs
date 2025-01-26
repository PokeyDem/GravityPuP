 using UnityEngine;

public class SpaceSuit : MonoBehaviour
{
    [SerializeField] public float timeToLiveInSmoke = 3f;
    private bool inSmoke = false;

    void FixedUpdate()
    {
        if (inSmoke) {
            timeToLiveInSmoke -= Time.fixedDeltaTime;
        } else {
            if (timeToLiveInSmoke < 3f)
            timeToLiveInSmoke += Time.fixedDeltaTime / 30;
        }
        print(timeToLiveInSmoke);
    }

    public void SetInSmoke(bool inSmoke) {
        this.inSmoke = inSmoke;
    }
}
