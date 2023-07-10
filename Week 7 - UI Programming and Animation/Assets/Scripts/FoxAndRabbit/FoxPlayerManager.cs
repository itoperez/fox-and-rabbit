using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoxPlayerManager : MonoBehaviour
{
    private int counterChest, counterWoodbox, counterFlower, counterMushroom;
    private int totalChest, totalWoodbox, totalFlower, totalMushroom;

    public GameObject Chest, Woodboxes, Flowers, Mushrooms;

    private bool foundAllChest, foundAllWoodboxes, foundAllFlowers, foundAllMushrooms;
    private bool lasersOn;


    private void Start()
    {
        totalChest = GameObject.FindGameObjectsWithTag("TreasureChest").Length;
        totalWoodbox = GameObject.FindGameObjectsWithTag("Woodbox").Length;
        totalFlower = GameObject.FindGameObjectsWithTag("Flower").Length;
        totalMushroom = GameObject.FindGameObjectsWithTag("Mushroom").Length;

        Chest.GetComponent<TextMeshProUGUI>().text = "Chest Opened: " + counterChest.ToString() + "/" + totalChest.ToString();
        Woodboxes.GetComponent<TextMeshProUGUI>().text = "Rabbits Freed:  " + counterWoodbox.ToString() + "/" + totalWoodbox.ToString();
        Flowers.GetComponent<TextMeshProUGUI>().text = "Flowers Smelled: " + counterFlower.ToString() + "/" + totalFlower.ToString();
        Mushrooms.GetComponent<TextMeshProUGUI>().text = "Mushroom Jumps: " + counterMushroom.ToString() + "/" + totalMushroom.ToString();

        foundAllChest = false; 
        foundAllWoodboxes = false; 
        foundAllFlowers = false; 
        foundAllMushrooms = false;

        lasersOn = true;
    }

    private void Update()
    {
        if(foundAllChest & foundAllWoodboxes & foundAllFlowers & foundAllMushrooms & lasersOn)
        {
            GameObject.Find("Lasers").SetActive(false);
            lasersOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Opening Chest        
        if (other.CompareTag("TreasureChest") & !other.GetComponent<TouchedPlayer>().hasTouchedPlayer)
        {
            counterChest++;
            Chest.GetComponent<TextMeshProUGUI>().text = "Chest Opened: " + counterChest.ToString() + "/" + totalChest.ToString();
            other.GetComponent<TouchedPlayer>().hasTouchedPlayer = true;
            if(counterChest == totalChest)
            {
                foundAllChest = true;
            }
        }
        
        // Freeing rabbits        
        if (other.CompareTag("Woodbox") & !other.GetComponent<TouchedPlayer>().hasTouchedPlayer)
        {
            counterWoodbox++;
            Woodboxes.GetComponent<TextMeshProUGUI>().text = "Rabbits Freed:  " + counterWoodbox.ToString() + "/" + totalWoodbox.ToString();
            other.GetComponent<TouchedPlayer>().hasTouchedPlayer = true;
            if (counterWoodbox == totalWoodbox)
            {
                foundAllWoodboxes = true;
            }
        }
        
        // Smelling flowers        
        if (other.CompareTag("Flower") & !other.GetComponent<TouchedPlayer>().hasTouchedPlayer)
        {
            counterFlower++;
            Flowers.GetComponent<TextMeshProUGUI>().text = "Flowers Smelled: " + counterFlower.ToString() + "/" + totalFlower.ToString();
            other.GetComponent<TouchedPlayer>().hasTouchedPlayer = true;
            if (counterFlower == totalFlower)
            {
                foundAllFlowers = true;
            }
        }
        
        // Mushroom Jumps
        if (other.CompareTag("Mushroom") & !other.GetComponent<TouchedPlayer>().hasTouchedPlayer)
        {
            counterMushroom++;
            Mushrooms.GetComponent<TextMeshProUGUI>().text = "Mushroom Jumps: " + counterMushroom.ToString() + "/" + totalMushroom.ToString();
            other.GetComponent<TouchedPlayer>().hasTouchedPlayer = true;
            if (counterMushroom == totalMushroom)
            {
                foundAllMushrooms = true;
            }
        }

        // Completed all, game over
        if (other.CompareTag("Trophy"))
        {
            Debug.Log("Yay! You won!");
        }
    }
}

