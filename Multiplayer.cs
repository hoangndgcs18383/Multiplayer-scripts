using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer : Gameplay
{
    void Update()
    {
        OpenMenu();
        OpenTab();
        OpenInput();
        OpenInventory();

        if (timeCount == false && isComplete == false)
        {
            StartCoroutine(Timer());
        }
    }
}
