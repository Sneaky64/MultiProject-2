using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager instance;
    float coins = 0;
    public void AddCoin()
    {
        coins++;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            DestroyImmediate(this);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

