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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(rb != null && rb.IsSleeping())
        {
            rb.isKinematic = true;
        }
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
