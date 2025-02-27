using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Disappearing : MonoBehaviour
{
    public Transform player; // Reference to the player's camera (or player object)
    public float disappearDistance = 5f; // Distance at which the object starts disappearing
    public float fullyInvisibleDistance = 2f; // Distance at which the object is fully invisible

    // private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer meshRenderer;
    private Material material;

    void Start(){
        // meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("Disappearing script: No MeshRenderer found on " + gameObject.name);
            return;
        }

        // Ensure the material exists and is assigned
        if (meshRenderer.material == null)
        {
            Debug.LogError("Disappearing script: No material found on " + gameObject.name);
            return;
        }

        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        // Calculate distance between player and object
        float distance = Vector3.Distance(transform.position, player.position);

        // Calculate transparency based on distance
        //float transparency = Mathf.InverseLerp(disappearDistance, fullyInvisibleDistance, distance);
        float transparency = Mathf.InverseLerp(fullyInvisibleDistance, disappearDistance, distance);


        // Set material transparency
        Color color = material.color;
        color.a = transparency; // Adjust Alpha based on distance
        material.color = color;

        // Disable rendering if fully transparent
        meshRenderer.enabled = transparency > 0;

        // You have to select the property name, not the display name
        // meshRenderer.material.SetFloat("_Transparency", transparency);
    }
}