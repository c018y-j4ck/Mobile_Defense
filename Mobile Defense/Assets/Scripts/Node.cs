using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;

    private GameObject turret;

    public static BuildManager buildManager;

    public Color highlightedColor;
    public float turretYOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("There is already a turret in this space. (Note: will need UI message)");
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + Vector3.up * turretYOffset, transform.rotation);
    }

    //on mouse methods below highlight the node that the player hovers over
    private void OnMouseEnter()
    {
        rend.material.color = highlightedColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
