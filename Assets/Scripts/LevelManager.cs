using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{    
    public static int totalMemories = 0;
    public Text memoryCount;


    void Start()
    {
        totalMemories = 0;
        memoryCount.text = "memories: " + totalMemories; 
    }

    // Update is called once per frame
    void Update()
    {
  
    }


    public void UpdateMemoryCountText()
    {
        totalMemories++;

        memoryCount.text = "memories: " + totalMemories;
    }
}
