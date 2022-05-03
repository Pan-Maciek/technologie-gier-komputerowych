using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING : MonoBehaviour
{
    [SerializeField] private LevelDisplay level_display;
    private void Awake()
    {
        LevelSystem LS = new LevelSystem();
        level_display.SetLevelSystem(LS);
    }
}
