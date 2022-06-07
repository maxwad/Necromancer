using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage instance;

    public GameObject unitManager;
    public GameObject boostManager;
    public GameObject battleManager;
    public GameObject playerManager;

    public GameObject player;

    public bool isGlobalMode = true;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //InitializeAll();
    }

    private void InitializeAll()
    {

    }

}
