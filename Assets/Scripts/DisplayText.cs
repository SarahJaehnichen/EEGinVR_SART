using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayText : MonoBehaviour
{
    string[] number = new string[]{"1", "2", "3", "4", "5", "6", "7", "8", "9"};
    string digit;
    string xxx;
    public TextMeshProUGUI display;


    // Start by showing sign that occurs between the trials.
    void Start()
    {
        showpause(out xxx);
    }

    void Update() {
        
    }

    // Display a random number between 1 and 9. A trial takes place every time this function is called.
    public void show(out string currNum)
    {
        currNum = number[Random.Range (0, number.Length)];
        display.text = currNum;
    }

    // Display the sign 'X'. This will be shown between the trials.
    public void showpause(out string curr)
    {
        curr = "X";
        display.text = curr;
    }    
}
