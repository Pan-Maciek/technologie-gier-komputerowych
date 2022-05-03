using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    private Text levelText;
    private LevelSystem LS;

    private void Awake()
    {
        levelText = transform/*.Find("levelTextObject")*/.GetComponent<Text>();
    }

    private void Update()
    {
        int level = LS.getLevel();
        int experience = LS.getExperience();
        int toLvlUp = LS.getExperienceToLevelUp();
        
        levelText.text = $"LEVEL {level}\nEXP {experience}\nTO LVL UP {toLvlUp}";

        if(Input.GetKeyDown(KeyCode.Space))
        {
            LS.AddExperience(50);
        }
    }

    public void SetLevelSystem(LevelSystem system)
    {
        this.LS = system;
    }
}
