using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject interactionCanvas;
    [SerializeField] GameObject CrosshairCanvas;
    bool somethingIsPickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        interactionCanvas.SetActive(false);
        CrosshairCanvas.SetActive(true);
    }

    public void DisableInteractionCanvas()
    {
        interactionCanvas.SetActive(false);
    }
    public void EnableInteractionCanvas()
    {
        interactionCanvas.SetActive(true);
    }
    public void DisableCrosshairCanvas()
    {
        CrosshairCanvas.SetActive(false);
    }

    public void EnableCrossHairCanvas()
    {
        CrosshairCanvas.SetActive(true);
    }

    public bool IsThereSomethingPickedUp()
    {
        return somethingIsPickedUp;
    }
    public void SomethingIsPickedUp()
    {
        somethingIsPickedUp = true;
    }

    public void NothingIsPickedUp()
    {
        somethingIsPickedUp = false;
    }
}
