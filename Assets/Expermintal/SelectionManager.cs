using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] string selectableTag = "Selectable";
    GameState gameState;
    [SerializeField] float maxInteractableDistance = 2f;

    bool waitedForTime = false;
    [SerializeField] float timeBeforeTextApearance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.IsThereSomethingPickedUp())
        {
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxInteractableDistance) )
        {

            var selection = hit.transform;

            if (selection.CompareTag(selectableTag))
            {
                InteractionScreenMode();
                if (Input.GetKey(KeyCode.E))
                {
                    NormalScreenMode();
                    hit.transform.gameObject.SendMessage("GetPickedByPlayer");
                }
            }
        }
        else
        {
            NormalScreenMode();
        }
    }
    void NormalScreenMode()
    {

        gameState.EnableCrossHairCanvas();
        gameState.DisableInteractionCanvas();
    }
    void InteractionScreenMode()
    {
        gameState.DisableCrosshairCanvas();
        gameState.EnableInteractionCanvas();
    }
}
