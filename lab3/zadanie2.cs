using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 1.0f;
    private float startX;
    private bool movingRight = true;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        if (movingRight)
        {
            transform.Translate(step, 0, 0);

            if (transform.position.x >= startX + 10)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(-step, 0, 0);

            if (transform.position.x <= startX)
            {
                movingRight = true;
            }
        }
    }
}
