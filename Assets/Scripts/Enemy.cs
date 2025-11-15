using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage()
    {
        Debug.Log($"{gameObject.name} take damage");
    }
}
