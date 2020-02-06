using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy myInstance;
    // Start is called before the first frame update
    void Awake()
    {
        if (myInstance == null)
        {
            myInstance = this;
            DontDestroyOnLoad(this);
        }
        else if (myInstance != this)
        {
            Destroy(this);
        }
        
    }

}
