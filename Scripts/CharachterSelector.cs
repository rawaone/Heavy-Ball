using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSelector : MonoBehaviour
{

    public GameObject[] characters;
    [SerializeField] int selectedCharacter = 0;

    void Start()
    {
        foreach(GameObject ch in characters)
        {
            ch.SetActive(false);
        }

        selectedCharacter = PlayerPrefs.GetInt("savecharacter");
        characters[selectedCharacter].SetActive(true);
        
    }

    public void ChangeCharacter(int newCharacter)
    {
        characters[selectedCharacter].SetActive(false);
        characters[newCharacter].SetActive(true);
        selectedCharacter = newCharacter;
        PlayerPrefs.SetInt("savecharacter", selectedCharacter);
        Debug.Log("The equip saved");
    }
}
