
// now if you want to make purchse button make EX: UnlockCharacter(2);

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;
using System;
using TMPro;

public class CharSwitcher : MonoBehaviour
{


    public GameObject[] Characters;
    public String[] CharName;
    private bool[] unlockedCharacters;

    public AudioClip[] SoundCharName;

    private int currentIndex = 0;

    private AudioSource audioSource;
    public AudioClip clipSwitch;

    // TEXTMESH PRO FOR CHAR NAME
    public TextMeshProUGUI charText;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipSwitch;

        LoadUnlockStates();

        UpdateCharName();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeftArrow()
    {
        if (Characters.Length == 0) return;

        Characters[currentIndex].SetActive(false);

        currentIndex = (currentIndex - 1 + Characters.Length) % Characters.Length;

        Characters[currentIndex].SetActive(true);

        StartCoroutine(RotateAndReset(Characters[currentIndex].transform));
        audioSource.Play();

        UpdateCharName();
        PlayCharacterNameAudio();
    }

    private IEnumerator RotateAndReset(Transform character)
    {
        character.rotation = Quaternion.Euler(0, 150f, 0);
        yield return new WaitForSeconds(3f);
        character.rotation = Quaternion.identity;
    }


    public void RightArrow()
    {
        if (Characters.Length == 0) return;

        if (Characters.Length > 2)
        {
            // WHEN I REACT FROM PLAYER 3 TO REST 
            // I WILL SHOW LOCK UI
            // AND IF CLICK UNLOCK 
            // I WILL MAKE FILE FOR CHAR'S LIKE
            // public static bool openChar2 = false
            // IF HE HAVE ENOUGH GOLD I WILL TURN TRUE
            // BUT IN THIS CASE EACH TIME USER BACK TO 
            // THE GAME NEED TO FIN HE IS OPEN CHARS STILL
        }

        // EX: if(charsStates.openChar2){setactive the false and he can play with show tap to start again}



        Characters[currentIndex].SetActive(false);


        currentIndex = (currentIndex + 1) % Characters.Length;


        Characters[currentIndex].SetActive(true);

        StartCoroutine(RotateAndReset(Characters[currentIndex].transform));

        audioSource.Play();

        UpdateCharName();
        PlayCharacterNameAudio();
    }


    public void UpdateCharName()
    {
        charText.text = CharName[currentIndex];
    }

    private void PlayCharacterNameAudio()
    {
        audioSource.PlayOneShot(SoundCharName[currentIndex]);
    }

    public void UnlockCharacter(int index)
    {
        if (index >= 0 && index < unlockedCharacters.Length)
        {
            unlockedCharacters[index] = true;
            SaveUnlockStates();
        }

        // The rest of Unlocking Codes
    }

    
    void SaveUnlockStates()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            PlayerPrefs.SetInt("CharUnlocked_" + i, unlockedCharacters[i] ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    void LoadUnlockStates()
    {
        unlockedCharacters = new bool[Characters.Length];

        for (int i = 0; i < Characters.Length; i++)
        {
            unlockedCharacters[i] = PlayerPrefs.GetInt("CharUnlocked_" + i, i == 0 ? 1 : 0) == 1;
            Characters[i].SetActive(false);
        }

        Characters[currentIndex].SetActive(true);
    }
}