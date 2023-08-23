using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDrawer : MonoBehaviour
{
    [SerializeField] private GameObject lineRenderer;

    private GameObject lineObj;

    private Coroutine rope;

    private LineRenderer line;

    private PolygonCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawRope();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    private void DrawRope()
    {
        if (rope != null) { StopCoroutine(rope); }

        rope = StartCoroutine(StartDrawing());
    }

    private void StopDrawing()
    {
        StopCoroutine(rope);

        CreateTriggerCollider();
    }

    private IEnumerator StartDrawing()
    {
        lineObj = Instantiate(lineRenderer);

        lineObj.transform.position = Vector3.zero;

        line = lineObj.GetComponent<LineRenderer>();
        col = lineObj.GetComponent<PolygonCollider2D>();

        line.positionCount = 0;

        while (true)
        {
            Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camPos.z = 0;

            line.positionCount++;
            line.SetPosition(line.positionCount - 1, camPos);

            yield return null;
        }
    }

    private void CreateTriggerCollider()
    {
        line.loop = true;
        col.SetPath(0, new Vector2[0]);

        List<Vector2> colEdges = new List<Vector2>();

        for (int i = 0; i < line.positionCount; i++)
        {
            Vector2 point = line.GetPosition(i);
            
            colEdges.Add(point);
        }

        col.SetPath(0, colEdges);
        col.enabled = true;
    }
}
