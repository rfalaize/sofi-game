using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string[] messages;
    [SerializeField] private int timeBetweenBubblesInSeconds = 2;
    [SerializeField] private int timeBeforeHidingBubbleInSeconds = 5;
    [SerializeField] private float typingSpeed = 0.03f;

    private Transform dialogBubbleTransform;
    private TextMeshProUGUI dialogText;
    private bool isSpeaking = false;

    void Start()
    {
        dialogBubbleTransform = transform.Find("DialogBubble");
        dialogText = transform.Find("DialogBubble/Canvas/DialogText").GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isSpeaking) return;
            isSpeaking = true;
            ShowDialog();
            StopCoroutine("StartSpeaking");
            StartCoroutine("StartSpeaking");
        }
    }

    private void ShowDialog()
    {
        dialogBubbleTransform.gameObject.SetActive(true);
    }

    private void HideDialog()
    {
        dialogBubbleTransform.gameObject.SetActive(false);
        dialogText.text = "";
    }

    private IEnumerator StartSpeaking()
    {
        if (messages == null || messages.Length == 0) yield return null;
        foreach (var msg in messages)
        {
            if (msg == null || msg.Equals("")) break;
            dialogText.text = "";
            foreach (char c in msg.ToCharArray())
            {
                yield return new WaitForSeconds(typingSpeed);
                dialogText.text += c;
            }
            yield return new WaitForSeconds(timeBetweenBubblesInSeconds); // wait a bit between each sentence
        }
        yield return new WaitForSeconds(timeBeforeHidingBubbleInSeconds);
        isSpeaking = false;
        HideDialog();
        yield return null;
    }

}
