using System.Collections.Generic;
using UnityEngine;

public class FallPlatformManager : MonoBehaviour
{
    public static FallPlatformManager Instance { get; private set; }

    private List<FallPlatform> platforms = new List<FallPlatform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlatform(FallPlatform platform)
    {
        if (!platforms.Contains(platform))
        {
            platforms.Add(platform);
        }
    }

    public void ResetAllPlatforms()
    {
        foreach (FallPlatform platform in platforms)
        {
            platform.ResetFallPlatform();
        }
    }
}
