using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive= true;
        Debug.Log("started Conversage! Message loaded" + messages.Length);
        DisplayeMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }


    void DisplayeMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorid];
        actorName.text = actorToDisplay.name;
        actorImage.sprite= actorToDisplay.sprite;
        AnimateTextColor();
    }
    

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayeMessage();
        }
        else
        {
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;

            Debug.Log("END OF CONV");
        }
    }
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            NextMessage();
        }
    }
}
