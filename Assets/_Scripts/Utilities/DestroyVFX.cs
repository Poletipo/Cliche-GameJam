using UnityEngine;

public class DestroyVFX : MonoBehaviour
{

    private float _timer = 0;
    private float _duration = 999;
    public bool IsPooled = false;

    private void Start()
    {
        _duration = GetComponent<ParticleSystem>().main.duration;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _duration)
        {

            if (IsPooled)
            {
                EndVfx();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void EndVfx()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void Setup(Vector3 position, Quaternion rotation)
    {
        _duration = GetComponent<ParticleSystem>().main.duration;
        _timer = 0;
        transform.position = position;
        transform.rotation = rotation;

        gameObject.SetActive(true);
    }

}
