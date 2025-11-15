using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SpriteRenderer sr;
    [SerializeField] private float redColorDuration = 1;
    private Color originColor;
    private float colorDuration;
    private bool needReverse = false;
    private float lastTakeDamageTime = 0;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    void Update()
    {
        if (!needReverse) return;

        var currentTime = Time.time;

        if (currentTime > lastTakeDamageTime + redColorDuration)
        {
            Turnback();
            lastTakeDamageTime = currentTime;
        }
    }

    void Update1()
    {
        if (!needReverse) return;

        colorDuration += Time.deltaTime;
        if (colorDuration > redColorDuration)
        {
            Turnback();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage()
    {
        sr.color = Color.red;
        needReverse = true;
        lastTakeDamageTime = Time.time;
    }

    public void TakeDamage1()
    {
        sr.color = Color.red;
        lastTakeDamageTime = Time.time;
        needReverse = true;
    }

    public void TakeDamage2()
    {
        sr.color = Color.red;
        Invoke(nameof(Turnback), redColorDuration);
    }


    void Turnback()
    {
        sr.color = originColor;
        needReverse = false;
    }
}
