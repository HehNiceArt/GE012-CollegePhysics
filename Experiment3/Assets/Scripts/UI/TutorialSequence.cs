using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialSequence : MonoBehaviour
{
    [SerializeField] private Animator animClip;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.02f;
    [SerializeField] private string fullDialogue = "";

    private bool isTyping = false;
    private bool textFinished = false;
    private bool isAnimationTriggered = false;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = fullDialogue;
                isTyping = false;
                textFinished = true;
            }
            else if (textFinished && !isAnimationTriggered)
            {
                if (animClip != null)
                {
                    animClip.Play("DarkTutorial");
                    isAnimationTriggered = true;
                }
            }
            else if (textFinished && isAnimationTriggered)
            {
                SceneManager.LoadScene("MASTER");
            }
        }

        if (animClip != null && isAnimationTriggered)
        {
            AnimatorStateInfo stateInfo = animClip.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("DarkTutorial") && stateInfo.normalizedTime >= 1f)
            {
                SceneManager.LoadScene("MASTER");
            }
        }
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in fullDialogue.ToCharArray())
        {
            if (!isTyping)
            {
                yield break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        textFinished = true;
    }
}
