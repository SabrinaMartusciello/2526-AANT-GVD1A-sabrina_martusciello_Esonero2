using UnityEngine;

public class InteractableNPC : MonoBehaviour
{
    [Header("Dati personaggio")]
    public string npcName = "Anima Errante";

    [TextArea(2, 6)]
    public string[] dialogueLines;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    DialogueManager.Instance.StartDialogue(npcName, dialogueLines);
                }
            }
        }
    }
}