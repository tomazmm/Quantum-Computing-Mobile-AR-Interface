using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void closeNotification(GameObject gObject)
    {
        gObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
