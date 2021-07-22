using UnityEngine;

public class BeeMotionController : MonoBehaviour
{
    [SerializeField] private Space relativeSpace;
    [SerializeField] private float speed;

    private Vector3 destination;
    private float step;
    private bool isMoved;

    public void GetRandomDestination()
    {
        destination = transform.position + Random.insideUnitSphere;
        isMoved = true;
    }

    private void Update()
    {
        if (isMoved)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);

            if (Vector3.Distance(transform.position, destination) <= 0.01F)
            {
                isMoved = false;
                transform.position = destination;
            }
        }
    }
}