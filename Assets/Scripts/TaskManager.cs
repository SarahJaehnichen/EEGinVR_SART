using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;
using System.Threading;

public class TaskManager : MonoBehaviour
{
    public DisplayText displayText;
    public WriteCSV writeCSV;
    public int num;
    public List<string> data = new List<string>();
    string currData;
    string digit;
    string xxx;
    private bool correctness;
    string header = "NumberOfTrials,NumberShown,Response,Correctness,ResponseTime";
    private bool responded = false;
    Stopwatch s = new Stopwatch();
    Stopwatch r = new Stopwatch();
    Stopwatch responsetime = new Stopwatch();
    string timing;
    private bool trial;


    // Set num to zero before the first frame update. We will start with a trial (and not with a pause). Thus trial is set to true.
    // The pause display is shown for 5 seconds.
    void Start()
    {
        num = 0;
        trial = true;
        displayText.showpause(out xxx);
        Thread.Sleep(5000);
    }

    // One update represents one trial or one break.
    void Update()
    {
        // We will have 225 valid trials in total. The first 2 trials will be deleted because they usually appear to fast to be responded to.
        // When finished, participants get a positive feedback sentence and the data is saved in a csv file.
        if (num >= 227)
        {
            displayText.display.text = "Well done!";
            writeCSV.MakeCSV(data, header);
            UnityEngine.Debug.Log("end");
        }

        // A trial takes place every second update.
        else if (trial)
        {
            // The number of trials is increased accordingly.
            // The trial digit is shown for 0.75 seconds.
            // During this time, participants may respond with pressing the space key.
            // In case they do so, the responsetime is recorded and we notice that they have responded.
            num = num + 1;
            s.Start();
            responsetime.Start();
            displayText.show(out digit);
            while (s.Elapsed < TimeSpan.FromSeconds(0.75)) 
            {
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    responsetime.Stop();
                    responded = true;
                    timing = responsetime.ElapsedMilliseconds.ToString();
                }
            }
            s.Stop();
            s.Reset();

            // Now, it is investigated whether the action (responding or not) was correct.
            // In case a response was detected, the action was correct given that the digit shown was a number different from 3.
            // In case a response was detected and the number shown was 3, responding was incorrect.
            // In case the participant did not respond, this was correct in case the number shown was 3 
            // and incorrect in case the number shown was not 3.    
            if(responded)
            {
                if(digit == "3")
                {
                    correctness = false;
                }
                else
                {
                    correctness = true;
                }
            }
            else
            {
                if(digit == "3")
                {
                    correctness = true;
                }
                else
                {
                    correctness = false;
                }
            }
            
            // We save the current trial and add it to our data.
            // It is saved: the current number of trials, the digit that was shown, whether the participant responded, 
            // whether it was correct that the participant did or did not respond and how much time responding (if so) took
            currData = (num.ToString() + "," + digit + "," + responded + "," + correctness + "," + timing);
            data.Add(currData);
            UnityEngine.Debug.Log(currData);

            // Some variables are reset for the next round. trial is set to false such that the next update will call a pause.
            responded = false;
            timing = "";
            trial = false;
            responsetime.Reset();             
        } 
        // A pause takes place every second update.
        else if (!trial)
        {
            // The pause sign is shown for 0.9 seconds.
            // trial is set to true such that the next update will call a trial.
            displayText.showpause(out xxx);
            Thread.Sleep(900);
            trial = true;
        }
    }
}