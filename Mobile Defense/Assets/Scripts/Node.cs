using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;

    private GameObject turret;

    public static BuildManager buildManager;

    public Color highlightedColor;
    public float turretYOffset = 0.5f;
    private bool isTurretSpot = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.Instance;

        if (this.gameObject.tag == "turretSpot")
        {
            isTurretSpot = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (buildManager.GetTurretToBuild() == null) return;

        if (turret != null)
        {
            Debug.Log("There is already a turret in this space. (Note: will need UI message)");
            return;
        }
        Turret tScript;
        if (buildManager.GetTurretToBuild().TryGetComponent<Turret>(out tScript) && isTurretSpot)
        {
            if (Director.score >= tScript.cost)
            {
                GameObject turretToBuild = buildManager.GetTurretToBuild();
                Director.RemoveScore(tScript.cost);
                turret = (GameObject)Instantiate(turretToBuild, transform.position + Vector3.up * turretYOffset, transform.rotation);
            }
        }
    }

    //on mouse methods below highlight the node that the player hovers over
    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null) return;

        if (turret == null && isTurretSpot)
        {
            if (Director.score >= 5)
            {
                rend.material.color = Color.green;
            }
            else
            {
                rend.material.color = Color.yellow;
            }
        }
        else
        {
            rend.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
