
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] string selectableTag = "Selectable";
    GameState gameState;
    Ball ball;
    [SerializeField] float maxInteractableDistance = 2f;
    bool waitedForTime = false; //for later use, dont delete (zoldyak)
    [SerializeField] float timeBeforeTextApearance = 1f;//for later use dont delete (zoldyak)
    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        ball = FindObjectOfType<Ball>();//TODO if decided to have two balls at a time in a scene
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
                    if (selection.GetComponent<BallSticky>())
                    {
                        gameState.DisableCrosshairCanvas();
                        gameState.DisableInteractionCanvas();
                        gameState.enableChangeBallTypeCanvas();
                    }
                    else
                    {
                        NormalScreenMode();
                    }
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
        gameState.DisableChangeBallTypeCanvas();
    }
    void InteractionScreenMode()
    {
        gameState.DisableCrosshairCanvas();
        gameState.EnableInteractionCanvas();
    }
}
