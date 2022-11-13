using System;

[Serializable]
public class Card
{
    public int id;
    public string question;
    public string answer;

    public Card(int id, string q, string a)
    {
        this.id = id;
        this.question = q;
        this.answer = a;
    }
}