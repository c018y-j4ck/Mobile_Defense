using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void PurchaseTurret()
    {
        Debug.Log("Purchased the regular turret");
        buildManager.SetTurretToBuild(buildManager.turretPrefab);
    }
    public void PurchaseMissile()
    {
        Debug.Log("Purchased the missile turret");
        buildManager.SetTurretToBuild(buildManager.missilePrefab);
    }
}
