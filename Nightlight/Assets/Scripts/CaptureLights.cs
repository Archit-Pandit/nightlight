using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureLights : MonoBehaviour
{
    [SerializeField] private int lightLayer = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != lightLayer) { return; }

        ScoreManager.Instance.AddScore(1);
        Debug.Log("Captured light");

        Destroy(collision.gameObject);
    }
}
