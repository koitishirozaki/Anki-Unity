using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using MyBox;
using System.IO;

public class CardsManagement : MonoBehaviour
{
    private static string fileName = "/Cards.json";
    private string savePath;

    public List<Card> cachedCards = new List<Card>();


    private void Start()
    {
        savePath = Application.persistentDataPath + fileName;

        ClearCache();
        LoadCards();
    }

    void ClearCache()
    {
        cachedCards.Clear();
    }

    void DeleteCard(int cardId)
    {
        for (int i = 0; i < cachedCards.Count; i++)
        {
            if (cachedCards[i].id == cardId)
            {
                cachedCards.Remove(cachedCards[i]);
            }
        }
        SaveCards();
    }

    //  ATTENTION  Always load this before executing the rest of the code (creating and saving)!!!
    void LoadCards()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            Card[] _tempCards = JsonHelper.FromJson<Card>(jsonData);
            List<Card> cardsLoaded = _tempCards.OfType<Card>().ToList();
            for (int i = 0; i < cardsLoaded.Count; i++)
            {
                cachedCards.Add(cardsLoaded[i]);
            }
        }
    }
    void SaveCards()
    {
        string dataToSave = JsonHelper.ToJson(cachedCards.ToArray());
        System.IO.File.WriteAllText(savePath, dataToSave);
    }

    void CreateCard(string question, string answer)
    {
        Card tempCard = new Card((cachedCards.Count), question, answer);
        cachedCards.Add(tempCard);
    }
}
