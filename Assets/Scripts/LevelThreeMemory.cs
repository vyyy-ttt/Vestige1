using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelThreeMemory : MonoBehaviour
{
    public static int totalMemories = 0;
    public static bool hasTriedElevator = false;

    public void InitializeMemory()
    {
        totalMemories = 0;
        hasTriedElevator = false;
    }

    public void UpdateMemoryCountText(Text memoryCount, Text memoryInfo)
    {
        totalMemories++;
        memoryInfo.gameObject.SetActive(true);

        if (totalMemories == 0)
        {
            memoryInfo.text = "You pick up a pair of gold bands – wedding rings – with ‘C&S 6/7/17 engraved on the inside.";
        }
        else if (totalMemories == 1)
        {
            memoryInfo.text = "You pick up a bouquet of flowers, slightly wilted. There’s a card attached but you can’t quite make out it says. They still smell nice but you're not sure how that makes you feel.";
        }
        else if (totalMemories == 2)
        {
            memoryInfo.text = "You pick up an old drawing notebook. You flip through the pages, the first few contain rudimentary sketches of still lives and anatomy studies. The rest is filled with crayon drawings of families, dogs, parks, school, friends, and the last page a teddy bear in a hospital room";
        }
        else if (totalMemories == 3)
        {
            memoryInfo.text = "You pick up a pile of bills – unpaid hospital bills, late electricity payments, past due rent, etc. The words seem to blur together as you flip through the pages and it becomes harder to tell what money is owed for what by the time you reach the end of the pile";
        }
        else if (totalMemories == 4)
        {
            memoryInfo.text = "You pick up card with the photo of a young girl smiling up at you. ‘In Loving Memory Emma Grace DeMarco 2017-2024’ A short Psalm expert is written on the back";
        }
        else if (totalMemories == 5)
        {
            memoryInfo.text = "You pick up a plain business card. It looks worn and bent, as if someone kept it in their pocket for a while forgotten about. No writing printed on it, just a phone number scribbled out. You wonder if you've ever called it...";
        }

        Debug.Log(totalMemories);
        if (hasTriedElevator)
        {
            memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
        }
        else
        {
            memoryCount.text = "memories: " + totalMemories;
        }
        Debug.Log(memoryCount.text);
    }

    public void UpdateMemoryText(Text memoryCount)
    {
        if (hasTriedElevator)
        {
            memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
        }
        else
        {
            memoryCount.text = "memories: " + totalMemories;
        }
    }
}
