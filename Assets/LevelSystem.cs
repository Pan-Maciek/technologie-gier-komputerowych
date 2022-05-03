using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem
{
    private int level;
    private int experience;
    
    private int experienceToNextLevel(int currentLevel)
    {
        return 100 * currentLevel;
    }

    public LevelSystem()
    {
        level = 1;
        experience = 0;
    }

    public void TriggerLevelUp(){
        experience = Math.Max(0, experience - experienceToNextLevel(level)); 
        // I'm limiting the "experience" by 0, if by any chance, we would like to trigger the LevelUp mechanic through e.g. "Level Up Potions"
        level += 1;
    }

    public void AddExperience(int gain)
    {
        experience += gain;
        if(experience >= experienceToNextLevel(level))
        {
            TriggerLevelUp();
        }
    }

    public int getLevel()
    {
        return level;
    }

    public int getExperience()
    {
        return experience;
    }

    public int getExperienceToLevelUp()
    {
        return experienceToNextLevel(level) - experience;
    }

    public String getAllData()  // TODO Just for TESTING
    {
        return $"Level: {level}\tExperience: {getExperience()}\tTo level up: {getExperienceToLevelUp()}";
    }

}
