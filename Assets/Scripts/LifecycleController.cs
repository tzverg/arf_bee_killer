using System;
using System.Collections;
using UnityEngine;

public class LifecycleController : MonoBehaviour
{
    [SerializeField] private float additionalDestroyDelay;

    private Animation anim;
    private Material sharedMaterial;
    private Color fadedColor;
    public bool OnDestroy { get; private set; }
    public bool OnFade { get; private set; }
    private string deathAnimName;
    private float deathAnimLenght;

    private bool fadeIn;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();

        deathAnimName = GetRandomDeathAnim();
        sharedMaterial = GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial;
        fadedColor = sharedMaterial.color;
        fadedColor.a = 0F;
    }

    private string GetRandomDeathAnim()
    {
        float randomSeed = UnityEngine.Random.Range(0F, 1F);

        if (randomSeed > 0.5F)
        {
            deathAnimLenght = anim.GetClip("death1").length;
            return "death1";
        }
        else
        {
            deathAnimLenght = anim.GetClip("death2").length;
            return "death2";
        }
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    public void OnKillObject()
    {
        OnDestroy = true;
        OnFade = true;

        anim.Play(deathAnimName);

        Invoke("DestroyMe", deathAnimLenght + additionalDestroyDelay);
    }

    private void Update()
    {
        if (OnFade)
        {
            //sharedMaterial.color = Color.Lerp(sharedMaterial.color, fadedColor, additionalDestroyDelay);
        }
    }
}