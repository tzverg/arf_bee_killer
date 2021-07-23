using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifecycleController))]
public class MotionController : MonoBehaviour
{
    [SerializeField] private MotionModel motionModel;
    [SerializeField] private float minMotionSpeed;
    [SerializeField] private float maxMotionSpeed;
    [SerializeField] private float minMotionDelay;
    [SerializeField] private float maxMotionDelay;

    private Animation anim;
    private LifecycleController lifecycleC;
    private Vector3 destination;
    private float step;
    private bool isMoved;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        lifecycleC = GetComponentInChildren<LifecycleController>();
    }

    private void OnEnable()
    {
        StartCoroutine("MotionCoroutine");
    }

    private void OnDisable()
    {
        StopCoroutine("MotionCoroutine");
    }

    private void Update()
    {
        if (isMoved)
        {
            Move();
        }
    }

    private float GetRandomFloatValue(float minInclusiveValue, float maxInclusiveValue)
    {
        return UnityEngine.Random.Range(minInclusiveValue, maxInclusiveValue);
    }

    private void GetRandomDestination()
    {
        switch (motionModel)
        {
            case MotionModel.OnPlane:
                destination = GetRandomDirectionOnPlane();
                break;
            case MotionModel.InAir:
                destination = GetRandomDirectionOnAir();
                break;
            default:
                Debug.LogError("we have no such motion model");
                break;
        }
        transform.LookAt(destination);
        isMoved = true;
    }

    private Vector3 GetRandomDirectionOnAir()
    {
        return transform.position + Random.insideUnitSphere;
    }

    private Vector3 GetRandomDirectionOnPlane()
    {
        Vector3 newDirection = Random.insideUnitSphere;
        newDirection.y = 0F;
        return transform.position + newDirection;
    }

    private IEnumerator MotionCoroutine()
    {
        while (isActiveAndEnabled && !lifecycleC.OnDestroy)
        {
            GetRandomDestination();
            yield return new WaitForSeconds(GetRandomFloatValue(minMotionDelay, maxMotionDelay));
        }
    }

    private void RunRandomAnimation(string animA, string animB)
    {
        float randomSeed = UnityEngine.Random.Range(0, 1);

        if (randomSeed > 0.5F)
        {
            anim.Play(animA);
        }
        else
        {
            anim.Play(animB);
        }
    }

    private void Move()
    {
        step = GetRandomFloatValue(minMotionSpeed, maxMotionSpeed) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        RunRandomAnimation("run", "walk");

        if (Vector3.Distance(transform.position, destination) <= 0.01F)
        {
            isMoved = false;
            transform.position = destination;
            anim.Play("idle");
        }
    }
}