using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LatLonDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera playerCamera; // Assign your main camera in the Inspector
    public TextMeshProUGUI latLonText; // Assign the TextMeshPro UI component

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera == null || latLonText == null) return;

        // Get camera orientation in world space
        Vector3 forward = playerCamera.transform.forward;

        // Convert direction to spherical coordinates (latitude & longitude)
        float latitude = Mathf.Asin(forward.y) * Mathf.Rad2Deg; // Y is the vertical axis
        float longitude = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg; // XZ plane for longitude

        // Update UI text
        latLonText.text = $"Lat: {latitude:F2}°\nLon: {longitude:F2}°";
    }
}
