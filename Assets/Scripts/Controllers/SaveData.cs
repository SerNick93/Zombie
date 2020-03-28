using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{   /// <summary>
    /// FOR PLAYER
    /// </summary>
    /* >Player Position
     * >Active Weapon
     * >Weapons in all three slots
     * >Ammo for all weapons
     * 
     * 
     * 
     * 
     * 
     */
    public float[] playerPosition;

    public SaveData(PlayerMovement player)
    {
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }

    /// <summary>
    /// FOR OPTIONS
    /// </summary>
    /* >Look Sensitivity
     * 
     * 
     * 
     * 
     */







}
