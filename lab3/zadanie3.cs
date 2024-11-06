using UnityEngine;

public class MoveCubeSquare : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3[] points;
    private int currentPointIndex = 0;
    private Vector3 targetPoint;

    void Start()
    {
        points = new Vector3[4];
        Vector3 startPos = transform.position;

        points[0] = startPos;
        points[1] = startPos + new Vector3(10, 0, 0);
        points[2] = startPos + new Vector3(10, 0, 10);
        points[3] = startPos + new Vector3(0, 0, 10);

        targetPoint = points[currentPointIndex];
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, step);

        if (Vector3.Distance(transform.position, targetPoint) < 0.001f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
            targetPoint = points[currentPointIndex];
            transform.Rotate(0, 90, 0);
        }
    }
}
