using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public float Lifetime = 5f;
    private float timer = 0f;
    void Start()
    {
        
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
