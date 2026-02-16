using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [Header("Settings")]
    public string characterName = "NPC";
    public DialogueSO dialogueData;
    public float interactRadius = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Visual Prompt (Optional)")]
    public GameObject interactIcon;

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;
        if (interactIcon) interactIcon.SetActive(false);
    }

    void Update()
    {
        if (!player) return;

        bool isTalking = DialogueManager.Instance.IsDialogueActive;
        float dist = Vector3.Distance(transform.position, player.position);
        bool inRange = dist <= interactRadius;

        if (interactIcon)
        {
            bool shouldShow = inRange && !isTalking;

            if (interactIcon.activeSelf != shouldShow)
            {
                interactIcon.SetActive(shouldShow);
            }
        }

        if (isTalking)
        {
            if (Input.GetKeyDown(interactKey) || Input.GetMouseButtonDown(0))
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
        }
        else if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);

                DialogueManager.Instance.StartDialogue(dialogueData);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
