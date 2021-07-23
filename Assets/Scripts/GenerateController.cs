using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateController : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int maxObjectCount;
    [SerializeField] private int generationDelay;

    private List<GameObject> objectList;

    public event Action<GameObject> objectAdded;
    public event Action objectRemoved;

    private void Awake()
    {
        objectList = new List<GameObject>();
    }

    private void OnEnable()
    {
        StartCoroutine("GeneratorCoroutine");
    }

    private void OnDisable()
    {
        StopCoroutine("GeneratorCoroutine");
    }

    private IEnumerator GeneratorCoroutine()
    {
        while (objectList.Count < maxObjectCount)
        {
            InstantiatePrefab();
            yield return new WaitForSeconds(generationDelay);
        }
        StopCoroutine("GeneratorCoroutine");
    }

    private bool InstantiatePrefab()
    {
        GameObject newBee = Instantiate(objectPrefab, transform);

        if (newBee != null)
        {
            objectList.Add(newBee);
            objectAdded?.Invoke(newBee);
            return true;
        }
        else
        {
            return false;
        }
    }
}