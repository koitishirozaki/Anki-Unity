using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using MyBox;
using System.IO;

public class CardsManagement : MonoBehaviour
{

    // CSV import
    string csvPath;

    // Saving
    private static string fileName = "/Cards.json";
    private string savePath;

    // Instancing
    public GameObject cardPrefab;
    public Transform spawnTransform;

    // Runtime
    public List<Card> cachedCards = new List<Card>();

    private List<GameObject> cardObjects = new List<GameObject>();

    [ButtonMethod]
    public void LoadCardsFromCSV()
    {
        ClearCache();
        LoadSaveCardsFromCSV(csvPath);
    }
    [ButtonMethod]
    public void AppendCardsFromCSV()
    {
        LoadSaveCardsFromCSV(csvPath);
    }

    [ButtonMethod]
    public void CreateDummyCards()
    {
        ClearCache();
        CreateCard("Abacate", "1");
        CreateCard("Abacatee", "2");
        CreateCard("Abacateee", "3");
        CreateCard("Abacateeee", "4");
        CreateCard("Abacateeeee", "5");
        CreateCard("Abacateeeeee", "6");
        CreateCard("Abacateeeeeee", "7");
        SaveCards();
    }
    [ButtonMethod]
    public void AppendDummyCards()
    {
        CreateCard("Abacate", "1");
        CreateCard("Abacatee", "2");
        CreateCard("Abacateee", "3");
        CreateCard("Abacateeee", "4");
        CreateCard("Abacateeeee", "5");
        CreateCard("Abacateeeeee", "6");
        CreateCard("Abacateeeeeee", "7");
        SaveCards();
    }

    [ButtonMethod]
    public void Instantiate3DCards()
    {
        InstantiateAllCards();
    }
    [ButtonMethod]
    public void Delete3DCards()
    {
        DeleteCardsInstances();
    }

    [ButtonMethod]
    public void PurgeCards()
    {
        DeleteCardsFromSave();
        DeleteCardsInstances();
    }


    private void Start()
    {
        savePath = Application.persistentDataPath + fileName;
        csvPath = Application.persistentDataPath + "/theNewDeck.csv";

        ClearCache();
        LoadCards();
    }

    void ClearCache()
    {
        cachedCards.Clear();
    }

    void InstantiateAllCards()
    {
        for (int i = 0; i < cachedCards.Count; i++)
        {
            GameObject cardTemp = GameObject.Instantiate(cardPrefab, spawnTransform.position + new Vector3(0, i * 0.1f, 0), Quaternion.Euler(90, 0, 0), this.transform);
            cardTemp.GetComponent<CardDisplayer>().InitializeCard(cachedCards[i].question, cachedCards[i].answer);
            cardObjects.Add(cardTemp);
        }
    }
    void DeleteCardsInstances()
    {
        foreach (GameObject obj in cardObjects)
        {
            Destroy(obj);
        }
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
    void DeleteCardsFromSave()
    {
        cachedCards.Clear();
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
    void LoadSaveCardsFromCSV(string csvPath)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(csvPath);
        for (int i = 0; i < data.Count; i++)
        {
            int id = int.Parse(data[i]["id"].ToString(), System.Globalization.NumberStyles.Integer);
            string question = data[i]["question"].ToString();
            string answer = data[i]["answer"].ToString();
            CreateCard(question, answer);
        }
        SaveCards();
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
    public void AppendCard(string question, string answer)
    {
        Card tempCard = new Card(0, question, answer);
        cachedCards.Add(tempCard);
        SaveCards();
    }
}
