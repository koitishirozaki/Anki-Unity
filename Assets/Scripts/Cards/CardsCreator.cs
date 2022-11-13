using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public class CardsCreator : MonoBehaviour
{
    public string question;
    public string answer;

    CardsManagement management;

    private void Start()
    {
        management = GetComponent<CardsManagement>();
    }
    [ButtonMethod]
    public void CreateCard()
    {
        management.AppendCard(question, answer);
        question = "";
        answer = "";
    }

}
