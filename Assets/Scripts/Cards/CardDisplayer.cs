using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI answerText;

    string _question;
    string _answer;

    public bool isSelected = false;

    public void InitializeCard(string question, string answer)
    {
        rb = GetComponent<Rigidbody>();
        SetQuestion(question);
        SetAnswer(answer);

        answerText.enabled = false;
    }

    private void Update()
    {
        if (rb != null && rb.IsSleeping())
        {
            rb.isKinematic = true;
        }

        if(isSelected && Input.GetKeyDown(KeyCode.Space))
        {
            answerText.enabled = true;
        }
    }

    void SetQuestion(string quest)
    {
        _question = quest;
        questionText.text = _question;
    }
    void SetAnswer(string ans)
    {
        _answer = ans;
        answerText.text = _answer;
    }



    /// <summary>
    /// Set Rigidbody's isKinematic parameter. Use it when you don't want to calculate the physics.
    /// </summary>
    /// <param name="val">TRUE for disabling physics, FALSE for enabling it.</param>
    public void SetKinematic(bool val)
    {
        rb.isKinematic = val;
    }
}
