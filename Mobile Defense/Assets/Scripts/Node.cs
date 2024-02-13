using UnityEngine;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;

    private GameObject turret;

    public Color highlightedColor;

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
        if (turret != null) Debug.Log("There is already a turret in this space. (Note: will need UI message)");


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
