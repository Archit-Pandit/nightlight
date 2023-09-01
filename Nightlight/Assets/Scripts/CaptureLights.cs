using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CaptureLights : MonoBehaviour
{
    [SerializeField] private ContactFilter2D lightLayer;

    [SerializeField] private float lightMoveSpeed = 10f;

    private Animator animator;

    private Vector3 captureDestination;

    private AnimatorStateInfo state;

    // Start is called before the first frame update
    void Start()
    {
        captureDestination = GameObject.FindGameObjectWithTag("Jar").transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Fade") && state.normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public void Capture()
    {
        Collider2D collider = GetComponent<Collider2D>();
        List<Collider2D> lights = new List<Collider2D>();

        Physics2D.OverlapCollider(collider, lightLayer, lights);

        ScoreManager.Instance.AddScore(lights.Count);

        StartCoroutine(MoveLight(lights));

        animator.Play("Fade");
    }

    private IEnumerator MoveLight(List<Collider2D> lightsCol)
    {
        foreach (Collider2D col in lightsCol)
        {
            StartCoroutine(Translate(col.gameObject.transform));

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Translate(Transform lightTransform)
    {
        while(Vector3.Distance(lightTransform.position, captureDestination) > 0.001f)
        {
            var step = lightMoveSpeed * Time.deltaTime;
            lightTransform.position = Vector3.MoveTowards(lightTransform.position, captureDestination, step);

            yield return null;
        }

        Destroy(lightTransform.gameObject);
    }
}
