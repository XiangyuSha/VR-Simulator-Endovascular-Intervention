using UnityEngine;

public class closeCanvas : MonoBehaviour
{
    public Canvas warningCanvas;

    // Method to close the warning canvas
    public void CloseCanvas()
    {
        warningCanvas.gameObject.SetActive(false); // Hide the canvas
    }    
}

