using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //SaveGame
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadGame
            LoadGame();
        }
    }
    public void LoadGame()
    {
        //Load Player Position
        SaveData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        PlayerMovement.MyInstance.transform.position = position;
    }


}
