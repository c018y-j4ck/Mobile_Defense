using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    /// <summary>
    /// A singleton of the build manager. Note that there should only be one of these in a scene!
    /// </summary>
    public static BuildManager Instance { get; private set; }

    private GameObject turretToBuild;

    void Awake()
    {
        if (Instance != null) Debug.LogError("Error: more than one BuildManager in the scene. " +
            "You should only have one BuildManager in a scene.");
        Instance = this;
    }

    /// <summary>
    /// Prefab of our first turret. We should rename this later to match the name
    /// of the turret itself
    /// </summary>
    public GameObject turretPrefab;

    private void Start()
    {
        turretToBuild = turretPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
