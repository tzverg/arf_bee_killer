using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerateController : MonoBehaviour
{
    [SerializeField] private GameObject beePrefab;
    [SerializeField] private int objectCount;
    [SerializeField] private float motionDelay;
    private List<BeeMotionController> beeList;

    public event Action<GameObject> objectAdded;
    public event Action objectRemoved;

    private void OnEnable()
    {
        beeList = new List<BeeMotionController>();

        for(int cnt =0; cnt < objectCount; cnt++)
        {
            InstantiatePrefab();
        }

        StartCoroutine(MotionCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine("MotionCoroutine");
    }

    private IEnumerator MotionCoroutine()
    {
        while (true)
        {
            foreach (BeeMotionController targetMotionController in beeList)
            {
                targetMotionController.GetRandomDestination();
            }
            yield return new WaitForSeconds(motionDelay);
        }
    }

    private bool InstantiatePrefab()
    {
        GameObject newBee = Instantiate(beePrefab, transform);
        BeeMotionController newBeeController = newBee.GetComponent<BeeMotionController>();
        if (newBeeController != null)
        {
            newBeeController.GetRandomDestination();
            beeList.Add(newBeeController);
            objectAdded?.Invoke(newBee);
        }

        return true;
    }
}